using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class TapTrigger : MonoBehaviour
{
	[SerializeField]
	private TapType m_type;
	public Signal OnTriggerAction = new Signal(typeof(TapTrigger));
	
	private void OnClicked ()
	{
		this.Log("TapTrigger::OnClicked", "TK2D Trigger Type:{0}", m_type);
		this.OnTriggerAction.Invoke(this);
	}
	
	public TapType TapType 
	{
		get { return m_type; }
		private set { m_type = value; }
	}

	public void OnTapTypeChanged(TapType p_type)
	{
		this.TapType = p_type;
	}
}