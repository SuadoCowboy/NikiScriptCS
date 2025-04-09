using System.Runtime.InteropServices;

public static partial class NikiScript
{
	public class CommandHandler
	{
		public IntPtr CommandHandlerPtr { get; private set; }

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerNew", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _New();

		public CommandHandler() {
			CommandHandlerPtr = _New();
		}

		public CommandHandler(IntPtr commandHandlerPtr) {
			CommandHandlerPtr = commandHandlerPtr;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerNew", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _Delete(IntPtr commandHandlerPtr);

		public void Delete()
		{
			if (CommandHandlerPtr == IntPtr.Zero)
				return;
			_Delete(CommandHandlerPtr);
			CommandHandlerPtr = IntPtr.Zero;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerGetCommandsNames", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetCommandsNames(IntPtr commandHandlerPtr);
		public string GetCommandsNames()
		{
			IntPtr namesPtr = _GetCommandsNames(CommandHandlerPtr);
			if (namesPtr == IntPtr.Zero)
				return string.Empty;

			string names = Marshal.PtrToStringAnsi(namesPtr) ?? string.Empty;
			return names;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerGetCommand", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetCommand(IntPtr commandHandlerPtr, string name);
		public Command? GetCommand(string name)
		{
			IntPtr commandPtr = _GetCommand(CommandHandlerPtr, name);
			if (commandPtr == IntPtr.Zero)
				return null;

			return new(commandPtr);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerAdd", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _Add(IntPtr commandHandlerPtr, IntPtr commandPtr);
		public bool Add(ref Command command)
		{
			IntPtr newCommandPtr = _Add(CommandHandlerPtr, command.CommandPtr);
			if (newCommandPtr == IntPtr.Zero)
				return false;

			command = new Command(newCommandPtr);
			return true;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerErase", CallingConvention = CallingConvention.Cdecl)]
		public static extern void _Erase(IntPtr commandHandlerPtr, string name);
		public void Erase(string name)
		{
			_Erase(CommandHandlerPtr, name);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerClear", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Clear(IntPtr commandHandlerPtr);
		public void Clear()
		{
			_Clear(CommandHandlerPtr);
		}
	}
}