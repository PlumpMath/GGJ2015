using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;

public class CollisionBox : MonoBehaviour
{
	[SerializeField] private GameObject m_onTriggerSpawnObj;
	//[SerializeField] private Hazard m_hazardTemplate;
	[SerializeField] private TapType m_tapType;
	private Hazard hazard;

	public TapType TapType { 
		get { 
			return m_tapType; 
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

		if(!hazard || hazard == null)
		{
			Debug.Log("CollisionBox::CreateDamage::p_damage: " + m_tapType );
			//GameObject go = (GameObject)GameObject.Instantiate(m_hazardTemplate.gameObject, newPosition, Quaternion.identity);
			//hazard = go.GetComponent<Hazard>();
			//this.Log("CollisionBox::CreatHazard", "ScaleX:{0} Y:{1}", go.transform.localScale.x, go.transform.localScale.y);

			// randomize positions
			if (GameLogic.Instance != null)
			{
				hazard = GameLogic.Instance.CreateHazard(this.TapType);

				if(hazard)
				{
					hazard.transform.position = newPosition;
					hazard.transform.rotation = Quaternion.identity;
					GameLogic.Instance.AddHazardOnShip(hazard);
				}
			}
			
		}

	}
}