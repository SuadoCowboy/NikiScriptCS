using static NikiScript;

class Program
{
	static void Echo(IntPtr pCtx)
	{
		Print(Level.ECHO, "For now we can't use ctx :clown:\n");
	}

	static void Main()
	{
		SetPrintCallback(IntPtr.Zero, new CallbackDelegate(PrintHandler));

		Print(Level.WARNING, "This is a warning from C#!\n");
		PrintUnknownCommand("echo_blazulite");

		for (Level i = Level.DEFAULT; i != Level.ERROR+1; ++i)
			Print(i, $"PrintLevel as string: {LevelToString(i)}\n");

		Command echoCommand = new("echo", 1, 1, new Command.CallbackDelegate(Echo), "Echoes the arguments", ["s[text]", "text to print out"]);

		string echoParams = echoCommand.GetArgumentsNames();
		Print(Level.ECHO, $"Args = {echoParams}\n");

		echoCommand.Callback(IntPtr.Zero);

		echoCommand.Delete();
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