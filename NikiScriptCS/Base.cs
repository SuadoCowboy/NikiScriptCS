using System.Runtime.InteropServices;

public static partial class NikiScript
{
	[DllImport("libNikiScript.dll", EntryPoint = "ns_freeCharString", CallingConvention = CallingConvention.Cdecl)]
	private static extern void DeleteString(IntPtr stringPtr);

	[DllImport("libNikiScript.dll", EntryPoint = "ns_freeCharArray", CallingConvention = CallingConvention.Cdecl)]
	private static extern void DeleteArrayOfStringPointers(IntPtr arrayPtr);
}