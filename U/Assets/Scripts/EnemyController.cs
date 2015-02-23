using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private float health;

    //TODO: Movement patterns
    //TODO: Attack patterns
    //TODO: List tracking thing

	void Start ()  
    {
	
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

