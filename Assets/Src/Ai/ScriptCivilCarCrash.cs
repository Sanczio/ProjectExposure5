using UnityEngine;
using System.Collections;

public class ScriptCivilCarCrash : MonoBehaviour {

    private ScriptCivilCar _carScript;
    private int _hitCount = 0;
    private float _timeToDestroy = 2.0f; //Take from Settings 

	// Use this for initialization
	void Start () {
        _carScript = this.GetComponent<ScriptCivilCar>();
	}


    void OnCollisionEnter(Collision otherObject)
    {

        if (otherObject.gameObject.tag == "Player" || otherObject.gameObject.tag == "bullet")
        {
            //print("Hit by player");
            _hitCount++;
            if (_hitCount == 1)
            { 
				_carScript.StopAgentInCar(); 
				addForce (otherObject);
			}

            if (_hitCount == 1)
            {
                DestroyCar();
            }
            
        }

    }

	private void addForce(Collision collider)
	{
		Rigidbody tempRigidbody = gameObject.GetComponent<Rigidbody>();
		tempRigidbody.isKinematic = false;
		tempRigidbody.AddForce (collider.gameObject.transform.forward * 0.00001f, ForceMode.Force);
	}

    //Play Crash animation before this
    void DestroyCar()
    {
        ScriptCarSpawner tempScript = GameObject.Find("Root").GetComponent<ScriptCarSpawner>();
        tempScript.RemoveCarFromList(this.gameObject);

        Destroy(this.gameObject, _timeToDestroy);

    }
}
