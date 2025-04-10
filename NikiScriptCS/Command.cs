using System.Runtime.InteropServices;

public static partial class NikiScript
{
	public class Command
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CallbackDelegate(IntPtr ctxPtr);

		public CallbackDelegate Callback { get; private set; }

		public IntPtr Ptr { get; private set; }

		public string Name {
			get => GetName();
			set => SetName(Ptr, value);
		}

		public byte MinArgs {
			get => GetMinArgs(Ptr);
			set => SetMinArgs(Ptr, value);
		}

		public byte MaxArgs {
			get => GetMaxArgs(Ptr);
			set => SetMaxArgs(Ptr, value);
		}

		public string Description {
			get => GetDescription();
			set => SetDescription(Ptr, value);
		}

		public string[] ArgsDescriptions {
			get => GetArgsDescriptions();
			set => SetArgsDescriptions(value);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandNew", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _New(string name, byte minArgs, byte maxArgs, CallbackDelegate callback, string description, string[] argsDescriptions);
		public Command(string name, byte minArgs, byte maxArgs, CallbackDelegate callback, string description, string[] argsDescriptions)
		{
			Callback = callback;
			Ptr = _New(name, minArgs, maxArgs, Callback, description, argsDescriptions);
		}

		public Command(IntPtr ptr)
		{
			Ptr = ptr;
			Callback = GetCallback();
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandDelete", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Delete(IntPtr Ptr);
		public void Delete()
		{
			if (Ptr == IntPtr.Zero)
				return;
			_Delete(Ptr);
			Ptr = IntPtr.Zero;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetArgumentsNames", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetArgumentsNames(IntPtr Ptr);
		public string GetArgumentsNames()
		{
			return Marshal.PtrToStringAnsi(_GetArgumentsNames(Ptr)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandPrintAsDataTree", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _PrintAsDataTree(IntPtr Ptr);
		public void PrintAsDataTree() { _PrintAsDataTree(Ptr); }

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetName", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetName(IntPtr Ptr, string name);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetName", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetName(IntPtr Ptr);
		private string GetName() {
			return Marshal.PtrToStringAnsi(_GetName(Ptr)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetMinArgs", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetMinArgs(IntPtr Ptr, byte minArgs);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetMinArgs", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte GetMinArgs(IntPtr Ptr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetMaxArgs", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetMaxArgs(IntPtr Ptr, byte maxArgs);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetMaxArgs", CallingConvention = CallingConvention.Cdecl)]
		private static extern byte GetMaxArgs(IntPtr Ptr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetCallback", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _SetCallback(IntPtr Ptr, CallbackDelegate callback);
		public void SetCallback(CallbackDelegate callback)
		{
			Callback = callback;
			_SetCallback(Ptr, callback);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetCallback", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetCallback(IntPtr Ptr);
		private CallbackDelegate GetCallback()
		{
			return Marshal.GetDelegateForFunctionPointer<CallbackDelegate>(_GetCallback(Ptr));
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetDescription", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetDescription(IntPtr Ptr, string description);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetDescription", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetDescription(IntPtr Ptr);
		private string GetDescription()
		{
			return Marshal.PtrToStringAnsi(_GetDescription(Ptr)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandSetArgsDescriptions", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _SetArgsDescriptions(IntPtr Ptr, string[] argsDescriptions);
		private void SetArgsDescriptions(string[] argsDescriptions)
		{
			_SetArgsDescriptions(Ptr, argsDescriptions);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetArgDescription", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetArgDescription(IntPtr Ptr, ushort index);
		public string GetArgDescription(ushort index)
		{
			return Marshal.PtrToStringAnsi(_GetArgDescription(Ptr, index)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandGetArgsDescriptionsSize", CallingConvention = CallingConvention.Cdecl)]
		private static extern ushort _GetArgsDescriptionsSize(IntPtr Ptr);
		public ushort GetArgsDescriptionsSize()
		{
			return _GetArgsDescriptionsSize(Ptr);
		}

		private string[] GetArgsDescriptions()
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