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

		echoCommand.PrintAsDataTree();

		echoCommand.Name = "echo2";

		echoCommand.Description = "Echoes the arguments, but with a different name";
		
		ushort size = echoCommand.GetArgsDescriptionsSize();
		Print(Level.ECHO, $"{size-1} -> {echoCommand.GetArgDescription((ushort)(size-1))}\n");
		
		echoCommand.MinArgs = 10;
		echoCommand.MaxArgs = 69;

		echoCommand.PrintAsDataTree();
		echoCommand.Delete();
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