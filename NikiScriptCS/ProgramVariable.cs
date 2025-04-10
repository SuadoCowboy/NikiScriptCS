using System.Runtime.InteropServices;

public static partial class NikiScript {
	public class ProgramVariable {
		public static string GetString(IntPtr CtxPtr, IntPtr varPtr)
		{
			ProgramVariable var = new(varPtr);

			return Marshal.PtrToStringAnsi(var.ValuePtr)?? "";
		}

		public static void SetString(IntPtr CtxPtr, IntPtr varPtr, string value)
		{
			ProgramVariable var = new(varPtr);

			byte[] bytes = System.Text.Encoding.ASCII.GetBytes(value + '\0');
        	Marshal.Copy(bytes, 0, var.ValuePtr, bytes.Length);
		}


		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate string GetValueDelegate(IntPtr ctxPtr, IntPtr varPtr);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SetValueDelegate(IntPtr ctxPtr, IntPtr varPtr, string value);

		/// <summary>
		/// In case the pointer was allocated by the C# code, we need to free it when the variable is deleted.
		/// </summary>
		private IntPtr AllocatedValuePtr = IntPtr.Zero;

		public IntPtr Ptr { get; private set; }
		public IntPtr ValuePtr {
			get => GetValue(Ptr);
			set => SetValue(Ptr, value);
		}

		public string Description {
			get => GetDescription();
			set => SetDescription(Ptr, value);
		}

		public GetValueDelegate Get {
			get => GetGetter(Ptr);
			set => SetGetter(Ptr, value);
		}

		public SetValueDelegate Set {
			get => GetSetter(Ptr);
			set => SetSetter(Ptr, value);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableNew", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _New(IntPtr valuePtr, string description, GetValueDelegate get, SetValueDelegate set);

		public ProgramVariable(IntPtr valuePtr, string description, GetValueDelegate get, SetValueDelegate set)
		{
			Ptr = _New(valuePtr, description, get, set);
		}

		// TODO: how will this work in C#?
		public ProgramVariable(string valueToAlloc, string description, GetValueDelegate get, SetValueDelegate set)
		{
			IntPtr allocatedValuePtr = Marshal.StringToHGlobalAnsi(valueToAlloc);
			if (allocatedValuePtr == IntPtr.Zero)
				throw new Exception("Failed to allocate memory for value");

			Ptr = _New(allocatedValuePtr, description, get, set);
		}

		public ProgramVariable(IntPtr ptr)
		{
			Ptr = ptr;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableDelete", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Delete(IntPtr ptr);

		public void Delete()
		{
			if (Ptr != IntPtr.Zero) {
				_Delete(Ptr);
				Ptr = IntPtr.Zero;
			}

			if (AllocatedValuePtr != IntPtr.Zero) {
				Marshal.FreeHGlobal(AllocatedValuePtr);
				AllocatedValuePtr = IntPtr.Zero;
			}
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableGetValue", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr GetValue(IntPtr ptr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableSetValue", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetValue(IntPtr ptr, IntPtr valuePtr);


		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableGetDescription", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _GetDescription(IntPtr ptr);

		private string GetDescription()
		{
			IntPtr descriptionPtr = _GetDescription(Ptr);
			if (descriptionPtr == IntPtr.Zero)
				return string.Empty;

			return Marshal.PtrToStringAnsi(descriptionPtr) ?? string.Empty;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableSetDescription", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetDescription(IntPtr ptr, string description);


		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableGetGetter", CallingConvention = CallingConvention.Cdecl)]
		private static extern GetValueDelegate GetGetter(IntPtr ptr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableSetGetter", CallingConvention = CallingConvention.Cdecl)]
		private static extern GetValueDelegate SetGetter(IntPtr ptr, GetValueDelegate get);


		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableGetSetter", CallingConvention = CallingConvention.Cdecl)]
		private static extern SetValueDelegate GetSetter(IntPtr ptr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariableSetSetter", CallingConvention = CallingConvention.Cdecl)]
		private static extern SetValueDelegate SetSetter(IntPtr ptr, SetValueDelegate set);
	}

	public class ProgramVariables {
		public IntPtr Ptr { get; private set; }

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariablesNew", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _New();

		public ProgramVariables() => Ptr = _New();
		public ProgramVariables(IntPtr ptr) => Ptr = ptr;

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariablesDelete", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Delete(IntPtr ptr);

		public void Delete()
		{
			if (Ptr == IntPtr.Zero)
				return;
			_Delete(Ptr);
			Ptr = IntPtr.Zero;
		}


		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariablesAdd", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _Add(IntPtr Ptr, string name, IntPtr variablePtr);

		/// <summary>
		/// Adds program variable to this object and updates the program variable pointer to the new pointer.
		/// </summary>
		/// <param name="variable"></param>
		/// <returns>true if success; false if new program variable pointer == IntPtr.Zero</returns>
		public bool Move(string name, ref ProgramVariable variable)
		{
			IntPtr newVariablePointer = _Add(Ptr, name, variable.Ptr);
			if (newVariablePointer == IntPtr.Zero)
				return false;

			variable.Delete();
			variable = new ProgramVariable(newVariablePointer);
			return true;
		}

		/// <summary>
		/// Adds command to the command handler. Command variable is not updated.
		/// </summary>
		/// <param name="command"></param>
		/// <summary>
		/// Adds program variable to this object and updates the program variable pointer to the new pointer.
		/// </summary>
		/// <param name="variable"></param>
		/// <returns>true if success; false if new program variable pointer == IntPtr.Zero</returns>
		public bool Add(string name, ProgramVariable variable)
		{
			IntPtr newCommandPtr = _Add(Ptr, name, variable.Ptr);
			if (newCommandPtr == IntPtr.Zero)
				return false;

			return true;
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariablesGet", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _Get(IntPtr Ptr, string name);
		public ProgramVariable? Get(string name)
		{
			IntPtr variablePtr = _Get(Ptr, name);
			if (variablePtr == IntPtr.Zero)
				return null;

			return new ProgramVariable(variablePtr);
		}

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariablesErase", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Erase(IntPtr Ptr, string name);
		public void Erase(string name) => _Erase(Ptr, name);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariablesClear", CallingConvention = CallingConvention.Cdecl)]
		private static extern void _Clear(IntPtr Ptr);
		public void Clear() => _Clear(Ptr);

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariablesAllocKeys", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr _AllocKeys(IntPtr ptr);
		public string[] Keys() => GetStringArrayFromConstCharArray(_AllocKeys(Ptr), (int)Size());

		[DllImport("libNikiScript.dll", EntryPoint = "ns_ProgramVariablesSize", CallingConvention = CallingConvention.Cdecl)]
		private static extern ulong _Size(IntPtr Ptr);
		public ulong Size() => _Size(Ptr);
	}
}