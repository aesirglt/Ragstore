# Ragstore Agent

Bot para Ragnarok Online que automatiza a coleta de informações de lojas de venda.

## Funcionalidades

- Detecta automaticamente lojas de venda no mapa
- Clica nas lojas e coleta informações
- Salva os dados dos pacotes de rede em arquivos de log
- Navega pelo mapa automaticamente
- Proxy integrado para captura de pacotes

## Requisitos

- Windows 10 ou superior
- .NET 8.0 Runtime
- Ragnarok Online client
- Resolução do jogo configurada para 800x600 (ajuste a variável `_gameWindowArea` em `ShopDetector.cs` se necessário)

## Configuração

1. Configure o cliente do Ragnarok para usar o proxy local:
   - Host: 127.0.0.1
   - Porta: 8080

2. Ajuste a área de captura da tela no arquivo `ShopDetector.cs` se necessário

3. Compile e execute o programa

## Uso

1. Abra o Ragnarok Online e faça login com seu personagem
2. Execute o Ragstore Agent
3. Clique em "Iniciar" para começar a coleta
4. O bot irá:
   - Detectar lojas no mapa
   - Clicar nas lojas automaticamente
   - Salvar informações dos pacotes
   - Navegar pelo mapa
5. Os logs serão salvos na pasta "shop_logs"

## Notas

- O bot usa processamento de imagem para detectar lojas e botões
- Ajuste os parâmetros de detecção em `ShopDetector.cs` se necessário
- Os logs são salvos em formato hexadecimal para análise posterior
- O proxy deve ser configurado no cliente do Ragnarok 