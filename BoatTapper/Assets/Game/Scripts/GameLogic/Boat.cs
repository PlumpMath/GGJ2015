using UnityEngine;
using System;
using System.Collections;

public enum Mass : int
{
	Left	= 0,
	Right	= 1,
}

public class Boat : MonoBehaviour 
{
	public static readonly float CURRENT_MASS = 2.0f;
	public static readonly float UNIT_MASS = 0.5f;
	public static readonly float MASS_RATE = 0.75f;

	[SerializeField]
	private Rigidbody2D[] m_mass;
	private float[] m_massTarget;

	private void Awake ()
	{
		m_massTarget = new float[2];
		m_massTarget[(int)Mass.Left] = Boat.CURRENT_MASS;
		m_massTarget[(int)Mass.Right] = Boat.CURRENT_MASS;
	}

	private void LateUpdate ()
	{
		this.UpdateMass(Mass.Left);
		this.UpdateMass(Mass.Right);
	}

	private void UpdateMass (Mass p_mass)
	{
		m_mass[(int)p_mass].mass = Mathf.Lerp(m_mass[(int)p_mass].mass, m_massTarget[(int)p_mass], Time.deltaTime * MASS_RATE);
		m_mass[(int)p_mass].WakeUp();
	}

	public void AdjustMass (Mass p_mass)
	{
		m_massTarget[(int)p_mass] += UNIT_MASS;
	}
}
