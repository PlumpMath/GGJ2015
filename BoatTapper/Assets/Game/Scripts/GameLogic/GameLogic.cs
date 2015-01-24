using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class GameLogic : MonoBehaviour 
{
	public static readonly float EASY_INTERVAL = 10.0f;
	public static readonly float MEDIUM_INTERVAL = 7.0f;
	public static readonly float HARD_INTERVAL = 4.0f;

	// template
	[SerializeField]
	private Hole m_holeTemplate;
	[SerializeField]
	private Boat m_boat;

	[SerializeField]
	private List<Transform> m_holePositions;
	private List<Hole> m_holes;
	private float m_interval;

	private void Start ()
	{
		m_holes = new List<Hole>();
		this.Assert<Hole>(m_holeTemplate, "ERROR: m_holeTemplate must be initialize!");
		this.Assert<Boat>(m_boat, "ERROR: m_boat must be initialize!");
	}

	private void Update ()
	{
		m_interval += Time.deltaTime;

		if (m_interval > GameLogic.HARD_INTERVAL)
		{
			m_interval = 0;
			this.CreateHole();
		}

	}

	private int NumHolesAt (Mass p_mass)
	{
		Predicate<Hole> massCondition = new Predicate<Hole>(h => h.MassHole == p_mass);
		return m_holes.FindAll(massCondition).Count;
	}

	private Hole CreateHole ()
	{
		GameObject go = (GameObject)GameObject.Instantiate(m_holeTemplate.gameObject);
		Hole hole = go.GetComponent<Hole>();
		m_holes.Add(hole);

		// randomize positions
		hole.transform.parent = m_boat.gameObject.transform;
		hole.transform.position = m_holePositions[UnityEngine.Random.Range(0, m_holePositions.Count)].position;
		hole.transform.rotation = Quaternion.identity;
		hole.OnDestroy += this.OnHoleDestroy;
		hole.OnTapEvent += this.OnTap;

		// sink the pakking boat
		m_boat.AdjustMass(hole.MassHole, this.NumHolesAt(hole.MassHole));

		return hole;
	}

	private void OnHoleDestroy (Hole p_hole)
	{
		m_holes.Remove(p_hole);
		m_boat.AdjustMass(p_hole.MassHole, this.NumHolesAt(p_hole.MassHole));
		GameObject.Destroy(p_hole.gameObject);
	}

	private void OnTap (TapType p_type)
	{
		this.Log("GameLog::OnTap", "tap:{0}", p_type); 
	}
}
