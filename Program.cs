using NikiScript;

class Program
{
    static private readonly _Print.CallbackDelegate _callback = new _Print.CallbackDelegate(PrintHandler);

    static void Main()
    {
        // Pass context with GCHandle?
        _Print.SetPrintCallback(IntPtr.Zero, _callback);

        _Print.Print(_Print.Level.WARNING, "This is a warning from C#!\n");
        _Print.PrintUnknownCommand("echo_blazulite");

		for (_Print.Level i = _Print.Level.DEFAULT; i != _Print.Level.ERROR+1; ++i)
			_Print.Print(i, $"PrintLevel as string: {_Print.LevelToString(i)}\n");

		nint pToken = Token.NewToken((byte)Token.Type.END, "Test blazulover");

		_Print.Print(_Print.Level.ECHO, $"{Token.GetTokenType(pToken)}\n");
		_Print.Print(_Print.Level.ECHO, $"{Token.GetTokenValue(pToken)}\n");

		Token.DeleteToken(pToken);
	}

    static void PrintHandler(IntPtr pData, _Print.Level level, string message)
    {
		switch (level) {
		case _Print.Level.DEFAULT:
			Console.ForegroundColor = ConsoleColor.White;
			break;

		case _Print.Level.ECHO:
			Console.ForegroundColor = ConsoleColor.Blue;
			break;

		case _Print.Level.WARNING:
			Console.ForegroundColor = ConsoleColor.Yellow;
			break;

		case _Print.Level.ERROR:
			Console.ForegroundColor = ConsoleColor.Red;
			break;
		}

        Console.Write(message);
		Console.ResetColor();
    }
}