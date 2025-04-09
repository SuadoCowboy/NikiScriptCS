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
			get => _GetName();
			set => _SetName(CommandPtr, value);
		}

		public byte MinArgs {
			get => _GetMinArgs(CommandPtr);
			set => _SetMinArgs(CommandPtr, value);
		}

		public byte MaxArgs {
			get => _GetMaxArgs(CommandPtr);
			set => _SetMaxArgs(CommandPtr, value);
		}

		public string Description {
			get => _GetDescription();
			set => _SetDescription(CommandPtr, value);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_newCommand", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _New(string name, byte minArgs, byte maxArgs, CallbackDelegate callback, string description, string[] argsDescriptions);
		public Command(string name, byte minArgs, byte maxArgs, CallbackDelegate callback, string description, string[] argsDescriptions) {
			Callback = callback;
			CommandPtr = _New(name, minArgs, maxArgs, Callback, description, argsDescriptions);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_deleteCommand", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Delete(IntPtr commandPtr);
		public void Delete()
		{
			if (CommandPtr == IntPtr.Zero) return;
			_Delete(CommandPtr);
			CommandPtr = IntPtr.Zero;
		}

		~Command() {
			_Delete(CommandPtr);
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

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandPrintAsDataTree", CallingConvention =CallingConvention.Cdecl)]
		private static extern void _PrintAsDataTree(IntPtr commandPtr);
		public void PrintAsDataTree() { _PrintAsDataTree(CommandPtr); }

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetName", CallingConvention =CallingConvention.Cdecl)]
		private static extern void _SetName(IntPtr commandPtr, string name);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandAllocGetName", CallingConvention =CallingConvention.Cdecl)]
		private static extern IntPtr _AllocGetName(IntPtr commandPtr);
		private string _GetName() {
			IntPtr namePtr = _AllocGetName(CommandPtr);
			string name = Marshal.PtrToStringAnsi(namePtr) ?? "";
			DeleteString(namePtr);
			return name;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetMinArgs", CallingConvention =CallingConvention.Cdecl)]
		private static extern void _SetMinArgs(IntPtr commandPtr, byte minArgs);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetMinArgs", CallingConvention =CallingConvention.Cdecl)]
		private static extern byte _GetMinArgs(IntPtr commandPtr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetMaxArgs", CallingConvention =CallingConvention.Cdecl)]
		private static extern void _SetMaxArgs(IntPtr commandPtr, byte maxArgs);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetMaxArgs", CallingConvention =CallingConvention.Cdecl)]
		private static extern byte _GetMaxArgs(IntPtr commandPtr);

		// TODO: get; set for Callback??? If it doesn't work just remove those two functions then
		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetCallback", CallingConvention =CallingConvention.Cdecl)]
		private static extern void _SetCallback(IntPtr commandPtr, CallbackDelegate callback);
		public void SetCallback(CallbackDelegate callback)
		{
			Callback = callback;
			_SetCallback(CommandPtr, callback);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetDescription", CallingConvention =CallingConvention.Cdecl)]
		private static extern void _SetDescription(IntPtr commandPtr, string description);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandAllocGetDescription", CallingConvention =CallingConvention.Cdecl)]
		private static extern IntPtr _AllocGetDescription(IntPtr commandPtr);
		private string _GetDescription()
		{
			IntPtr descriptionPtr = _AllocGetDescription(CommandPtr);
			string description = Marshal.PtrToStringAnsi(descriptionPtr) ?? "";
			DeleteString(descriptionPtr);
			return description;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetArgsDescriptions", CallingConvention =CallingConvention.Cdecl)]
		private static extern void _SetArgsDescriptions(IntPtr commandPtr, string[] argsDescriptions);
		public void SetArgsDescriptions(string[] argsDescriptions)
		{
			_SetArgsDescriptions(CommandPtr, argsDescriptions);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandAllocGetArgDescription", CallingConvention =CallingConvention.Cdecl)]
		private static extern IntPtr _AllocGetArgDescription(IntPtr commandPtr, ushort index);
		public string GetArgDescription(ushort index)
		{
			IntPtr argDescriptionPtr = _AllocGetArgDescription(CommandPtr, index);
			string argDescription = Marshal.PtrToStringAnsi(argDescriptionPtr) ?? "";
			DeleteString(argDescriptionPtr);
			return argDescription;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetArgsDescriptionsSize", CallingConvention =CallingConvention.Cdecl)]
		private static extern ushort _GetArgsDescriptionsSize(IntPtr commandPtr);
		public ushort GetArgsDescriptionsSize()
		{
			return _GetArgsDescriptionsSize(CommandPtr);
		}

		public string[] GetArgsDescriptions() {
			ushort size = GetArgsDescriptionsSize();
			string[] argsDescriptions = new string[size];
			for (ushort i = 0; i < size; i++) {
				argsDescriptions[i] = GetArgDescription(i);
			}
			return argsDescriptions;
		}
	}
}