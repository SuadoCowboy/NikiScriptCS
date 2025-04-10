using System.Runtime.InteropServices;
using static NikiScript;

static void PrintHandler(IntPtr dataPtr, Level level, string message)
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

ProgramVariables variables = new();

variables.Add("name", new("John Doe", "User nickname", ProgramVariable.GetString, ProgramVariable.SetString));

variables.Add("penes", new("3penes", "2penes", ProgramVariable.GetString, ProgramVariable.SetString));

Print(Level.ECHO, $"{variables.Size()}\n");
foreach (string key in variables.Keys())
	Print(Level.ECHO, $"{key}\n");

ProgramVariable var = variables.Get("penes")?? throw new Exception("Failed to get variable 'penes'");

Print(Level.ECHO, $"Var \"penes\" info:\n- Description: {var.Description}\n- Value: {var.Get(IntPtr.Zero, var.Ptr)}\n");

var.Description = "7penesss";
var.Set(IntPtr.Zero, var.Ptr, "Suado Cockboy");

Print(Level.ECHO, $"Var \"penes\" info:\n- Description: {var.Description}\n- Value: {var.Get(IntPtr.Zero, var.Ptr)}\n");

variables.Delete();