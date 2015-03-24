using UnityEngine;
using System.Collections;

public class E_Laser : MonoBehaviour {

    public Transform tinyLaser;
    public Transform bigLaser;


    private bool _lockRotation;
    private float _rotationSpeed;
    private float _attackDelay;
    private float _attackDamage;
    private Vector2 _playerPosition;
    private float _attackTime;
    private float _attackLength = 1f;

    private Vector2 _rotationDirection;


	void Awake () 
    {
        _lockRotation = false;
        _attackTime = Time.time - 1;
        _rotationSpeed = 10;
        _attackDelay = 1;
	}
	

	void Update () {
        _playerPosition = Movement.instance.transform.position;
        Vector2 _2dPosition = new Vector2();
        _2dPosition.x = transform.position.x;
        _2dPosition.y = transform.position.y;

        if (_lockRotation == false)
        {
            RotateTowardPlayer();
            RaycastHit2D _rayHit = Physics2D.Raycast(transform.position, transform.up);
            Debug.DrawRay(transform.position, transform.up);
            if (_rayHit.collider != null)
            {
                if (_rayHit.collider.CompareTag("Player"))
                { 
                    _lockRotation = true;
                    _attackTime = Time.time + _attackDelay;
                }
            }
        }
        
        
	    
        if (_lockRotation == true && Time.time > _attackTime && Time.time < _attackTime + _attackLength)
        {
            //Display large laser
            Debug.Log("LazoR!");
            _lockRotation = false;
        }
	}


    private void RotateTowardPlayer()
    {

        Vector2 temp = transform.position;
        _rotationDirection = _playerPosition - temp;
        _rotationDirection.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, _rotationDirection), Time.time * _rotationSpeed);
        

    }

}
