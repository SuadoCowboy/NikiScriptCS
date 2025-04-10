using System.Runtime.InteropServices;

public static partial class NikiScript
{
	[DllImport("libNikiScript.dll", EntryPoint = "ns_freeCharArray", CallingConvention = CallingConvention.Cdecl)]
	private static extern void DeleteCharArray(IntPtr charArrayPtr);

	[DllImport("libNikiScript.dll", EntryPoint = "ns_free2DCharArray", CallingConvention = CallingConvention.Cdecl)]
	private static extern void Delete2DCharArray(IntPtr charArray2dPtr);
	
	/// <summary>
	/// creates an string array from const char** i.e. C version of string array.<br/>
	/// This function will free the memory allocated for the array of strings by calling Delete2DCharArray.<br/>
	/// The array of strings is allocated in the C++ code and passed to this function.<br/>
	/// </summary>
	/// <param name="_stringsPtrs">pointer to a const char** object</param>
	/// <param name="size">how many items there are in the array stringsPtrs</param>
	private static string[] GetStringArrayFromConstCharArray(IntPtr _stringsPtrs, int size) {
		// const char*[]
		IntPtr[] stringPtrs = new IntPtr[size];
		Marshal.Copy(_stringsPtrs, stringPtrs, 0, size);
		string[] keys = new string[size];

		int i = 0;
		foreach(IntPtr stringPtr in stringPtrs) {
			keys[i] = Marshal.PtrToStringAnsi(stringPtr) ?? string.Empty;
			++i;
		}

		Delete2DCharArray(_stringsPtrs);
		return keys;
	}
}