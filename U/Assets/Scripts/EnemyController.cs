using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private float health;
  

    private Transform _playerTransform;

    //TODO: Movement patterns
    //TODO: Attack patterns
    //TODO: List tracking thing

	void Start ()  
    {
        _playerTransform = GameObject.Find("Player").transform;
        HOTween.To(transform, 3, "position", _playerTransform.position);
	}
	

	void Update () {

        if (health <= 0)
        {
            Destroy(gameObject);
            //TODO: Remove from list
        }
	
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShot"))
        {
            PlayerDamage playerShot = other.GetComponent<PlayerDamage>();
            health -= playerShot.damageAmount;
            playerShot.DestroyInstance();
        }
    }




}

