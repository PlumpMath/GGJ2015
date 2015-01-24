using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class CollisionBox : MonoBehaviour
{
	[SerializeField] private GameObject m_onTriggerSpawnObj;
	[SerializeField] private Hazard m_hazardTemplate;
	private Hazard hazard;

	public TapType TapType { 
		get { 
			if(m_hazardTemplate) 
				return m_hazardTemplate.TapType; 
			return TapType.Patch; 
		} 
	}

	public bool CanSpawn {
		get {
			return hazard != null && hazard.gameObject.activeSelf;
		}
	}

	public void CreateHazard(Vector3 p_position)
	{
		Vector3 newPosition = p_position;
		newPosition.z = transform.position.z;

		if(m_onTriggerSpawnObj)
		{
			Debug.Log("CollisionBox::CreateDamage::m_onTriggerSpawnObj: " + m_onTriggerSpawnObj.name );
			GameObject go = (GameObject)GameObject.Instantiate(m_onTriggerSpawnObj.gameObject, newPosition, Quaternion.identity);
		}

		if(m_hazardTemplate && (!hazard || hazard == null))
		{
			Debug.Log("CollisionBox::CreateDamage::p_damage: " + m_hazardTemplate.name );
			GameObject go = (GameObject)GameObject.Instantiate(m_hazardTemplate.gameObject, newPosition, Quaternion.identity);
			hazard = go.GetComponent<Hazard>();
			this.Log("CollisionBox::CreatHazard", "ScaleX:{0} Y:{1}", go.transform.localScale.x, go.transform.localScale.y);
		}

		// randomize positions
		if (GameLogic.Instance != null && hazard.TapType == this.TapType)
		{
			GameLogic.Instance.AddHazardOnShip(hazard);
		}

	}
}