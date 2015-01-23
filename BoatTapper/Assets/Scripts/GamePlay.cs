using UnityEngine;
using System.Collections;
using Handlers;
using Extensions;
using TouchEvent = System.Action<UnityEngine.Touch>;

namespace Handlers
{
	public class GamePlay : MonoBehaviour 
	{
		[SerializeField]
		private TouchHandler m_touchHandler;
		// templates
		[SerializeField]
		private GameObject m_hole;
		
		private void Start () 
		{
			this.Assert<TouchHandler>(m_touchHandler, "ERROR: m_touchHandler is null.");

			// setup listener
			m_touchHandler.OnTouchBegun += this.OnTouchBegun;
		}

		private void OnTouchBegun (Touch p_touch)
		{
		}
	}
}