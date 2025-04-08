class Program
{
    static private readonly NikiScript.PrintCallbackDelegate _callback = new NikiScript.PrintCallbackDelegate(PrintHandler);

    static void Main()
    {
        // Optional: Pass context with GCHandle
        NikiScript.SetPrintCallback(IntPtr.Zero, _callback);

        // Test it!
        NikiScript.Print(PrintLevel.WARNING, "This is a warning from C#!\n");
        NikiScript.PrintUnknownCommand("echo_blazulite");

		for (PrintLevel i = PrintLevel.DEFAULT; i != PrintLevel.ERROR+1; ++i) {
			NikiScript.Print(i, $"PrintLevel as string: {NikiScript.PrintLevelToString(i)}\n");
		}
	}

    static void PrintHandler(IntPtr pData, PrintLevel level, string message)
    {
		switch (level) {
		case PrintLevel.DEFAULT:
			Console.ForegroundColor = ConsoleColor.White;
			break;

		case PrintLevel.ECHO:
			Console.ForegroundColor = ConsoleColor.Blue;
			break;

		case PrintLevel.WARNING:
			Console.ForegroundColor = ConsoleColor.Yellow;
			break;

		case PrintLevel.ERROR:
			Console.ForegroundColor = ConsoleColor.Red;
			break;
		}

        Console.Write(message);
		Console.ResetColor();
    }
}