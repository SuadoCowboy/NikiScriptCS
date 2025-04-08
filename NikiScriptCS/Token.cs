using System.Runtime.InteropServices;

public static partial class NikiScript
{
	internal static class Token
	{
		public enum Type : byte {
			NONE = 0,
			IDENTIFIER = 1, ///< If it's the first token after an EOS, END or NONE. Should be either a variable or command
			ARGUMENT = 2, ///< If lexer could not convert to integer and an identifier was already caught up
			EOS = 4, ///< End Of Statement
			END = 8 ///< End of input data
		};

		[DllImport("libNikiScript.dll", EntryPoint="ns_newToken", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr NewToken(byte type, string value);

		[DllImport("libNikiScript.dll", EntryPoint="ns_deleteToken", CallingConvention = CallingConvention.Cdecl)]
		public static extern void DeleteToken(IntPtr token);

		[DllImport("libNikiScript.dll", EntryPoint="ns_getTokenValue", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetTokenValue(IntPtr token);

		public static string GetTokenValue(IntPtr token) {
			return Marshal.PtrToStringAnsi(_GetTokenValue(token)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint="ns_setTokenValue", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetTokenValue(IntPtr token, string value);

		[DllImport("libNikiScript.dll", EntryPoint="ns_getTokenType", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte GetTokenType(IntPtr token);

		[DllImport("libNikiScript.dll", EntryPoint="ns_setTokenType", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetTokenType(IntPtr token, byte type);
	}
}