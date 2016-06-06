using UnityEngine;
using System.Collections;

public class ScriptEnemyCarCrash : MonoBehaviour {

    private ScriptCivilCar _carScript;
    private int _hitCount = 0;
    private float _timeToDestroy = 2.0f; //Take from Settings 

    private bool _carIsFull = false;
    private ScriptEnemyCarTrash _sEnemyCarTrash;

    public GameObject _trashObject;

    
    // Use this for initialization
    void Start()
    {
        _carScript = this.GetComponent<ScriptCivilCar>();
        _sEnemyCarTrash = this.GetComponent<ScriptEnemyCarTrash>();
    }


    void OnCollisionEnter(Collision otherObject)
    {
        print(otherObject.gameObject.tag);
        if (otherObject.gameObject.tag == "Player" || otherObject.gameObject.tag == "bullet")
        {

            print("Hit by player");
            _hitCount++;
            if (_hitCount == 1)
            {
                StopAgent();
            }

            if (_hitCount == 1)
            {
                if (_carIsFull)
                {
                    GivePlayerReward();
                }

                DestroyCar();
            }

        }
        if (otherObject.gameObject.tag == "bullet")
        {
            print("BUllet hitted");
            otherObject.gameObject.GetComponent<ScriptBulletMovement>().DestroyBullet();
        }

    }

    //Stop Agent and inform about that
    void StopAgent()
    {
        _sEnemyCarTrash.setAgentStatus(false);
        _carScript.StopAgentInCar();
    }

    //Ask root to give reward for player

    void GivePlayerReward()
    {
        ScriptPickUpSpawner tempScript = GameObject.Find("Root").GetComponent<ScriptPickUpSpawner>();
        tempScript.CallPickUpSpawner(DecideWhatPickUp(), this.gameObject.transform);

    }

    string DecideWhatPickUp()
    {


        return "ShootingBoost";
    }

    //Play Crash animation before this
    void DestroyCar()
    {
        ScriptCarSpawner tempScript = GameObject.Find("Root").GetComponent<ScriptCarSpawner>();
        tempScript.RemoveCarFromList(this.gameObject);
        Destroy(this.gameObject, _timeToDestroy);

    }

    public void UpdateTrashStatus(bool trashStatus)
    {
        _carIsFull = trashStatus;
    }


}
