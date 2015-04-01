using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour {

    public float health;


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
        transform.Translate(-transform.up * _speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, _rotationDirection), Time.time * _rotationSpeed);

        
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
