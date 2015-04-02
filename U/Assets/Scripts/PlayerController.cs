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

    private UI_PlayerDamage _damageDisplay;


	void Awake () 
    {
        currentHealth = playerMaxHealth;
        waveHealthDelta = 0f;
        //_damageDisplay = UI_PlayerDamage.instance;
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("EnemyShot"))
        {
            EnemyDamage damageClass = other.GetComponent<EnemyDamage>();
            currentHealth -= damageClass.damageAmount;
            waveHealthDelta += damageClass.damageAmount;
            Destroy(other.gameObject);
            //_damageDisplay.UpdateText(damageClass.damageAmount);
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
                //_damageDisplay.UpdateText(damageClass.damageAmount);
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
                //_damageDisplay.UpdateText(E_Leech.leechDamage); 
            }
                       
        }
    }

    public void NewWave()
    {
        waveHealthDelta = 0;
    }

    public void ResetValues()
    {
        waveHealthDelta = 0;
        currentHealth = playerMaxHealth;
    }
}
