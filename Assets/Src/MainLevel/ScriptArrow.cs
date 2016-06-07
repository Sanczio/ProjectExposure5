using UnityEngine;
using System.Collections;

public class ScriptArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	float amount = 0;
	// Update is called once per frame
	void Update () {
		
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, amount += 4.0f, transform.eulerAngles.z);
	}

	public void destroyArrow(float after)
	{
		Destroy (gameObject,after);
	}
}
