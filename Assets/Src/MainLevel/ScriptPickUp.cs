using UnityEngine;
using System.Collections;

public class ScriptPickUp : MonoBehaviour {

    public string _pickUpType;
	private ScriptPlayerControls player;

    private float _slowMTime = 1.0f;
    private bool _ifPlayerSlowed = false;
    private float _percentToSlow = 0.5f;

	void Start()
	{
		player = GameObject.Find ("Player").GetComponent<ScriptPlayerControls> ();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "pickUpCollider") {
			switch (_pickUpType) {
            case "MovementBoost":
                    ScriptSlowMotion tempSlow = GameObject.Find("Root").GetComponent<ScriptSlowMotion>();
                    tempSlow.slowMotion(_slowMTime, _ifPlayerSlowed, _percentToSlow);
                    Destroy(this.gameObject);
                    break;

            case "ShootingBoost":
                    ScriptPlayerControls tempScript = player.GetComponent<ScriptPlayerControls>();
                    tempScript.ActivateUnlimitedAmmo();
                    Destroy(this.gameObject);
                    break;
            case "InstaPickUp":
                    ScriptActiveRunAway tempRunAwayScript = player.GetComponentInChildren<ScriptActiveRunAway>();
                    tempRunAwayScript.ActivateInstaPick();
                    Destroy(this.gameObject);
                    break;
            }
        }
	}


	public void SetPickUpType(string pickUpType)
	{
        _pickUpType = pickUpType;
	}

}
