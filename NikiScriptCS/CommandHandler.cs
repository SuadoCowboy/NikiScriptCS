using System.Runtime.InteropServices;

public static partial class NikiScript
{
	public class CommandHandler
	{
		public IntPtr CommandHandlerPtr { get; private set; }

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerNew", CallingConvention =CallingConvention.Cdecl)]
		private static extern IntPtr _New();

		public CommandHandler() {
			CommandHandlerPtr = _New();
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_CommandHandlerNew", CallingConvention =CallingConvention.Cdecl)]
		private static extern IntPtr _Delete(IntPtr commandPtr);

		~CommandHandler() {
			_Delete(CommandHandlerPtr);
		}
	}
}