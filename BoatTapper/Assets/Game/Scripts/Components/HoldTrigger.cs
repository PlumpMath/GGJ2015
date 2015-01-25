using UnityEngine;
using System.Collections;

public enum ButtonEvent {
	BUTTON_DOWN, BUTTON_UP, NONE
}

public class HoldTrigger : MonoBehaviour {

	[SerializeField]
	private TapType m_type;
	private ButtonEvent m_buttonEvent = ButtonEvent.NONE;
	public Signal OnTriggerAction = new Signal(typeof(HoldTrigger));

	private void OnButtonDown()
	{
		m_buttonEvent = ButtonEvent.BUTTON_DOWN;
		OnTriggerAction.Invoke(this);
	}

	private void OnButtonUp()
	{
		m_buttonEvent = ButtonEvent.BUTTON_UP;
		OnTriggerAction.Invoke(this);
	}

	public TapType TapType 
	{
		get { return m_type; }
		private set { m_type = value; }
	}

	public ButtonEvent ButtonEvent 
	{
		get { return m_buttonEvent; }
		private set { m_buttonEvent = value; }
	}

	public void OnTapTypeChanged(TapType p_type)
	{
		this.TapType = p_type;
	}
}
