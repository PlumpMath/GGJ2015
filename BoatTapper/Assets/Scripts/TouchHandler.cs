//#define DEBUG_TOUCHES

using UnityEngine;
using System.Collections;

namespace Handler
{
	using Extensions;
	using TouchEvent = System.Action<UnityEngine.Touch>;

	public class TouchHandler : MonoBehaviour 
	{
		// events
		public event TouchEvent OnTouchBegun;
		public event TouchEvent OnTouchMoved;
		public event TouchEvent OnTouchEnded;
		public event TouchEvent OnTouchCancelled;

		// fields
		[SerializeField]
		private int m_touchLimit;
		[SerializeField]
		private TextMesh m_textMesh;

		private void Awake ()
		{
			// assert
			this.Assert<TouchEvent>(this.OnTouchBegun, "OnTouchBegun event is null.");
			this.Assert<TouchEvent>(this.OnTouchMoved, "OnTouchMoved event is null.");
			this.Assert<TouchEvent>(this.OnTouchEnded, "OnTouchEnded event is null.");
			this.Assert<TouchEvent>(this.OnTouchCancelled, "OnTouchCancelled event is null.");
			this.Assert<TextMesh>(m_textMesh, "TextMesh must not be null");

			// enable multitouch
			//Input.simulateMouseWithTouches = false;
			Input.multiTouchEnabled = true;
		}

		// Update is called once per frame
		private void Update ()
		{
			string touches = string.Empty;

			#if DEBUG_TOUCHES
			if (!Input.GetMouseButtonDown(0)) { return; }
			for (int i = 0; i < 1; i++) 
			#else
			for (int i = 0; i < Input.touches.Length; i++) 
			#endif
			{
				#if DEBUG_TOUCHES
				Touch touch = Input.GetTouch(i);
				#else
				Touch touch = Input.touches[i];
				#endif
				
				this.Log("TouchHandler::Update", "id:{0} phase:{1}", touch.fingerId, touch.phase);
				touches += "1 ";

				switch(touch.phase) 
				{
				case TouchPhase.Began:
					IsTouchTriggeredTarget(touch);
					break;
				case TouchPhase.Moved:
					MoveById(touch);
					break;
				case TouchPhase.Ended:
					RemovedId(touch);
					break;
				}
			}

			m_textMesh.text = touches;
		}

		public int TouchLimit 
		{
			set { m_touchLimit = value; } 
			get { return m_touchLimit; }
		}

		private void IsTouchTriggeredTarget (Touch p_touch) 
		{
			for (int i = 0; i < TouchLimit; i++) 
			{
				if (this.OnTouchBegun == null) { break; }
				this.OnTouchBegun(p_touch);
			}
		}

		private void MoveById (Touch p_touch) 
		{
			for (int i = 0; i < TouchLimit; i++) 
			{
				if (this.OnTouchMoved == null) { break; }
				this.OnTouchMoved(p_touch);
			}
		}
		private void RemovedId (Touch p_touch) 
		{
			for (int i = 0; i < TouchLimit; i++) 
			{
				if (this.OnTouchEnded == null) { break; }
				this.OnTouchEnded(p_touch);
			}
		}

		public Vector3 GetWorldPointToViewportPoint (Vector3 p_worldPoint)
		{
			return Camera.main.camera.WorldToViewportPoint(p_worldPoint);
		}

		public Vector3 GetScreenPointToViewportPoint (Vector2 p_screenPosition)
		{
			return Camera.main.camera.ScreenToViewportPoint(p_screenPosition);
		} 

		public Vector3 GetScreenPointToWorldPoint (Vector2 p_screenPosition)
		{
			return Camera.main.camera.ScreenToWorldPoint(new Vector3(p_screenPosition.x, p_screenPosition.y, 5));
		} 

		public Vector3 GetViewportPointToWorldPoint (Vector2 p_screenPosition)
		{
			return Camera.main.camera.ViewportToWorldPoint(new Vector3(p_screenPosition.x, p_screenPosition.y, 5));
		}
	}
}
