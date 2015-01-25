using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleScreen : MonoBehaviour 
{

	
	[SerializeField]
	private List<GameObject> m_prepareForBattleTemplate = new List<GameObject>();
	
	private Queue<GameObject> PopupQueue = new Queue<GameObject>();

	private void Start()
	{
		for(int i = 0; i < m_prepareForBattleTemplate.Count; i++)
		{
			GameObject target = m_prepareForBattleTemplate[i].gameObject;
			target.SetActive(false);
			PopupQueue.Enqueue(target);
		}
		AnimatePopups();
	}

	private void AnimatePopups()
	{
		if(PopupQueue.Count > 0)
		{
			GameObject target = PopupQueue.Dequeue();
			target.SetActive(true);
			Hashtable hash = new Hashtable();
			hash["scale"] = Vector3.zero;
			hash["easetype"] = iTween.EaseType.easeInOutBounce;
			hash["time"] = 1.0f;
			
			hash["oncompletetarget"] = this.gameObject;
			hash["oncomplete"] = "AnimatePopups";
			
			
			// Animation Code Here
			iTween.ScaleFrom(target, hash);
		}
	}

	private void OnClickedPlay ()
	{
		Application.LoadLevel(1);
	}

	private void OnClickedCredits ()
	{
	}
}
