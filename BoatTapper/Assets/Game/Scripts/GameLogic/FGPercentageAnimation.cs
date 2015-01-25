using UnityEngine;
using System.Collections;

public class FGPercentageAnimation : MonoBehaviour {

	public Vector3 startPosition, endPosition;

	void Awake () {
		if(GameLogic.Instance != null)
			GameLogic.Instance.OnGameProgressUpdate += UpdatePosition;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void UpdatePosition () {
	
	}

	void UpdatePosition(float p_percentage)
	{
		transform.localPosition = Vector3.Lerp(startPosition, endPosition, p_percentage);
	}
}
