namespace Totten.Solution.BotAgent.Domain.Features.Sessions;
using System.Runtime.InteropServices;

[UnmanagedFunctionPointer(CallingConvention.ThisCall)] 
public unsafe delegate void GetMaxhp(CSession* ptr);

[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
public unsafe delegate void FuncDelegate(void* eaxStruct);