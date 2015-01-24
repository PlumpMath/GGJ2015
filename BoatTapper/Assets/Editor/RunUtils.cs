using System;

using UnityEngine;
using UnityEditor;

/**
 * Contains utilities to run certain parts of the game such as main game or editor.
 */
public class RunUtils {
	
	[MenuItem("Game/Run Game #&z")]
	public static void Run() {
		EditorApplication.SaveCurrentSceneIfUserWantsTo();
		EditorApplication.OpenScene("Assets/Game/Scenes/load-singletons.unity");
		EditorApplication.isPlaying = true;
	}
	
}
