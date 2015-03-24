using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour {

    private float _speed = 2;
    private float _rotationSpeed = 1;

    private Vector2 _playerPosition;




	void Awake () 
    {
        
	}
	
	
	void FixedUpdate () 
    {
        _playerPosition = Movement.instance.transform.position;
        Vector2 _rotationDirection;
        Vector2 temp = transform.position;
        _rotationDirection = _playerPosition - temp;
        _rotationDirection.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, _rotationDirection), Time.time * _rotationSpeed);

        transform.Translate(-transform.up * _speed * Time.deltaTime);
	}
}
