//ns_newString
//ns_freeString

using System.Runtime.InteropServices;

public static partial class NikiScript
{
	private class Base
	{
		[DllImport("libNikiScript.dll", EntryPoint = "ns_newString", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr NewString(ulong size);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_freeString", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DeleteString(IntPtr StringPointer);
	}
}