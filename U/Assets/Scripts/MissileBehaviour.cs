using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour {

    public float health;


    private float _speed = 3;
    private float _rotationSpeed = 2;

    private Vector2 _playerPosition;
    private Rigidbody2D _rigidbody;




	void Awake () 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	
	void FixedUpdate () 
    {
        _playerPosition = Movement.instance.transform.position;
        Vector2 _rotationDirection;
        Vector2 temp = transform.position;
        _rotationDirection = _playerPosition - temp;
        _rotationDirection.Normalize();
        //transform.Translate(-transform.up * _speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, _rotationDirection), Time.time * _rotationSpeed);
        _rigidbody.AddForce(_rotationDirection * _speed);
        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShot"))
        {
            PlayerDamage playerShot = other.GetComponent<PlayerDamage>();
            health -= playerShot.damageAmount;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
