using UnityEditor;
using UnityEngine;
using System.Collections;

[InitializeOnLoad]
public static class S88Editor
{
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // Alias | Namespace
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // CONSTANTS
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public const string S88_ROOT = "Synergy88/";
    public const string S88_TOOL = "Tools/";
    public const string S88_DEBUG = "Debug/";

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // MENUS
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	[MenuItem(S88_ROOT + S88_TOOL, false)]
    public static void Tools ()
    {
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // DEBUG
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [MenuItem(S88_ROOT + S88_DEBUG + "Des", false)]
    public static void DebugDes ()
    {
        // Apply your debug settings here
    }

    [MenuItem(S88_ROOT + S88_DEBUG + "Camille", false)]
    public static void DebugCamille ()
    {
        // Apply your debug settings here
    }

	// cmd + shift + r
	[MenuItem(S88_ROOT + S88_DEBUG + "Aries/Run MainGame %#r", false)]
    public static void DebugRunMainGame ()
    {
        // Apply your debug settings here
		EditorApplication.SaveCurrentSceneIfUserWantsTo();
		EditorApplication.OpenScene("Assets/Game/Scenes/game_page.unity");
		// Toggle
		//EditorApplication.isPlaying = !EditorApplication.isPlaying;
		EditorApplication.isPlaying = true;
    }

	// cmd + shift + x
	[MenuItem(S88_ROOT + S88_DEBUG + "Aries/Clear LOGS %#x", false)]
	public static void DebugClearLogs ()
	{
		// clear console
		var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
		var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		clearMethod.Invoke(null,null);
	}

	// cmd + shift + d
	[MenuItem(S88_ROOT + S88_DEBUG + "Aries/Clear PREFS %#d", false)]
	public static void DebugClearPrefs ()
	{
		// clear prefs
		PlayerPrefs.DeleteAll();
	}

    [MenuItem(S88_ROOT + S88_DEBUG + "Xander", false)]
    public static void DebugXander ()
    {
        // Apply your debug settings here
    }

    [MenuItem(S88_ROOT + S88_DEBUG + "Anjo", false)]
    public static void DebugAnjo ()
    {
        // Apply your debug settings here
    }
}
