using System.Runtime.InteropServices;

public static partial class NikiScript
{
	public enum Level : byte
	{
		DEFAULT = 0,
		ECHO,
		WARNING,
		ERROR
	}

	[DllImport("libNikiScript.dll", EntryPoint="ns_print", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Print(Level level, string message);

	[DllImport("libNikiScript.dll", EntryPoint="ns_printUnknownCommand", CallingConvention = CallingConvention.Cdecl)]
	public static extern void PrintUnknownCommand(string command);

	[DllImport("libNikiScript.dll", EntryPoint = "ns_levelToString", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr _LevelToString(Level level);

	public static string LevelToString(Level level)
	{
		return Marshal.PtrToStringAnsi(_LevelToString(level)) ?? "";
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void CallbackDelegate(IntPtr pData, Level level, string message);

	[DllImport("libNikiScript.dll", EntryPoint="ns_setPrintCallback", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SetPrintCallback(IntPtr pData, CallbackDelegate callback);
}