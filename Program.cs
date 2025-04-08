using static NikiScript;

class Program
{
	// static private readonly _Print.CallbackDelegate _callback = new _Print.CallbackDelegate(PrintHandler);

	static void Main()
	{
		// Pass context with GCHandle?
		SetPrintCallback(IntPtr.Zero, new CallbackDelegate(PrintHandler));

		Print(Level.WARNING, "This is a warning from C#!\n");
		PrintUnknownCommand("echo_blazulite");

		for (Level i = Level.DEFAULT; i != Level.ERROR+1; ++i)
			Print(i, $"PrintLevel as string: {LevelToString(i)}\n");
	}

	static void PrintHandler(IntPtr pData, Level level, string message)
	{
		switch (level) {
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
}