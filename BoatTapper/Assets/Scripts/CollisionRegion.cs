using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class CollisionRegion : MonoBehaviour 
{
	[SerializeField] private List<CollisionBox> m_boxes;

	private void Start ()
	{
		this.Assert<List<CollisionBox>>(m_boxes, "ERROR: m_boxes must be setup in inspector");
	}
}