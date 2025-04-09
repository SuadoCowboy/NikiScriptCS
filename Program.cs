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
		Command echoCommand = new("echo", 1, 1, new Command.CallbackDelegate(Echo), "Echoes the arguments", ["s[text]", "text to print out"]);
	
		CommandHandler commands = new();
		commands.Add(ref echoCommand);

		commands.Erase("echo");

		commands.Clear();
		commands.Delete();
	}

	static void PrintHandler(IntPtr dataPtr, Level level, string message)
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