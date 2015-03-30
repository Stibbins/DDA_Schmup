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
    private float _valueModifier;


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
                _valueModifier -= 1;
            }

            if (_healthLoss < _previousHealth/2 && _healthLoss > 0)
            {
                _valueModifier += 0.5f;
            }
            if (_healthLoss <= 0)
            {
                _valueModifier += 0.75f;
            }
        }

        //HIGH HEALTH
        if (_currentHealth >= _maxHealth / 2)
        {
            if (_healthLoss >= _previousHealth / 2)
            {
                _valueModifier -= 0.5f;
            }

            if (_healthLoss < _previousHealth / 2 && _healthLoss > 0)
            {
                _valueModifier += 1f;
            }
            if (_healthLoss <= 0)
            {
                _valueModifier += 2f;
            }
        }

        _LaserRotation += _valueModifier;
        _LaserDelay -= _valueModifier;
       // _MissileArc += _valueModifier/10;
        _MissileRate -= _valueModifier;
        _LeechSpeed += _valueModifier;
        _LeechDamage += _valueModifier;
        

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
