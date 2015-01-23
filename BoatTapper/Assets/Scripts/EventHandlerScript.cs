using UnityEngine;
using System.Collections;

public class EventHandlerScript : MonoBehaviour {


	public void disAbleButton(GameObject obj)
	{
		obj.collider.enabled = false;

	}

}
