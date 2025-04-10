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

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerSize", CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong _Size(IntPtr commandHandlerPtr);
		public ulong Size() { return _Size(CommandHandlerPtr); }

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerFreeKeys", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _FreeKeys(IntPtr keysPtr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerAllocKeys", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _AllocKeys(IntPtr commandHandlerPtr);
		public string[] Keys()
		{
			// const char**
			IntPtr keysPtrPtr = _AllocKeys(CommandHandlerPtr);
			if (keysPtrPtr == IntPtr.Zero)
				return [];

			int nEntries = (int)_Size(CommandHandlerPtr); // The number of narrow strings in the array

			// const char*[]
			IntPtr[] keysPtrs = new IntPtr[nEntries];
			Marshal.Copy(keysPtrPtr, keysPtrs, 0, nEntries);

			string[] keys = new string[nEntries];

			int i = 0;
			foreach(IntPtr keyPtr in keysPtrs) {
				keys[i] = Marshal.PtrToStringAnsi(keyPtr) ?? string.Empty;
				++i;
			}

			_FreeKeys(keysPtrPtr);
			return keys;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerGet", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _Get(IntPtr commandHandlerPtr, string name);
		public Command? Get(string name)
		{
			IntPtr commandPtr = _Get(CommandHandlerPtr, name);
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
			IntPtr newCommandPtr = _Add(CommandHandlerPtr, command.CommandPtr);
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
		/// <returns></returns>
		public bool Add(Command command)
		{
			IntPtr newCommandPtr = _Add(CommandHandlerPtr, command.CommandPtr);
			if (newCommandPtr == IntPtr.Zero)
				return false;

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