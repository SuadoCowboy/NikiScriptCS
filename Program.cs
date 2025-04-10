using System.Runtime.InteropServices;
using static NikiScript;

void PrintHandler(IntPtr dataPtr, Level level, string message)
{
	switch (level)
	{
		case Level.DEFAULT:
			Console.ForegroundColor = ConsoleColor.White;
			break;
		case Level.ECHO:
			Console.ForegroundColor = ConsoleColor.Blue;
			break;
		case Level.WARNING:
			Console.ForegroundColor = ConsoleColor.Yellow;
			break;
		case Level.ERROR:
			Console.ForegroundColor = ConsoleColor.Red;
			break;
	}

	Console.Write(message);
	Console.ResetColor();
}

SetPrintCallback(IntPtr.Zero, new CallbackDelegate(PrintHandler));

IntPtr allocatedNamePtr = Marshal.StringToHGlobalAnsi("John Doe");
if (allocatedNamePtr == IntPtr.Zero) {
	Print(Level.ERROR, "Failed to allocate memory for name\n");
	return;
}

ProgramVariable var = new(allocatedNamePtr, "a simple test", ProgramVariable.GetString, ProgramVariable.SetString);

Print(Level.ECHO, $"Var Info:\nDescription: {var.Description}\nValue: {var.Get(IntPtr.Zero, var.Ptr)}\n");

var.Description = "2penes";
var.Set(IntPtr.Zero, var.Ptr, "Suado Cockboy");

Print(Level.ECHO, $"Var Info:\nDescription: {var.Description}\nValue: {var.Get(IntPtr.Zero, var.Ptr)}\n");

var.Delete();
Marshal.FreeHGlobal(allocatedNamePtr);