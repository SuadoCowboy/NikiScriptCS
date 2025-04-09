using static NikiScript;

class Program
{
	static void Echo(IntPtr pCtx)
	{
		Print(Level.ECHO, "For now we can't use ctx :clown:\n");
	}

	static void Echo2(IntPtr pCtx)
	{
		Print(Level.ECHO, "penes gaming\n");
	}

	static void Main()
	{
		SetPrintCallback(IntPtr.Zero, new CallbackDelegate(PrintHandler));

		Command echoCommand = new("echo", 1, 1, Echo, "Echoes the arguments", ["s[text]", "text to print out"]);
		Command echoCommand2 = new($"{echoCommand.Name}2", echoCommand.MinArgs, echoCommand.MaxArgs, echoCommand.Callback, echoCommand.Description, echoCommand.ArgsDescriptions);
		Command echoCommand3 = new("echo", 1, 1, Echo2, "Echoes again", ["s[text2]", "text to print out again lol"]);

		CommandHandler commands = new();

		commands.Add(ref echoCommand);
		Command echoCommand4 = new(echoCommand.CommandPtr); // IT HAS TO BE DEFINED AFTER THIS BECAUSE THE DELETE FUNCTION IS CALLED AND A NEW POINTER IS SET

		commands.Add(ref echoCommand2);

		if (!commands.Add(ref echoCommand3))
			Print(Level.ERROR, $"could not create echoCommand3 because {echoCommand4.Name} command already exists\n");;

		if (!commands.Add(ref echoCommand4))
			Print(Level.ERROR, $"could not create echoCommand4 because {echoCommand4.Name} command already exists\n");

		Print(Level.ECHO, commands.GetCommandsNames()+'\n');
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