using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Path;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private float health;
    [SerializeField]


    public bool _alive { get; private set; }

    

    //TODO: Movement patterns
    //TODO: Attack patterns
    //TODO: List tracking thing

	void Awake ()  
    {
        _alive = true;

	}
	

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShot"))
        {
            PlayerDamage playerShot = other.GetComponent<PlayerDamage>();
            health -= playerShot.damageAmount;
            playerShot.DestroyInstance();
            if (health <= 0)
            {
                _alive = false;
                //_waveController.UntrackEnemy(transform.gameObject);
                //Destroy(gameObject);
            }
        }
    }



}

