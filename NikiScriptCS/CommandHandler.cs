using System.Runtime.InteropServices;

public static partial class NikiScript
{
	public class CommandHandler
	{
		public IntPtr Ptr { get; private set; }

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerNew", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _New();

		public CommandHandler() {
			Ptr = _New();
		}

		public CommandHandler(IntPtr commandHandlerPtr) {
			Ptr = commandHandlerPtr;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerNew", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _Delete(IntPtr commandHandlerPtr);

		public void Delete()
		{
			if (Ptr == IntPtr.Zero)
				return;
			_Delete(Ptr);
			Ptr = IntPtr.Zero;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerSize", CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong _Size(IntPtr commandHandlerPtr);
		public ulong Size() { return _Size(Ptr); }

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerAllocKeys", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _AllocKeys(IntPtr commandHandlerPtr);
		public string[] Keys() => GetStringArrayFromConstCharArray(_AllocKeys(Ptr), (int)Size());

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerGet", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _Get(IntPtr commandHandlerPtr, string name);
		public Command? Get(string name)
		{
			IntPtr commandPtr = _Get(Ptr, name);
			if (commandPtr == IntPtr.Zero)
				return null;

			return new(commandPtr);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerAdd", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _Add(IntPtr commandHandlerPtr, IntPtr commandPtr);

		/// <summary>
		/// Adds command to the command handler and updates command variable to the new command pointer.
		/// </summary>
		/// <param name="command"></param>
		/// <returns>true if success; false if new command pointer == IntPtr.Zero</returns>
		public bool Move(ref Command command)
		{
			IntPtr newCommandPtr = _Add(Ptr, command.Ptr);
			if (newCommandPtr == IntPtr.Zero)
				return false;

			command.Delete();
			command = new Command(newCommandPtr);
			return true;
		}

		/// <summary>
		/// Adds command to the command handler. Command variable is not updated.
		/// </summary>
		/// <param name="command"></param>
		/// <returns>true if success; false if new program variable pointer == IntPtr.Zero</returns>
		public bool Add(Command command)
		{
			IntPtr newCommandPtr = _Add(Ptr, command.Ptr);
			if (newCommandPtr == IntPtr.Zero)
				return false;

			return true;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerErase", CallingConvention = CallingConvention.Cdecl)]
		public static extern void _Erase(IntPtr commandHandlerPtr, string name);
		public void Erase(string name)
		{
			_Erase(Ptr, name);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerClear", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Clear(IntPtr commandHandlerPtr);
		public void Clear()
		{
			_Clear(Ptr);
		}
	}
}