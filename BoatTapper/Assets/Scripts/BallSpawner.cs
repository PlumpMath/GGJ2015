using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class BallSpawner : MonoBehaviour 
{
	public readonly float EASY_SPAWN = 10f;
	public readonly float MEDIUM_SPAWN = 6f;
	public readonly float HARD_SPAWN = 4f;

	[SerializeField]
	private CannonBall m_ballTemplate;
	[SerializeField]
	private Vector2 m_throwForce;
	private List<CannonBall> m_balls;
	private float m_interval;

	private void Start ()
	{
		this.Assert<CannonBall>(m_ballTemplate, "ERROR: m_ballTemplate must not be null.");

		// initializations
		m_balls = new List<CannonBall>();
		m_throwForce = new Vector2(-1024f, 768f) * 0.15f;
	}

	private void Update ()
	{
		m_interval += Time.deltaTime;

		if (m_interval >= HARD_SPAWN) 
		{
			m_interval = 0f;
			this.Throw(3);
		}
	}

	public void Throw (int p_num)
	{
		GameObject obj = (GameObject)GameObject.Instantiate(m_ballTemplate.gameObject);
		CannonBall ball = obj.GetComponent<CannonBall>();
		ball.OnDestroyBall += this.OnDestroyBall;
		ball.Throw(m_throwForce);
		ball.transform.parent = this.transform;
		ball.transform.position = this.transform.position;

		m_balls.Add(ball);
	}

	private void OnDestroyBall (CannonBall p_ball)
	{
		m_balls.Remove(p_ball);
		GameObject.Destroy(p_ball.gameObject);
	}
}
