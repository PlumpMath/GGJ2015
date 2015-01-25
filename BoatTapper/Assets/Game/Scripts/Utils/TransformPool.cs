using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransformPool : MonoBehaviour {

	public Transform[] m_template;

	[SerializeField]
	List<Transform> m_list = new List<Transform>();

	[SerializeField]
	Stack<Transform> m_pool = new Stack<Transform>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform GetRandomTemplate() {
		return m_template[Random.Range(0, m_template.Length)];
	}

	public Transform InstantiateTemplate() {
		return (Instantiate(GetRandomTemplate().gameObject) as GameObject).transform;
	}

	public Transform Pop() {
		if(this.IsEmpty()) {
			this.Push(InstantiateTemplate());
		}

		Transform r_object = m_pool.Pop();
		r_object.gameObject.SetActive(true);

		return r_object;
	}

	public void Push(Transform p_object) {
		p_object.gameObject.SetActive(false);

		if(!m_list.Contains(p_object)) {
			m_list.Add(p_object);
		}

		if(m_list.Contains(p_object)) {		
			if(!m_pool.Contains(p_object))
				m_pool.Push(p_object);
		}
	}

	public bool IsEmpty() {
		return this.Count() <= 0;
	}

	public int Count() {
		return m_pool.Count;
	}
}
