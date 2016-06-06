using UnityEngine;
using System.Collections;

public class ScriptActiveRunAway : MonoBehaviour {

    public GameObject _parent;
    public bool _instaPickUpActive = false;
    private float _instaPickTime = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.tag == "bio_trash")
        {
            ActivatePickupRunAway(collidingObject);

        }
        if (collidingObject.tag == "recycable_trash_a" || collidingObject.tag == "recycable_trash_b" || collidingObject.tag == "recycable_trash_c")
        {
            ActivatePickupRunAway(collidingObject);

        }


    }

    void OnTriggerStay(Collider collidingObject)
    {
        if (collidingObject.tag == "bio_trash")
        {
            ActivatePickupRunAway(collidingObject);

        }
        if (collidingObject.tag == "recycable_trash_a" || collidingObject.tag == "recycable_trash_b" || collidingObject.tag == "recycable_trash_c" )
        {
            ActivatePickupRunAway(collidingObject);

        }

    }

    void ActivatePickupRunAway(Collider targetObject)
    {
		if ( targetObject.gameObject != null){
			ScriptPickUpRunAway tempScript = targetObject.GetComponent<ScriptPickUpRunAway>();
			tempScript.ActivatePickUp(_parent.transform, _instaPickUpActive);
		} 
    }

    public void ActivateInstaPick()
    {
        StartCoroutine(UnlimitedShooting());
    }

    IEnumerator UnlimitedShooting()
    {
        _instaPickUpActive = true;
        yield return new WaitForSeconds(_instaPickTime);
        _instaPickUpActive = false;
    }
}
