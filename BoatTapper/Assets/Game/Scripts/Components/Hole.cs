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
	public Action<Hole> OnDestroy;

	[SerializeField]
	private TapType m_type;
	[SerializeField]
	private int m_numberOfTaps;

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

	private void OnTap ()
	{
		if (m_numberOfTaps <= 0) 
		{ 
			this.Destroy();
			return; 
		}

		m_numberOfTaps--;

		this.Log("GamePlay::OnTap", "taps:{0}", m_numberOfTaps);
	}

	private void Destroy ()
	{
		if (this.OnDestroy == null) { return; }
		this.OnDestroy(this);
	}
}
