using UnityEngine;
using System.Collections;

public class TimedObjectDestructor : MonoBehaviour {

	public float m_delay = 0.5f;

	// Use this for initialization
	void Start () {
		StartCoroutine(DestroyAfterSeconds());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator DestroyAfterSeconds()
	{
		yield return new WaitForSeconds(m_delay);
		Destroy(this.gameObject);
	}
}
