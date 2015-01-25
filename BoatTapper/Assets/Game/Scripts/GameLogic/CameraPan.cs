using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
	[SerializeField] private float m_moveDuration = 5f;
	[SerializeField] private Vector3 m_startingPoint = new Vector3(3.022326f, -0.25f, -5);
	[SerializeField] private Vector3 m_endPoint = new Vector3(0.0f, -0.25f, -5);

	private void Awake ()
	{
		this.transform.position = m_startingPoint;
		GameLogic.Instance.OnStartGame += StartGame;
	}

	private void Start ()
	{
		//this.StartGame();
	}

	private void StartGame ()
	{
		iTween.MoveTo(this.gameObject, m_endPoint, m_moveDuration);
	}
}
