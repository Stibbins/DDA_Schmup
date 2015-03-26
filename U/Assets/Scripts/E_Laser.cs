using UnityEngine;
using System.Collections;

public class E_Laser : MonoBehaviour {

    public Transform tinyLaser;
    public Transform bigLaser;


    private bool _lockRotation;
    public float _rotationSpeed;
    public float _attackDelay;
    private float _attackDamage;
    private Vector2 _playerPosition;
    private float _attackTime;
    public float _attackLength;

    private Vector2 _rotationDirection;
    private Transform _tinyLaserTransform;
    private Transform _bigLaserTransform;


	void Awake () 
    {
        _lockRotation = false;
        _attackTime = Time.time - 1;
        _tinyLaserTransform = Instantiate(tinyLaser, transform.position, Quaternion.identity) as Transform;
        _tinyLaserTransform.parent = transform;
        _bigLaserTransform = Instantiate(bigLaser, transform.position, Quaternion.identity) as Transform;
        _bigLaserTransform.parent = transform;
        _bigLaserTransform.gameObject.SetActive(false);
	}
	

	void FixedUpdate () {
        _playerPosition = Movement.instance.transform.position;
        Vector2 _2dPosition = new Vector2();
        _2dPosition.x = transform.position.x;
        _2dPosition.y = transform.position.y;

        if (_lockRotation == false)
        {
            RotateTowardPlayer();
            RaycastHit2D _rayHit = Physics2D.Raycast(transform.position, -transform.up);
            if (_rayHit.collider != null)
            {
                if (_rayHit.collider.CompareTag("Player"))
                { 
                    _lockRotation = true;
                    _attackTime = Time.time + _attackDelay;
                }
            }
        }
        
        
	    
        if (_lockRotation == true && Time.time > _attackTime)
        {
            _tinyLaserTransform.gameObject.SetActive(false);
            _bigLaserTransform.gameObject.SetActive(true);

        }

        if (_lockRotation == true && Time.time > _attackTime + _attackLength)
        {
            _lockRotation = false;
            _tinyLaserTransform.gameObject.SetActive(true);
            _bigLaserTransform.gameObject.SetActive(false);
            
        }
	}


    private void RotateTowardPlayer()
    {

        Vector2 temp = transform.position;
        _rotationDirection = temp - _playerPosition;
        _rotationDirection.Normalize();
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, _rotationDirection), Time.deltaTime * _rotationSpeed);
        

    }

}
