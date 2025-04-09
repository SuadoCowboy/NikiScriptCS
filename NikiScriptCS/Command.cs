using System.Runtime.InteropServices;

/*
ns_newCommand
ns_deleteCommand
ns_CommandGetArgumentsNames
*/

public static partial class NikiScript
{
	public class Command
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CallbackDelegate(IntPtr ctxPtr);

		public CallbackDelegate Callback;
		private IntPtr CommandPtr;

		[DllImport("libNikiScript.dll", EntryPoint = "ns_newCommand", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _New(string name, byte minArgs, byte maxArgs, CallbackDelegate callback, string description, string[] argsDescriptions);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_deleteCommand", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Delete(IntPtr commandPtr);
		
		public void Delete()
		{
			if (CommandPtr == IntPtr.Zero) return;
			_Delete(CommandPtr);
			CommandPtr = IntPtr.Zero;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandAllocGetArgumentsNames", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _AllocGetArgumentsNames(IntPtr commandPtr);

		public string GetArgumentsNames()
		{
			IntPtr argsPtr = _AllocGetArgumentsNames(CommandPtr);

			string str = Marshal.PtrToStringAnsi(argsPtr) ?? "";
			DeleteString(argsPtr);
			return str;
		}

		public Command(string name, byte minArgs, byte maxArgs, CallbackDelegate callback, string description, string[] argsDescriptions) {
			Callback = callback;
			CommandPtr = _New(name, minArgs, maxArgs, Callback, description, argsDescriptions);
		}

		~Command() {
			_Delete(CommandPtr);
		}
	}
}