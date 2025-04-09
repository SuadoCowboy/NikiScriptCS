using System.Runtime.InteropServices;

public static partial class NikiScript
{
	[DllImport("libNikiScript.dll", EntryPoint = "ns_freeString", CallingConvention = CallingConvention.Cdecl)]
	private static extern void DeleteString(IntPtr StringPointer);
}