using UnityEngine;
using System;
using System.Collections;
using Extensions;

public enum TapType
{
	Patch,
	Hammer,
	Move,
	Pail,
	Stitch,
}

public class Hazard : MonoBehaviour 
{
	public Action<TapType> OnTapEvent;
	public Action<TapType> OnButtonDownEvent;
	public Action<TapType> OnButtonUpEvent;

	public Action<Hazard> OnDestroy;

	[SerializeField]
	private int m_numberOfTaps;

	[SerializeField]
	private TapType m_tapType;

	[SerializeField]
	private TapType m_holdType;

	[SerializeField]
	private bool m_isActive = true;

	private void Awake ()
	{
	}

	private void Start ()
	{
	}

	public int Taps 
	{
		get { return m_numberOfTaps; }
		set { m_numberOfTaps = value; }
	}

	public TapType TapType
	{
		get { return m_tapType; }
	}

	public TapType HoldType
	{
		get { return m_holdType; }
	}

	public bool ActiveHazard
	{
		get { return m_isActive; }
	}

	public Side SideLocation
	{
		get
		{
			if (transform.localPosition.x < 0)
			{
				return Side.Left;
			}
			else
			{
				return Side.Right;
			}
		}
	}

	[Signal]
	private void OnTriggerTap (TapTrigger p_tap)
	{
		if(p_tap.TapType == m_tapType)
		{
			if (m_numberOfTaps <= 0) 
			{ 
				this.Destroy();
				return; 
			}
			
			m_numberOfTaps--;
			
			if (this.OnTapEvent != null)
			{
				this.OnTapEvent(p_tap.TapType);
			}
		}
	}

	[Signal]
	private void OnTriggerHold (HoldTrigger p_tap)
	{
		if(p_tap.TapType == m_holdType)
		{
			switch(p_tap.ButtonEvent)
			{
				case ButtonEvent.BUTTON_DOWN:
					if (this.OnButtonDownEvent != null)
					{
						m_isActive = false;
						this.OnButtonDownEvent(p_tap.TapType);
					}
					break;
				case ButtonEvent.BUTTON_UP:
					if (this.OnButtonUpEvent != null)
					{
						m_isActive = true;
						this.OnButtonUpEvent(p_tap.TapType);
					}
					break;
				default:
					break;
			}
		}
	}
	
	private void Destroy ()
	{
		if (this.OnDestroy == null) { return; }
		this.OnDestroy(this);
	}
}
