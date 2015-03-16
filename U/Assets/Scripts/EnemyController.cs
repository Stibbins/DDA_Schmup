using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour {

    [SerializeField]
    private float health;
    public bool _alive { get; private set; }

    private static WaveController _waveController;


    void Awake ()  
    {
        if (_waveController == null)
        {
            _waveController = GameObject.Find("GameLogic").GetComponent<WaveController>();
        }
        _alive = true;
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
                _alive = false;
                _waveController.UnTrackEnemy(this);
                Destroy(gameObject);
            }
        }
    }

    public void SetValues()
    {
        //TODO
    }

}

