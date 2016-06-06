using UnityEngine;
using System.Collections;

public class ScriptBulletMovement : MonoBehaviour {

    private Vector3 _targetPosition;
    float _currentTime = 0.0f;
    float _maxTime = 20.0f;
    float _timeToDestroy = 0.0f;

    public GameObject _target;

	// Use this for initialization
	void Start () {
	
	}
	
    public void SetTarget(Vector3 targetPosition)
    {

    }

	// Update is called once per frame
	void Update () {
        if (_target)
        {
            this.transform.LookAt(_target.transform);
        }
        else DestroyBullet();
        
        transform.position += transform.forward*10 * Time.deltaTime;
        _currentTime = _currentTime +( 1 * Time.deltaTime);
        if (_currentTime >= _maxTime)
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject, _timeToDestroy);

    }



}
