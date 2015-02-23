using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private float _bulletLifeTime;


    private float _deathTime;

	// Use this for initialization
	void Start () 
    {
        //_bulletSpeed = 0.1f;
        //_bulletLifeTime = 3f;
        _deathTime = Time.time + _bulletLifeTime;

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.time < _deathTime)
        {
            Vector2 _movement = Vector2.zero;
            _movement.y += _bulletSpeed;
            transform.Translate(_movement);
        }
        else
            Destroy(gameObject);
	}
}
