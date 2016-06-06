﻿using UnityEngine;
using System.Collections;

public class ScriptPickUpRunAway : MonoBehaviour {

    private NavMeshAgent _agent;
	public int ID;
	private float _runAwayDistance = 1.0f;
    public float _runToPlayerDistanc = 1.0f;
	ScriptPlayerControls player;
	ScriptTrashController trashController;

    public bool _active;

	//private GameObject[] bio_spawns;
	//private GameObject[] recycable_spawns;

    // Use this for initialization
    void Start () {
		_agent = gameObject.GetComponent<NavMeshAgent>();
		player = GameObject.Find ("Player").GetComponent<ScriptPlayerControls> ();
		trashController = GameObject.Find ("Root").GetComponent<ScriptTrashController> ();
		ScriptSettingsGameplay settings = GameObject.Find ("Root").GetComponent<ScriptSettingsGameplay> ();
		_runAwayDistance = settings.trash_escape_distance;
		_agent.speed = settings.trash_escape_speed;

    }
	
	public void setSpeed( float speed )
	{
		_agent.speed = speed;
	}

    public void setActive(bool status)
    {
        _active = status;
    }

    public void ActivatePickUp(Transform playerPosition, bool instaPick)
    {
		if ( _active && _agent != null && gameObject != null && _agent.enabled)
        {
            _agent.destination = DecideWhereToGo(playerPosition, instaPick);
        }
		
    }

    Vector3 DecideWhereToGo(Transform playerPosition, bool instaPick)
    {   
        

		Vector3 goalPosition ;
        Transform tempTransform = this.GetComponent<Transform>();
        float distancePlayerTrash = Vector3.Distance(tempTransform.position, playerPosition.position );
        if (distancePlayerTrash <= _runToPlayerDistanc || instaPick)
        {
            return playerPosition.position;
        } else
        {
            Vector3 normVector = playerPosition.forward;//-1 * (playerPosition - tempTransform.position);
            goalPosition = playerPosition.position + normVector.normalized * _runAwayDistance;
            return goalPosition;
        }
        
    }

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "PlayerCrashCollider") {
			ScriptTrashController controller = GameObject.Find ("Root").GetComponent<ScriptTrashController> ();

			if (gameObject.tag == "recycable_trash_a") {
				player.addTrash (1);
				controller.removeTrash (gameObject);
				Destroy (gameObject);

				//spawnTempRecy.makeAvailable (true);
			}
			if (gameObject.tag == "recycable_trash_b") {
				player.addTrash (2);
				controller.removeTrash (gameObject);
				Destroy (gameObject);
				//spawnTempRecy.makeAvailable (true);
			}
			if (gameObject.tag == "recycable_trash_c") {
				player.addTrash (3);
				controller.removeTrash (gameObject);
				Destroy (gameObject);
				//spawnTempRecy.makeAvailable (true);
			}
			if (gameObject.tag == "bio_trash") {
				player.addTrash (0);
				controller.removeTrash (gameObject);
				Destroy (gameObject);
				//spawnTempBio.makeAvailable (true);
			}	
		}
	}
		

}
