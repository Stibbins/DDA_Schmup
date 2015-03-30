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
    
    public float _enemySpawnerMod;
    private float _playerDelta;
    private float _deltaRate;
    private float _maxDelta;
    private float _relativeDelta;
    private float _playerTime;
   


	void Awake () 
    {
        _MissileRate = sv_MissileRate;
        _MissileArc = sv_MissileArc;
        _LaserRotation = sv_LaserRotation;
        _LaserDelay = sv_LaserDelay;
        _LeechSpeed = sv_LeechSpeed;
        _LeechDamage = sv_LeechDamage;
        _playerMovement = Movement.instance;
     
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

    public void SetValues()
    {
        E_Laser._rotationSpeed = _LaserRotation;
        E_Laser._attackDelay = _LaserDelay;
        E_Missile._arcSize = _MissileArc;
        E_Missile._fireRate = _MissileRate;
        E_Leech.movementSpeed = _LeechSpeed;
        E_Leech.leechDamage = _LeechDamage;
    }

}
