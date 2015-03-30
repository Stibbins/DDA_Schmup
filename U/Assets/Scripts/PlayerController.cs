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
    public float currentHealth;
    public float waveHealthDelta;


	void Awake () 
    {
        currentHealth = playerMaxHealth;
        waveHealthDelta = 0f;
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("EnemyShot"))
        {
            EnemyDamage damageClass = other.GetComponent<EnemyDamage>();
            currentHealth -= damageClass.damageAmount;
            waveHealthDelta += damageClass.damageAmount;
            Destroy(other.gameObject);
            Debug.Log("Current health: " + currentHealth);
        }

        
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("EnemyLaser"))
        {
            EnemyDamage damageClass = other.GetComponent<EnemyDamage>();
            LaserTimer laserClass = other.GetComponent<LaserTimer>();
            if (Time.time > laserClass.nextDamageTime)
            {
                currentHealth -= damageClass.damageAmount;
                waveHealthDelta += damageClass.damageAmount;
                laserClass.SetNewDamageTick();
                Debug.Log("Current health: " + currentHealth);
            }

        }

        if (other.CompareTag("EnemyLeech"))
        {
            E_Leech leechClass = other.GetComponent<E_Leech>();
            if (Time.time > leechClass.nextDamageTime)
            {
                currentHealth -= E_Leech.leechDamage;
                waveHealthDelta = +E_Leech.leechDamage;
                leechClass.SetNewDamageTick();
                Debug.Log("Current health: " + currentHealth); 
            }
                       
        }
    }

    public void NewWave()
    {
        waveHealthDelta = 0;
    }
}
