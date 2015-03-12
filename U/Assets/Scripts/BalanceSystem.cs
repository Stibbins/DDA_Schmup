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


    //------Structs holding values for different enemy types
    public struct S_MissileValues
    {
        public float _missileRate;
        public float _missileArc;
        public S_MissileValues (float rate, float arc)
        {
            _missileRate = rate;
            _missileArc = arc;
        }
    }

    //---- End of structs

    public S_MissileValues _missileValues;

    //---- FUZZY IMPLEMENTATION
    
    public float _enemySpawnerMod;


	void Awake () 
    {
        S_MissileValues _missileValues = new S_MissileValues(sv_MissileRate, sv_MissileArc);
        _playerMovement = Movement.instance;
	}
	
	
	void Update () 
    {
        
	}


    public S_MissileValues GetMissileValues()
    {
        return  _missileValues;
    }

    public void ModifyEnemySpawnAmount()
    {
        float _playerDelta = _playerMovement._waveDelta;
        float _playerTime = Time.time - _playerMovement._waveTimeStart;
        Debug.Log("Enemy amount updated");
    }




}
