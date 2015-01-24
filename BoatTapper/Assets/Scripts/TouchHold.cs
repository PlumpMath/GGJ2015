using UnityEngine;
using System;
using System.Collections;
using Extensions;
using Handlers;

public class TouchHold : MonoBehaviour 
{
	private TouchHandler m_touchHandler;

	private void Start () 
	{
		m_touchHandler = TouchHandler.Instance;

		// setup listener
		m_touchHandler.OnTouchBegun += this.OnTouchBegun;
	}

	private void OnTouchBegun (Touch p_touch)
	{
		/*
		float distance = Vector3.Distance (VWTouch,VWtarget);
		if (distance <= m_triggerDistance && memoryball.TouchId == -1) {
			memoryball.TouchId = p_touch.fingerId;
			break;
		}
		*/

		float distance = Vector2.Distance(p_touch.position, this.transform.position);
		this.Log("TouchHold::OnTouchBegun", "TouchId:{0} Distance:{1}", p_touch);
	}
}
