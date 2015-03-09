using UnityEngine;
using System.Collections;

public class BalanceSystem : MonoBehaviour {


    private Movement _playerMovement;

    ///-----STARTING VALUES!
    [SerializeField]
    private float sv_MissileRate;
    [SerializeField]
    private float sv_MissileArc;

    //-------END OF STARTING VALUES!



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


	void Start () 
    {
        S_MissileValues _missileValues = new S_MissileValues(sv_MissileRate, sv_MissileArc);
        _playerMovement = GameObject.Find("Player").GetComponent<Movement>();
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
    }




}
