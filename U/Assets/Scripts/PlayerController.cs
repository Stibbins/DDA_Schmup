using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Singleton
    private static PlayerController _instance;
    public static PlayerController instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerController>();
            }
            return _instance;
        }
    }


    public float playerMaxHealth;
    public float _currentHealth
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            _currentHealth = value;
        }
    }


    //---------------

	void Awake () 
    {
	
	}

	void Update () 
    {
	
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("EnemyShot"))
        {
            EnemyDamage damageClass = other.GetComponent<EnemyDamage>();
            _currentHealth -= damageClass.damageAmount;
            Destroy(other.gameObject);
            Debug.Log("Current health: " + _currentHealth);
        }
    }
}
