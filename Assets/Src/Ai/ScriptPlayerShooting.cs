using UnityEngine;
using System.Collections;

public class ScriptPlayerShooting : MonoBehaviour {

    public GameObject _bulletPrefab;


	// Use this for initialization
	void Start () {
	
	}
	
    /*
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //RaycastHit.transform.gameObject hit : GameObject;

            if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.tag == "EnemyCar")
                {
                    
                    
                }
            }
        }
    }
    */

    public void CallBulletSpawn(GameObject _target)
    {
        SpawnBullet(_target);
    }

    void SpawnBullet(GameObject target)
    {

        GameObject bullet = Instantiate(_bulletPrefab, this.transform.position + transform.forward*3, Quaternion.identity) as GameObject;
        ScriptBulletMovement tempScript = bullet.GetComponent<ScriptBulletMovement>();
        tempScript._target = target;

    }
}
