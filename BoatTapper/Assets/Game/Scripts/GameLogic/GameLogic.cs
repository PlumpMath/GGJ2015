using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

[System.Serializable]
public class TransformList
{
	[SerializeField]
	public Transform[] list;
}

public class GameLogic : MonoBehaviour 
{
	public static readonly float EASY_INTERVAL = 10.0f;
	public static readonly float MEDIUM_INTERVAL = 7.0f;
	public static readonly float HARD_INTERVAL = 4.0f;
	public static readonly Vector2 UNIT_SEA_LEVEL = new Vector2(0.0f, 0.01f);
	public static readonly int NUMBER_OF_HOLES = 3;

	// template
	[SerializeField]
	private Hazard m_holeTemplate;
	[SerializeField]
	private Boat m_boat;
	[SerializeField]
	private Transform m_seaLevel;

	private List<Hazard> m_hazards;
	private Vector2 m_defaultSeaLevel;
	[SerializeField] private Vector2 m_targetSeaLevel;
	[SerializeField] private int[] m_hazardLimit = {20, 20, 20, 20, 20};

	[SerializeField]
	private float m_levelDuration = 180f;
	private float m_levelElapsedTime = 0f;
	private float m_levelProgression = 1.0f;

	[SerializeField] public bool GameHasStarted { get; set; }

	public static GameLogic Instance { get; private set; }

	private void Awake () 
	{
		if(GameLogic.Instance == null)
			GameLogic.Instance = this;
	}

	private void Start ()
	{
		m_hazards = new List<Hazard>();
		this.Assert<Hazard>(m_holeTemplate, "ERROR: m_holeTemplate must be initialized!");
		this.Assert<Boat>(m_boat, "ERROR: m_boat must be initialized!");
		this.Assert<Transform>(m_seaLevel, "ERROR: m_seaLevel must be initialized!");

		// set default values
		m_defaultSeaLevel = m_seaLevel.transform.position;
		m_targetSeaLevel = m_seaLevel.transform.position;
	}

	private void Update ()
	{

		if(GameHasStarted)
		{
			if(m_levelElapsedTime <= m_levelDuration)
			{
				m_levelElapsedTime += Time.deltaTime * m_levelProgression;

				/* Replace This
				m_interval += Time.deltaTime;


				if (m_interval > GameLogic.HARD_INTERVAL)
				{
					m_interval = 0;
					this.AddHazardOnShip(m_holeTemplate);
				}
				//*/
			}
			else
			{
				// 
				OnGameHasEnded();
				GameHasStarted = false;
			}
		}
	}

	private void FixedUpdate ()
	{
		m_seaLevel.transform.position  = (Vector2.MoveTowards(new Vector2(m_seaLevel.position.x, m_seaLevel.position.y), m_targetSeaLevel, 0.1f * Time.deltaTime));
	}
	/*
	private Transform[] GetHazardPositions(TapType p_type)
	{
		return m_hazardPositions[(int) p_type].list;
	}

	private void SetHazardAtRandomPosition(Hazard p_damage)
	{	
		Debug.Log("GameLogic::SetHazardAtRandomPosition::p_damage: " + p_damage.name );
		p_damage.transform.parent = m_boat.gameObject.transform;

		Transform[] hazardPositions = GetHazardPositions(p_damage.TapType);
		if(hazardPositions.Length > 0)
			p_damage.transform.position = hazardPositions[UnityEngine.Random.Range(0, hazardPositions.Length)].position;
		
		p_damage.transform.rotation = Quaternion.identity;

		Debug.Log("GameLogic::SetHazardAtRandomPosition::p_damage.transform.position: " + p_damage.transform.position );
	}
	//*/

	private List<Hazard> GetDamageAtSide (List<Hazard> p_holes, Side p_side)
	{
		Predicate<Hazard> massCondition = new Predicate<Hazard>(h => h.LocalSideLocation == p_side);
		return p_holes.FindAll(massCondition);
	}

