using System;
using System.Runtime.InteropServices;

namespace RagnarokController
{
    public class PacketManager
    {
        private readonly MemoryManager _memoryManager;
        private readonly MemoryScanner _scanner;
        
        // Headers dos pacotes
        private const ushort HEADER_CZ_REQUEST_MOVE = 0x2e5;
        
        // Padrões de bytes para funções importantes
        private static readonly byte[][] SEND_PACKET_PATTERNS = new byte[][]
        { 
            new byte[] // Padrão 1: Início da função com setup de stack e verificação de this
            { 
                0x55,                   // push ebp
                0x8B, 0xEC,            // mov ebp, esp
                0x83, 0xEC, 0x08,      // sub esp, 8
                0x53,                   // push ebx
                0x56,                   // push esi
                0x57,                   // push edi
                0x8B, 0xF9,            // mov edi, ecx
                0x85, 0xFF             // test edi, edi
            },
            new byte[] // Padrão 2: Verificação de tamanho do pacote e buffer
            {
                0x8B, 0x45, 0x08,      // mov eax, [ebp+08]
                0x85, 0xC0,            // test eax, eax
                0x7E, 0x00,            // jle short
                0x8B, 0x4D, 0x0C       // mov ecx, [ebp+0C]
            },
            new byte[] // Padrão 3: Chamada para GetPacketSize e comparação
            {
                0x8B, 0x4D, 0x08,      // mov ecx, [ebp+08]
                0xE8, 0x00, 0x00, 0x00, 0x00, // call GetPacketSize
                0x3B, 0x45, 0x08       // cmp eax, [ebp+08]
            }
        };
        
        private static readonly string[] SEND_PACKET_MASKS = new string[]
        {
            "xxxxxxxxxxxxx",
            "xxxxxxx?xxx",
            "xxxx????xxx"
        };

        // Padrões para CRagConnection::instanceR
        private static readonly byte[][] INSTANCE_R_PATTERNS = new byte[][]
        {
            new byte[] // Padrão 1: Início da função e acesso à variável global
            {
                0x55,                   // push ebp
                0x8B, 0xEC,            // mov ebp, esp
                0x83, 0xEC, 0x08,      // sub esp, 8
                0xA1, 0x00, 0x00, 0x00, 0x00  // mov eax, [g_ragConnection]
            },
            new byte[] // Padrão 2: Verificação de instância e retorno
            {
                0xA1, 0x00, 0x00, 0x00, 0x00, // mov eax, [g_ragConnection]
                0x85, 0xC0,                    // test eax, eax
                0x75, 0x00,                    // jnz short
                0x68                           // push offset
            }
        };
        
        private static readonly string[] INSTANCE_R_MASKS = new string[]
        {
            "xxxxxxxx????",
            "x????xx?x"
        };
        
        private IntPtr _sendPacketAddress;
        private IntPtr _instanceRAddress;
        private IntPtr? _cachedConnectionPtr;
        
        // Estrutura do pacote de movimento (baseada no PDB antigo)
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct PACKET_CZ_REQUEST_MOVE
        {
            public ushort PacketHeader;  // 0x2e5
            public int DestX;            // Coordenada X de destino
            public int DestY;            // Coordenada Y de destino
            public byte Type;            // Tipo de movimento (0 = andar, 1 = teleportar)
        }

        // Delegates para as funções
        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        private delegate bool SendPacketDelegate(IntPtr thisPtr, int size, IntPtr buffer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr InstanceRDelegate();

        public PacketManager(MemoryManager memoryManager)
        {
            _memoryManager = memoryManager;
            _scanner = new MemoryScanner(memoryManager);
            FindPacketFunctions();
        }

        private void FindPacketFunctions()
        {
            Console.WriteLine("Procurando funções de processamento de pacotes...");

            // Cria o analisador de executáveis
            var analyzer = new ExecutableAnalyzer(
                @"high\HighPriest.exe",  // Executável antigo com PDB
                @"high\Ragexe.exe",      // Executável atual
                _scanner
            );

            // Analisa os executáveis e obtém os endereços
            analyzer.AnalyzeExecutables();
            var addresses = analyzer.GetNewAddresses();

            // Atualiza os endereços encontrados
            if (addresses.TryGetValue("SendPacket", out IntPtr sendPacketAddr))
            {
                _sendPacketAddress = sendPacketAddr;
                Console.WriteLine($"Função SendPacket encontrada em: 0x{_sendPacketAddress.ToInt64():X}");
            }
            else
            {
                Console.WriteLine("Aviso: Não foi possível encontrar a função SendPacket!");
            }

            if (addresses.TryGetValue("instanceR", out IntPtr instanceRAddr))
            {
                _instanceRAddress = instanceRAddr;
                Console.WriteLine($"Função instanceR encontrada em: 0x{_instanceRAddress.ToInt64():X}");
            }
            else
            {
                Console.WriteLine("Aviso: Não foi possível encontrar a função instanceR!");
            }
        }

        public void SendMovePacket(int x, int y)
        {
            if (_sendPacketAddress == IntPtr.Zero)
            {
                Console.WriteLine("Erro: Função SendPacket não encontrada!");
                return;
            }

            var packet = new PACKET_CZ_REQUEST_MOVE
            {
                PacketHeader = HEADER_CZ_REQUEST_MOVE,
                DestX = x,
                DestY = y
            };

            // Aloca memória para o pacote
            byte[] packetBytes = StructureToByteArray(packet);
            int packetSize = packetBytes.Length;
            
            IntPtr packetBuffer = Marshal.AllocHGlobal(packetSize);
            Marshal.Copy(packetBytes, 0, packetBuffer, packetSize);

            try
            {
                // Obtém o ponteiro this do CRagConnection
                IntPtr ragConnectionPtr = GetRagConnectionInstance();
                if (ragConnectionPtr == IntPtr.Zero)
                {
                    Console.WriteLine("Erro: Não foi possível encontrar a instância do CRagConnection!");
                    return;
                }

                // Cria o delegate para a função SendPacket
                var sendPacket = Marshal.GetDelegateForFunctionPointer<SendPacketDelegate>(_sendPacketAddress);
                
                // Chama a função
                bool result = sendPacket(ragConnectionPtr, packetSize, packetBuffer);
                if (!result)
                {
                    Console.WriteLine("Erro ao enviar o pacote de movimento!");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(packetBuffer);
            }
        }

        private IntPtr GetRagConnectionInstance()
        {
            if (_cachedConnectionPtr.HasValue)
                return _cachedConnectionPtr.Value;

            if (_instanceRAddress == IntPtr.Zero)
                return IntPtr.Zero;

            try
            {
                var instanceR = Marshal.GetDelegateForFunctionPointer<InstanceRDelegate>(_instanceRAddress);
                _cachedConnectionPtr = instanceR();
                
                // Valida o ponteiro retornado
                if (_cachedConnectionPtr.Value != IntPtr.Zero)
                {
                    // Tenta ler alguns bytes para verificar se é um ponteiro válido
                    byte[] test = _memoryManager.ReadMemory(_cachedConnectionPtr.Value, 4);
                }
                
                return _cachedConnectionPtr.Value;
            }
            catch
            {
                _cachedConnectionPtr = IntPtr.Zero;
                return IntPtr.Zero;
            }
        }

        private byte[] StructureToByteArray(object structure)
        {
            int size = Marshal.SizeOf(structure);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(structure, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }
    }
} 