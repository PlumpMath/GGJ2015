using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class CannonBall : MonoBehaviour 
{
	public readonly float THRESHOLD = -0.60f;

	public event Action<CannonBall> OnDestroyBall;

	[SerializeField] private TapType m_tapType;

	private void Update ()
	{
		//this.Log("CannonBall::Update", "Y:{0}", this.transform.position.y);

		// test
		/*
		if (this.transform.position.y <= THRESHOLD) 
		{
			this.Explode();
		}
		//*/
	}

	private void OnTriggerEnter2D (Collider2D p_coll)
	{
		CollisionBox box = p_coll.gameObject.GetComponent<CollisionBox>();

		this.Log("Hit:", box + "*");

		if(box)
		{
			this.Log("Hit:", "box.TapType:" + box.TapType.ToString() + " == m_TapType: " + m_tapType.ToString());
			if (box.TapType == m_tapType) 
			{ 
				box.CreateHazard(this.transform.position);
				this.Explode();
			}
		}
		else
		{
			this.Explode();
			this.Log("Hit:","NO BOX");
		}
	}

	public TapType TapType { get{ return m_tapType; } set { m_tapType = value; } }

	public void Throw (Vector2 p_throwForce)
	{
		this.rigidbody2D.AddForce(p_throwForce);
		this.rigidbody2D.AddTorque(-p_throwForce.x);
	}

	public void Explode ()
	{
		Debug.Log ("BOOM!");
		// explode animation
		// stop rigidbody
		this.rigidbody2D.velocity = Vector2.zero;
		this.rigidbody2D.isKinematic = true;
		// test: for destroy ball
		this.DestroyBall();
	}

	private void DestroyBall ()
	{
		if (this.OnDestroyBall == null) { return; }
		this.OnDestroyBall(this);
	}
}
