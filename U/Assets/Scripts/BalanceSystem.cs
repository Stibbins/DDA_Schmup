using UnityEngine;
using System.Collections;

public class BalanceSystem : MonoBehaviour {

    //Singleton
    private static BalanceSystem _instance;
    public static BalanceSystem instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BalanceSystem>();
            }
            return _instance;
        }
    }



    private Movement _playerMovement;
    private PlayerController _playerController;

    ///-----STARTING VALUES!
    public float sv_MissileRate;
    public float sv_MissileArc;
    public float sv_LaserRotation;
    public float sv_LaserDelay;
    public float sv_LeechSpeed;
    public float sv_LeechDamage;

    //----- Not Starting Values

    private float _MissileRate;
    private float _MissileArc;
    private float _LaserRotation;
    private float _LaserDelay;
    private float _LeechSpeed;
    private float _LeechDamage;


    //---- FUZZY IMPLEMENTATION
    // MOVEMENT
    //public float _enemySpawnerMod;
    private float _playerDelta;
    private float _deltaRate;
    private float _maxDelta;
    private float _relativeDelta;
    private float _playerTime;
   
    //HEALTH
    private float _healthLoss;
    private float _maxHealth;
    private float _currentHealth;
    private float _previousHealth;


	void Awake () 
    {
        _MissileRate = sv_MissileRate;
        _MissileArc = sv_MissileArc;
        _LaserRotation = sv_LaserRotation;
        _LaserDelay = sv_LaserDelay;
        _LeechSpeed = sv_LeechSpeed;
        _LeechDamage = sv_LeechDamage;
        _playerMovement = Movement.instance;
        _playerController = PlayerController.instance;
        _previousHealth = _playerController.playerMaxHealth;
        _currentHealth = _previousHealth;
     
	}
	

    //Part of the difficulty adjustment
    public int ModifyEnemySpawnAmount()
    {
        _playerDelta = _playerMovement._waveDelta;
        //_deltaRate = _playerMovement._deltaSampleRate;
        _playerTime = Time.time - _playerMovement._waveTimeStart;
        _maxDelta = _playerMovement.speedModifier * _playerTime;
        
        
        _relativeDelta = _playerDelta / _maxDelta;

        if (_relativeDelta < 0.5f)
        {
            Debug.Log("Enemy amount increased");
            return 1;
        }
        
        else
        {
            Debug.Log("Enemy amount decreased");
            return -1; 
        }
        
    }

    public void ModifyEnemyValues()
    {
        _previousHealth = _currentHealth;
        _healthLoss = _playerController.waveHealthDelta;
        _maxHealth = _playerController.playerMaxHealth;
        _currentHealth = _playerController.currentHealth;

        //_valueModifier = (_currentHealth / _maxHealth) + (_healthLoss / _previousHealth);

        //LOW HEALTH
        if (_currentHealth < _maxHealth/2)
        {
            if (_healthLoss >= _previousHealth/2)
            {
                _LaserRotation *= 0.5f;
                _LaserDelay *= 1.5f;
                _MissileArc *= 1.5f;
                _MissileRate *= 1.5f;
                _LeechSpeed *= 0.65f;
                _LeechDamage *= 0.7f;
            }

            if (_healthLoss < _previousHealth/2 && _healthLoss > 0)
            {
                _LaserRotation *= 1.1f;
                _LaserDelay *= 0.9f;
                _MissileArc *= 0.9f;
                _MissileRate *= 0.9f;
                _LeechSpeed *= 1.05f;
                _LeechDamage *= 1.1f;
            }
            if (_healthLoss <= 0)
            {
                _LaserRotation *= 1.2f;
                _LaserDelay *= 0.9f;
                _MissileArc *= 0.85f;
                _MissileRate *= 0.95f;
                _LeechSpeed *= 1.05f;
                _LeechDamage *= 1.2f;
            }
        }

        //HIGH HEALTH
        if (_currentHealth >= _maxHealth / 2)
        {
            if (_healthLoss >= _previousHealth / 2)
            {
                _LaserRotation *= 0.9f;
                _LaserDelay *= 1.1f;
                _MissileArc *= 1.1f;
                _MissileRate *= 1.1f;
                _LeechSpeed *= 0.95f;
                _LeechDamage *= 0.9f;
            }

            if (_healthLoss < _previousHealth / 2 && _healthLoss > 0)
            {
                _LaserRotation *= 1.2f;
                _LaserDelay *= 0.8f;
                _MissileArc *= 0.9f;
                _MissileRate *= 0.9f;
                _LeechSpeed *= 1.1f;
                _LeechDamage *= 1.2f;
            }
            if (_healthLoss <= 0)
            {
                _LaserRotation *= 1.4f;
                _LaserDelay *= 0.7f;
                _MissileArc *= 0.8f;
                _MissileRate *= 0.7f;
                _LeechSpeed *= 1.15f;
                _LeechDamage *= 1.5f;
            }
        }

        
        

    }

    public void PlayerDeathMod()
    {
        _LaserRotation *= 0.5f;
        _LaserDelay *= 1.5f;
        _MissileArc *= 1.2f;
        _MissileRate *= 1.4f;
        _LeechSpeed *= 0.7f;
        _LeechDamage *= 0.7f;
    }

    public void SetValues()
    {
        E_Laser._rotationSpeed = _LaserRotation;
        E_Laser._attackDelay = _LaserDelay;
        E_Missile._arcSize = _MissileArc;
        E_Missile._fireRate = _MissileRate;
        E_Leech.movementSpeed = _LeechSpeed;
        E_Leech.leechDamage = _LeechDamage;
        Debug.Log("Enemy Values modified");
    }

 

}
