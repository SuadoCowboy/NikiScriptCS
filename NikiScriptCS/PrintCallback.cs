using System.Runtime.InteropServices;

internal static class NikiScript
{
	public enum PrintLevel : byte
	{
		DEFAULT = 0,
		ECHO,
		WARNING,
		ERROR
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void PrintCallbackDelegate(IntPtr pData, PrintLevel level, string message);

	[DllImport("Lib/libNikiScript.dll", EntryPoint="ns_setPrintCallback", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SetPrintCallback(IntPtr pData, PrintCallbackDelegate callback);

	[DllImport("Lib/libNikiScript.dll", EntryPoint="ns_print", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Print(PrintLevel level, string message);

	[DllImport("Lib/libNikiScript.dll", EntryPoint="ns_printUnknownCommand", CallingConvention = CallingConvention.Cdecl)]
	public static extern void PrintUnknownCommand(string command);

	[DllImport("Lib/libNikiScript.dll", EntryPoint="ns_printLevelToString", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr _PrintLevelToString(PrintLevel level);

	public static string PrintLevelToString(PrintLevel level)
	{
		IntPtr ptr = _PrintLevelToString(level);
		return Marshal.PtrToStringAnsi(ptr) ?? "";
	}
}