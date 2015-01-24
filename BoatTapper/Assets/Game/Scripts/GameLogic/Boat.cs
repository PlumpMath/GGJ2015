using UnityEngine;
using System;
using System.Collections;

public enum Side : int
{
	Left	= 0,
	Right	= 1,
}

public class Boat : MonoBehaviour 
{
	[SerializeField] private float CURRENT_MASS = 2.0f;
	[SerializeField] private float UNIT_MASS = 0.05f;
	[SerializeField] private float MASS_RATE = 0.75f;

	[SerializeField] private Rigidbody2D[] m_mass;
	private float[] m_massTarget;

	private void Awake ()
	{
		m_massTarget = new float[2];
		m_massTarget[(int)Side.Left] = CURRENT_MASS;
		m_massTarget[(int)Side.Right] = CURRENT_MASS;
	}

	private void LateUpdate ()
	{
		this.UpdateMass(Side.Left);
		this.UpdateMass(Side.Right);
		transform.position = Vector3.Lerp( transform.position, new Vector3(0, transform.position.y, transform.position.z), Time.deltaTime);
	}

	private void UpdateMass (Side p_side)
	{
		m_mass[(int)p_side].mass = Mathf.Lerp(m_mass[(int)p_side].mass, m_massTarget[(int)p_side], Time.deltaTime * MASS_RATE);
		m_mass[(int)p_side].WakeUp();
	}

	public void AdjustMass (Side p_side, int p_damage_num)
	{
		m_massTarget[(int)p_side] = CURRENT_MASS + (UNIT_MASS*p_damage_num);
	}
}
