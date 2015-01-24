using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class CollisionBox : MonoBehaviour
{
	[SerializeField] private Hazard m_hazardTemplate;

	public TapType TapType { 
		get { 
			if(m_hazardTemplate) 
				return m_hazardTemplate.TapType; 
			return TapType.Patch; 
		} 
	}

	public void CreateHazard(Vector3 p_position)
	{
		Debug.Log("CollisionBox::CreateDamage::p_damage: " + m_hazardTemplate.name );
		GameObject go = (GameObject)GameObject.Instantiate(m_hazardTemplate.gameObject, p_position, Quaternion.identity);
		Hazard hazard = go.GetComponent<Hazard>();

		this.Log("CollisionBox::CreatHazard", "ScaleX:{0} Y:{1}", go.transform.localScale.x, go.transform.localScale.y);

		// randomize positions
		if (GameLogic.Instance != null && hazard.TapType == this.TapType)
		{
			GameLogic.Instance.AddHazardOnShip(hazard);
		}

	}
}