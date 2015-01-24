using UnityEngine;
using System.Collections;

public class BoatShaker : MonoBehaviour {

	public GameObject[] weights;
	private int Weight = 1;
	private int index = 0;
	public float Min = 1.25f;
	public float Max = 1.55f;
	public float interval = 5f;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("ChangeWeight", 1f, interval);
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	void ChangeWeight()
	{
		//Debug.Log (index);
		weights[index].rigidbody2D.mass = Min;
		//weights [index].rigidbody2D.gravityScale = 1f;
		index += Weight;
		Weight *= -1;
		weights[index].rigidbody2D.mass = Max;
		//weights [index].rigidbody2D.gravityScale = 1f;
		/*for (int i=0; i<weights.Length; i++)
		{
			weights[i].rigidbody2D.mass += Random.Range(-2, 2);
		}*/

		/*weights[0].rigidbody2D.mass = Random.Range(0.5f, 2);
		weights [1].rigidbody2D.mass = 2 - weights [0].rigidbody2D.mass;*/




	}


}
