//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// DEBUG
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
#define ENABLE_DEBUG_LOGS

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace S88Extension
{
    /// <summary>
    /// Debug COLORS
    /// </summary>Pla
    public enum DColor
    {
        GREEN,
        YELLOW,
        RED,
        BLUE,
    };

    public enum Dev
    {
        Des,
        Camille,
        Xander,
        Aries,
        Anjo
    }

	public enum Tags
	{
		Invalid				= 0x0,
		Bets				= 0x1 << 0,
		IsWaiting			= 0x1 << 1,
		PhotonNetwork		= 0x1 << 2,
		PhotonMessages		= 0x1 << 3,

		XanderLogs			= 0x1 << 4,
		AriesLogs			= 0x1 << 5,
		AnjoLogs			= 0x1 << 6,
		CarlosLogs			= 0x1 << 7,
		DesLogs				= 0x1 << 8,
		CamilleLogs			= 0x1 << 9,
		MarsLogs			= 0x1 << 10,

		ActionManager 		= 0x1 << 11,
		PokerAction 		= 0x1 << 12,

		Max					= 0x1 << 13,
		ALL					= ~Invalid >> 1,
		WITH_OUT_ARIES_LOGS	= ALL & ~(AriesLogs | ActionManager | PokerAction),
	};

	public static class Extensions
    {
		#region LOG TAGS
#if ENABLE_DEBUG_LOGS
		private static Tags tags = Tags.AriesLogs | Tags.ActionManager | Tags.PokerAction;
#elif
		private static Tags tags = Tags.Invalid;
#endif
		#endregion

		#region USER LOGS
		public static void XanderLog<T> (this T p_self, string p_format, params object[] p_args)
		{
			p_self.Log<T>(Tags.XanderLogs, "Xander", string.Format(p_format, p_args));
		}
		
		public static void AriesLog<T> (this T p_self, string p_format, params object[] p_args)
		{
			p_self.Log<T>(Tags.AriesLogs, "Aries", string.Format(p_format, p_args));
		}
		
		public static void AnjoLog<T> (this T p_self, string p_format, params object[] p_args)
		{
			p_self.Log<T>(Tags.AnjoLogs, "Anjo", string.Format(p_format, p_args));
		}
		
		public static void CarlosLog<T> (this T p_self, string p_format, params object[] p_args)
		{
			p_self.Log<T>(Tags.CarlosLogs, "Carlos", string.Format(p_format, p_args));
		}
		
		public static void DesLog<T> (this T p_self, string p_format, params object[] p_args)
		{
			p_self.Log<T>(Tags.DesLogs, "Des", string.Format(p_format, p_args));
		}
		
		public static void CamilleLog<T> (this T p_self, string p_format, params object[] p_args)
		{
			p_self.Log<T>(Tags.CamilleLogs, "Camille", string.Format(p_format, p_args));
		}
		
		public static void MarsLog<T> (this T p_self, string p_format, params object[] p_args)
		{
			p_self.Log<T>(Tags.MarsLogs, "Mars", string.Format(p_format, p_args));
		}
		#endregion

        #region LOGS EXTENSION
        private static Dictionary<DColor, string> COLORS = new Dictionary<DColor, string>()
        {
            { DColor.GREEN,    "green" },
            { DColor.YELLOW,   "yellow" },
            { DColor.RED,      "red" },
            { DColor.BLUE,     "blue" },
        };
		
        public static void Log<T> (this T p_self, string p_header, string p_logs)
        {
			p_self.Log<T>(Tags.Invalid, p_header, p_logs);
        }

		public static void Log<T> (this T p_self, Tags p_tag, string p_header, string p_logs)
		{
			if (p_tag == Tags.Invalid) { return; }
			if (!tags.Has(p_tag)) { return; }
			Debug.Log(string.Format("<color=yellow>[{0}]</color> {1}\n", p_header, p_logs));
		}

        public static void LogInfo<T> (this T p_self, string p_header, string p_logs)
        {
			p_self.LogInfo<T>(Tags.Invalid, p_header, p_logs);
        }

		public static void LogInfo<T> (this T p_self, Tags p_tag, string p_header, string p_logs)
		{
			if (p_tag == Tags.Invalid) { return; }
			if (!tags.Has(p_tag)) { return; }
			Debug.Log(string.Format("<color=green>[POKER_INFO]</color> <color=yellow>[{0}]</color> {1}\n", p_header, p_logs));
		}

        public static void LogError<T> (this T p_self, string p_header, string p_logs)
        {
			p_self.LogError<T>(Tags.Invalid, p_header, p_logs);
        }

		public static void LogError<T> (this T p_self, Tags p_tag, string p_header, string p_logs)
		{
			if (p_tag == Tags.Invalid) { return; }
			if (!tags.Has(p_tag)) { return; }
			Debug.Log(string.Format("<color=red>[POKER_ERROR]</color> <color=yellow>[{0}]</color> {1}\n", p_header, p_logs));
		}
        #endregion

        #region INTEGER EXTENSION
        public static T Clamp<T> (this T p_val, T p_min, T p_max) where T : IComparable<T>
		{
			if (p_val.CompareTo(p_min) < 0) return p_min;
			else if(p_val.CompareTo(p_max) > 0) return p_max;
			else return p_val;
		}

		public static int ClampInt (this int p_val, int p_min, int p_max) 
		{
			if (p_val < p_min) return p_min;
			else if(p_val > p_max) return p_max;
			else return p_val;
		}

        public static uint ToUINT (this int p_val)
        {
            return (uint)p_val;
        }
        #endregion

        #region ENUM EXTENSION
        public static bool Has<T> (this Enum type, T value) 
        {
			try {
				return (((int)(object)type & (int)(object)value) == (int)(object)value);
			} 
			catch {
				return false;
			}
		}
		
		public static bool Is<T> (this Enum type, T value) 
        {
			try {
				return (int)(object)type == (int)(object)value;
			}
			catch {
				return false;
			}    
		}
		
		public static T Add<T> (this Enum type, T value) 
        {
			try {
				return (T)(object)(((int)(object)type | (int)(object)value));
			}
			catch(Exception ex) {
				throw new ArgumentException(
					string.Format(
					"Could not append value from enumerated type '{0}'.",
					typeof(T).Name
					), ex);
			}    
		}
		
		public static T Remove<T> (this Enum type, T value) 
        {
			try {
				return (T)(object)(((int)(object)type & ~(int)(object)value));
			}
			catch (Exception ex) {
				throw new ArgumentException(
					string.Format(
					"Could not remove value from enumerated type '{0}'.",
					typeof(T).Name
					), ex);
			}
        }
        #endregion

        #region VECTOR EXTENSION
        public static void SetX (this Transform p_trans, float p_x)
        {
            p_trans.position = new Vector3(p_x, p_trans.position.y, p_trans.position.z);
        }

        public static void SetY (this Transform p_trans, float p_y)
        {
            p_trans.position = new Vector3(p_trans.position.y, p_y, p_trans.position.z);
        }

        public static void SetZ (this Transform p_trans, float p_z)
        {
            p_trans.position = new Vector3(p_trans.position.x, p_trans.position.y, p_z);
        }
        #endregion

        #region DECK EXTENSION
        #endregion

        #region MONO EXTENSION
        public static void Assert<T> (this MonoBehaviour p_self, T p_item, string p_message)
        {
            if (p_item == null)
            {
                //Debug.LogError("Error! Message:" + p_message + "\n");
                p_self.LogError("ERROR", "Message: " + p_message);
            }
        }

        public static void Assert<T> (this T p_self, bool p_isSatisfied, string p_message)
        {
            if (!p_isSatisfied)
            {
                p_self.LogError("ERROR", "Message: " + p_message);
            }
        }
        #endregion
    }
}