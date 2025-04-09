using System.Runtime.InteropServices;

public static partial class NikiScript
{
	public class Command
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CallbackDelegate(IntPtr ctxPtr);

		public CallbackDelegate Callback { get; private set; }

		public IntPtr CommandPtr { get; private set; }

		public string Name {
			get => GetName();
			set => SetName(CommandPtr, value);
		}

		public byte MinArgs {
			get => GetMinArgs(CommandPtr);
			set => SetMinArgs(CommandPtr, value);
		}

		public byte MaxArgs {
			get => GetMaxArgs(CommandPtr);
			set => SetMaxArgs(CommandPtr, value);
		}

		public string Description {
			get => GetDescription();
			set => SetDescription(CommandPtr, value);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandNew", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _New(string name, byte minArgs, byte maxArgs, CallbackDelegate callback, string description, string[] argsDescriptions);
		public Command(string name, byte minArgs, byte maxArgs, CallbackDelegate callback, string description, string[] argsDescriptions)
		{
			Callback = callback;
			CommandPtr = _New(name, minArgs, maxArgs, Callback, description, argsDescriptions);
		}

		public Command(IntPtr commandPtr)
		{
			CommandPtr = commandPtr;
			Callback = GetCallback();
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandDelete", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Delete(IntPtr commandPtr);
		public void Delete()
		{
			if (CommandPtr == IntPtr.Zero)
				return;
			_Delete(CommandPtr);
			CommandPtr = IntPtr.Zero;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetArgumentsNames", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetArgumentsNames(IntPtr commandPtr);
		public string GetArgumentsNames()
		{
			return Marshal.PtrToStringAnsi(_GetArgumentsNames(CommandPtr)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandPrintAsDataTree", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _PrintAsDataTree(IntPtr commandPtr);
		public void PrintAsDataTree() { _PrintAsDataTree(CommandPtr); }

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetName", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetName(IntPtr commandPtr, string name);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetName(IntPtr commandPtr);
		private string GetName() {
			return Marshal.PtrToStringAnsi(_GetName(CommandPtr)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetMinArgs", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetMinArgs(IntPtr commandPtr, byte minArgs);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetMinArgs", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte GetMinArgs(IntPtr commandPtr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetMaxArgs", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetMaxArgs(IntPtr commandPtr, byte maxArgs);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetMaxArgs", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte GetMaxArgs(IntPtr commandPtr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetCallback", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _SetCallback(IntPtr commandPtr, CallbackDelegate callback);
		public void SetCallback(CallbackDelegate callback)
		{
			Callback = callback;
			_SetCallback(CommandPtr, callback);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetCallback", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetCallback(IntPtr commandPtr);
		private CallbackDelegate GetCallback()
		{
			return Marshal.GetDelegateForFunctionPointer<CallbackDelegate>(_GetCallback(CommandPtr));
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetDescription", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetDescription(IntPtr commandPtr, string description);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetDescription", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetDescription(IntPtr commandPtr);
		private string GetDescription()
		{
			return Marshal.PtrToStringAnsi(_GetDescription(CommandPtr)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetArgsDescriptions", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _SetArgsDescriptions(IntPtr commandPtr, string[] argsDescriptions);
		public void SetArgsDescriptions(string[] argsDescriptions)
		{
			_SetArgsDescriptions(CommandPtr, argsDescriptions);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetArgDescription", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetArgDescription(IntPtr commandPtr, ushort index);
		public string GetArgDescription(ushort index)
		{
			return Marshal.PtrToStringAnsi(_GetArgDescription(CommandPtr, index)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetArgsDescriptionsSize", CallingConvention = CallingConvention.Cdecl)]
		private static extern ushort _GetArgsDescriptionsSize(IntPtr commandPtr);
		public ushort GetArgsDescriptionsSize()
		{
			return _GetArgsDescriptionsSize(CommandPtr);
		}

		public string[] GetArgsDescriptions()
		{
			ushort size = GetArgsDescriptionsSize();
			string[] argsDescriptions = new string[size];
			for (ushort i = 0; i < size; i++) {
				argsDescriptions[i] = GetArgDescription(i);
			}
			return argsDescriptions;
		}
	}
}