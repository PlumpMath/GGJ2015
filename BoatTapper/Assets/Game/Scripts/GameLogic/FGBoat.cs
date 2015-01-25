using UnityEngine;
using System.Collections;
using Extensions;

public class FGBoat : MonoBehaviour 
{
	public readonly float MIN_DELAY	= 5f;
	public readonly float MAX_DELAY = 10f;
	public readonly float MIN_TRAVEL_TIME = 60f;
	public readonly float MAX_TRAVEL_TIME = 100f;

	[SerializeField] private Vector3 m_startingPoint;
	[SerializeField] private Vector3 m_endingPoint;
	[SerializeField] private float m_duration;
	[SerializeField] private float m_delay;

	private void Start () 
	{
		this.StartMoving();
	}

	private void StartMoving ()
	{
		m_delay = UnityEngine.Random.Range(MIN_DELAY, MAX_DELAY);
		m_duration = UnityEngine.Random.Range(MIN_TRAVEL_TIME, MAX_TRAVEL_TIME);

		Hashtable hash = new Hashtable();
		hash["position"] = m_endingPoint;
		hash["time"] = m_duration;
		hash["delay"] = m_delay;
		hash["oncompletetarget"] = this.gameObject;
		hash["oncomplete"] = "OnMoveComplete";

		iTween.MoveTo(this.gameObject, hash);
	}

	private void OnMoveComplete ()
	{
		this.StartMoving();
	}
}
