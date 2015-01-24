using UnityEngine;
using System;
using System.Collections;
using Extensions;

public enum TapType
{
	Patch,
	Hammer,
	Move,
	Pale,
	Stitch,
}

public class Hole : MonoBehaviour 
{
	public Action<TapType> OnTapEvent;
	public Action<Hole> OnDestroy;

	[SerializeField]
	private int m_numberOfTaps;
	//[SerializeField]
	//private TapType m_type;

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

	[Signal]
	private void OnTriggerTap (TapTrigger p_tap)
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
	
	private void Destroy ()
	{
		if (this.OnDestroy == null) { return; }
		this.OnDestroy(this);
	}
}
