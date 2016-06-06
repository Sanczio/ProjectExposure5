using UnityEngine;
using System.Collections;

public class ScriptPickUpSpawner : MonoBehaviour {

    public GameObject _pickUpMovementSpeed;
    public GameObject _pickUpShooting;
    public GameObject _pickUpInstaPick;

	// Use this for initialization
	void Start () {
	
	}
	
    public void CallPickUpSpawner(string pickUpType, string placeName)
    {
        GameObject tempPlace = GameObject.Find(placeName);
        Vector3 tempPosition = tempPlace.transform.position;
        switch (pickUpType)
        {
            case "MovementBoost":
                SpawnPickUp(_pickUpMovementSpeed, tempPosition, pickUpType);
                break;

            case "ShootingBoost":
                SpawnPickUp(_pickUpShooting, tempPosition, pickUpType);
                break;
            case "InstaPickUp":
                SpawnPickUp(_pickUpInstaPick, tempPosition, pickUpType);
                break;
        }
            
    }

    public void CallPickUpSpawner(string pickUpType, Transform transform)
    {
        Vector3 tempPos = transform.position;
        switch (pickUpType)
        {
            case "MovementBoost":
                SpawnPickUp(_pickUpMovementSpeed, tempPos, pickUpType);
                break;

            case "ShootingBoost":
                SpawnPickUp(_pickUpShooting, tempPos, pickUpType);
                break;
            case "InstaPickUp":
                SpawnPickUp(_pickUpInstaPick, tempPos, pickUpType);
                break;


        }

    }

    void SpawnPickUp(GameObject prefab, Vector3 placeToSpawn, string pickUpType)
    {
        GameObject spawnPickUp = Instantiate(prefab, placeToSpawn, Quaternion.identity) as GameObject;
        ScriptPickUp tempScript = spawnPickUp.GetComponent<ScriptPickUp>();
        tempScript.SetPickUpType(pickUpType);

    }

}
