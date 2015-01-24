using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class CollisionBox : MonoBehaviour
{
	[SerializeField] private TapType m_tapType;

	public TapType TapType { get { return m_tapType; } }
}