using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Utility
{
    using S88Extension;
    
	public sealed class Utils 
	{
        public static readonly int BITS = 4;

        public static void Log (string p_log)
        {
            Debug.Log(String.Format("{0} {1}\n", "<color=yellow>[POKER]</color>", p_log));
        }

        public static void LogImp (string p_log)
        {
            Debug.Log(String.Format("{0} {1}\n", "<color=green>[POKER]</color>", p_log));
        }

        public static void LogWarn (string p_log)
        {
            Debug.Log(String.Format("{0} {1}\n", "<color=red>[ERROR]</color>", p_log));
        }

        public static byte[] StructToBytes (object p_struct)
        {
            int size = Marshal.SizeOf(p_struct);
            IntPtr buffer = Marshal.AllocHGlobal(size);

            try
            {
                Marshal.StructureToPtr(p_struct, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

        }

        // TODO: Use template here instead of using the 'Type'
        public static object BytesToStruct (
            byte[] p_bytes, 
            Type p_type, 
            int p_offset = 0
        ) {
            int size = Marshal.SizeOf(p_type);
            IntPtr buffer = Marshal.AllocHGlobal(size);

            try
            {
                Marshal.Copy(p_bytes, p_offset, buffer, size);
                return Marshal.PtrToStructure(buffer, p_type);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

		// TODO: Use for Big Chip Win
		public static void SetSpriteAlpha(tk2dSprite p_sprite, float p_alpha)
		{
			p_sprite.color = new Color(p_sprite.color.r,p_sprite.color.g, p_sprite.color.b, p_alpha);
			foreach(Transform child in p_sprite.transform)
			{
				tk2dSprite spriteChild = child.GetComponent<tk2dSprite>();
				if(spriteChild)
					SetSpriteAlpha(spriteChild, p_alpha);
			}
		}

		public static void SetSpriteColor(Transform p_tk2DComponent, Color p_color)
		{
			tk2dBaseSprite sprite = p_tk2DComponent.GetComponent<tk2dBaseSprite>();
			if(sprite)
				sprite.color = p_color;
			tk2dTextMesh textMesh = p_tk2DComponent.GetComponent<tk2dTextMesh>();
			if(textMesh)
				textMesh.color = p_color;

			foreach(Transform child in p_tk2DComponent)
			{
				if(child)
					SetSpriteColor(child, p_color);
			}
		}

		public static float GetSummation(float p_constant, float p_seriesLength)
		{
			return p_constant * ((p_seriesLength / 2f) * (1f + p_seriesLength));
		}
	}
}