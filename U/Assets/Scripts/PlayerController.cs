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
    private float _currentHealth;
   


    //---------------

	void Awake () 
    {
        _currentHealth = playerMaxHealth;
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
