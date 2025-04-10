public static partial class NikiScript
{
	public static class Utils
	{
		public static IntPtr StringToPtr(string content) => new(Convert.ToInt32(content, 16));
	}
}