	private List<Hazard> GetDamageOfTapType (List<Hazard> p_holes, TapType p_type)
	{
		Predicate<Hazard> massCondition = new Predicate<Hazard>(h => h.TapType == p_type);
		return p_holes.FindAll(massCondition);
	}

	private List<Hazard> GetDamageOfHoldType (List<Hazard> p_holes, TapType p_type)
	{
		Predicate<Hazard> massCondition = new Predicate<Hazard>(h => h.HoldType == p_type);
		return p_holes.FindAll(massCondition);
	}

	private List<Hazard> GetActiveHazards (List<Hazard> p_holes, bool p_state)
	{
		Predicate<Hazard> massCondition = new Predicate<Hazard>(h => h.ActiveHazard == p_state);
		return p_holes.FindAll(massCondition);
	}

	public void UpdatePaused(bool p_paused)
	{
		Time.timeScale = p_paused ? 0.0f : 1.0f; // YES. I KNOW THIS IS SHITTY CODE.
	}

	public Hazard AddHazardOnShip (Hazard p_hazard)
	{
		p_hazard.transform.parent = m_boat.transform;
		m_hazards.Add(p_hazard);

		//hazard.transform.position = m_holePositions[UnityEngine.Random.Range(0, m_holePositions.Count)].position;
		p_hazard.OnDestroy += this.OnDamageDestroy;
		p_hazard.OnTapEvent += this.OnTap;

		// sink the pakking boat
		DamageShip(p_hazard);

		return p_hazard;
	}

	private void DamageShip(Hazard p_damage)
	{
		List<Hazard> activeHazards = this.GetActiveHazards(m_hazards, true);
		List<Hazard> hazards = this.GetDamageOfTapType(activeHazards, p_damage.TapType);
		List<Hazard> fireHazards = this.GetDamageOfTapType(activeHazards, TapType.Pail);

		hazards.AddRange(fireHazards);

		switch(p_damage.TapType)
		{
		case TapType.Hammer:
			List<Hazard> holesAtSide = this.GetDamageAtSide(hazards, p_damage.LocalSideLocation);
			m_boat.AdjustMass(p_damage.LocalSideLocation, holesAtSide.Count);
			this.AdjustSeaLevel(hazards.Count);
			break;
		case TapType.Stitch:
			this.AdjustShipSpeed(hazards.Count);
			break;
		case TapType.Pail:
			List<Hazard> damageAtSide = this.GetDamageAtSide(hazards, p_damage.LocalSideLocation);
			m_boat.AdjustMass(p_damage.LocalSideLocation, damageAtSide.Count);
			this.AdjustSeaLevel(hazards.Count);
			this.AdjustShipSpeed(hazards.Count);
			break;

		}

	}

	private void AdjustShipSpeed(int p_sailHoles)
	{
		int holeLimit = m_hazardLimit[(int) TapType.Stitch];
		m_levelProgression = Mathf.Max(holeLimit - p_sailHoles, 1) / Mathf.Max(holeLimit, 2);
	}

	private void AdjustSeaLevel (int p_hullHoles)
	{
		if(p_hullHoles < m_hazardLimit[(int) TapType.Hammer])
		{
			m_targetSeaLevel = m_defaultSeaLevel - (UNIT_SEA_LEVEL * p_hullHoles);
			m_targetSeaLevel.x = 0f;
		}
		else
		{
			ForcedSinkShip();
		}

	}

	private void ForcedSinkShip()
	{
		//m_boat.AdjustMass(Side.Left, 100); // Use Constants instead
		m_targetSeaLevel = m_defaultSeaLevel - (UNIT_SEA_LEVEL * 100);
	}

	private void OnDamageDestroy (Hazard p_damage)
	{
		m_hazards.Remove(p_damage);
		DamageShip(p_damage);
		GameObject.Destroy(p_damage.gameObject);
	}

	private void OnGameHasEnded ()
	{
		// Stuff Here
		Debug.Log("YOU WIN!");
	}

	private void OnTap (TapType p_type)
	{
		this.Log("GameLog::OnTap", "tap:{0}", p_type); 
	}
}
