using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CannonBall : MonoBehaviour 
{
	public event Action<CannonBall> OnDestroyBall;
	
	public void Throw (Vector2 p_throwForce)
	{
		this.rigidbody2D.AddForce(p_throwForce);
	}

	private void DestroyBall ()
	{
		if (this.OnDestroyBall == null) { return; }
		this.OnDestroyBall(this);
	}
}
