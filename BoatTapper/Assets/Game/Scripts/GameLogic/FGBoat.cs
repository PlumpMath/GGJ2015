using UnityEngine;
using System.Collections;
using Extensions;

public class FGBoat : MonoBehaviour 
{
	public float minDelay	= 5f;
	public float maxDelay = 10f;
	public float minTravelTime = 60f;
	public float maxTravelTime = 100f;

	[SerializeField] private Vector3 m_startingPoint;
	[SerializeField] private Vector3 m_endingPoint;
	[SerializeField] private float m_duration;
	[SerializeField] private float m_delay;

	public bool lockYPosition = false;

	private void Start () 
	{
		this.StartMoving();
	}

	private void StartMoving ()
	{
		m_delay = UnityEngine.Random.Range(minDelay, maxDelay);
		m_duration = UnityEngine.Random.Range(minTravelTime, maxTravelTime);

		Vector3 startTargetPos = m_startingPoint;
		Vector3 targetPos = m_endingPoint;

		if(lockYPosition)
		{
			startTargetPos.y = transform.position.y;
			targetPos.y = transform.position.y;
		}
		
		transform.position = startTargetPos;
		Hashtable hash = new Hashtable();
		hash["position"] = targetPos;
		hash["time"] = m_duration;
		hash["delay"] = m_delay;
		hash["oncompletetarget"] = this.gameObject;
		hash["oncomplete"] = "OnMoveComplete";
		hash["easetype"] = iTween.EaseType.linear;

		iTween.MoveTo(this.gameObject, hash);
	}

	private void OnMoveComplete ()
	{
		Debug.Log("OnMoveComplete");
		this.StartMoving();
	}
}
