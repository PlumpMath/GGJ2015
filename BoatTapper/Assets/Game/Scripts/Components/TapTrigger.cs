using UnityEngine;
using System.Collections;

public class TapTrigger : MonoBehaviour
{
	[SerializeField]
	private TapType m_type;
	public Signal OnTriggerAction = new Signal(typeof(TapTrigger));
	
	private void OnClicked ()
	{
		OnTriggerAction.Invoke(this);
	}
	
	public TapType TapType 
	{
		get { return m_type; }
		private set { m_type = value; }
	}
}