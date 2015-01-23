using UnityEngine;
using System.Collections;

namespace Extensions 
{
	public static class Ext
	{
		public static void Assert<T> (this MonoBehaviour p_self, T p_item, string p_message)
		{
			if (p_item == null)
			{
				p_self.LogError("ERROR", "Messae:" + p_message);
			}
		}
		
		public static void Assert<T> (this T p_self, bool p_isSatisfied, string p_message)
		{
			if (!p_isSatisfied)
			{
				p_self.LogError("ERROR", "Messae:" + p_message);
			}
		}

		public static void Log<T> (this T p_self, string p_header, string format, params object[] p_args)
		{
			p_self.Log<T>(p_header, string.Format(format, p_args));
		}

		public static void Log<T> (this T p_self, string p_header, string p_logs)
		{
			Debug.Log(string.Format("<color=yellow>[{0}]</color> {1}\n", p_header, p_logs));
		}

		public static void LogError<T> (this T p_self, string p_header, string format, params object[] p_args)
		{
			p_self.LogError<T>(p_header, string.Format(format, p_args));
		}
		
		public static void LogError<T> (this T p_self, string p_header, string p_logs)
		{
			Debug.Log(string.Format("<color=red>[{0}]</color> {1}\n", p_header, p_logs));
		}
	}
}