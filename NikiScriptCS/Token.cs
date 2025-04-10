using System.Runtime.InteropServices;

public static partial class NikiScript
{
	public class Token
	{
		public enum Type : byte {
			NONE = 0,
			IDENTIFIER = 1, ///< If it's the first token after an EOS, END or NONE. Should be either a variable or command
			ARGUMENT = 2, ///< If lexer could not convert to integer and an identifier was already caught up
			EOS = 4, ///< End Of Statement
			END = 8 ///< End of input data
		};

		public IntPtr Ptr { get; private set; }

		public string Value {
			get => GetValue();
			set => SetValue(Ptr, value);
		}

		[DllImport("libNikiScript.dll", EntryPoint="ns_TokenNew", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _New(byte type, string value);

		public Token(byte type, string value) {
			Ptr = _New(type, value);
		}

		[DllImport("libNikiScript.dll", EntryPoint="ns_TokenDelete", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Delete(IntPtr tokenPtr);
		public void Delete() {
			if (Ptr == IntPtr.Zero)
				return;

			_Delete(Ptr);
			Ptr = IntPtr.Zero;
		}

		[DllImport("libNikiScript.dll", EntryPoint="ns_TokenGetValue", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetValue(IntPtr tokenPtr);

		private string GetValue() {
			return Marshal.PtrToStringAnsi(_GetValue(Ptr)) ?? "";
		}

		[DllImport("libNikiScript.dll", EntryPoint="ns_TokenSetValue", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SetValue(IntPtr tokenPtr, string value);

		[DllImport("libNikiScript.dll", EntryPoint="ns_TokenGetType", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte  TokenGetType(IntPtr tokenPtr);

		[DllImport("libNikiScript.dll", EntryPoint="ns_TokenSetType", CallingConvention = CallingConvention.Cdecl)]
		public static extern void  TokenSetType(IntPtr tokenPtr, byte type);
	}
}