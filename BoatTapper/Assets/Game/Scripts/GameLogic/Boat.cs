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
	[SerializeField] private float CURRENT_MASS = 2.0f;
	[SerializeField] private float UNIT_MASS = 0.05f;
	[SerializeField] private float MASS_RATE = 0.75f;

	[SerializeField] private Rigidbody2D[] m_mass;
	private float[] m_massTarget;

	private void Awake ()
	{
		m_massTarget = new float[2];
		m_massTarget[(int)Mass.Left] = CURRENT_MASS;
		m_massTarget[(int)Mass.Right] = CURRENT_MASS;
	}

	private void LateUpdate ()
	{
		this.UpdateMass(Mass.Left);
		this.UpdateMass(Mass.Right);
		transform.position = new Vector2(0, transform.position.y);
	}

	private void UpdateMass (Mass p_mass)
	{
		m_mass[(int)p_mass].mass = Mathf.Lerp(m_mass[(int)p_mass].mass, m_massTarget[(int)p_mass], Time.deltaTime * MASS_RATE);
		m_mass[(int)p_mass].WakeUp();
	}

	public void AdjustMass (Mass p_mass, int p_holes_num)
	{
		m_massTarget[(int)p_mass] = CURRENT_MASS + (UNIT_MASS*p_holes_num);
	}
}
