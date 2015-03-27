using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WaveController : MonoBehaviour {

    /*
     * Spawns enemies at wave start 
     * 
     * Maintains a list of currently active enemies. 
     * When there are none left, trigger end-of-wave behaviour.
     */
    //---- WAVE SPAWNING

    private static WaveController _instance;
    public static WaveController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<WaveController>();
            }
            return _instance;
        }
    }

    public EnemyController enemyPrefab;
    public int spawnCount;
    public float spawnDelay;

    private bool _gameActive;
    private bool _spawnActive;
    private float _spawnTimer;
    public Transform waveSpawn;
    private Transform[] _transformArray;


    //---- WAVE MANAGEMENT
    private List<EnemyController> _enemyList = new List<EnemyController>();
    public bool _waveActive {get; private set;}
    public int _currentWave { get; private set; }


    // ---- OTHER
    public int numberOfWaves;
    private BalanceSystem _balanceSystem;


    void Awake ()
    {
        _gameActive = true;
        _waveActive = false;
        _spawnActive = true;
        _spawnTimer = Time.time + spawnDelay;
        Random.seed = (int)Time.time;
        _transformArray = waveSpawn.GetComponentsInChildren<Transform>();
        _currentWave = 0;
        _balanceSystem = BalanceSystem.instance;
    }

    void Update()
    {
        if (_currentWave >= numberOfWaves)
        {
            _gameActive = false;
        }

        

        if (_waveActive && _gameActive)
        {
            
            if (_spawnActive)
            {
                
                //Make sure the number of spawns does not exceed possible
                //spawn locations
                if (_transformArray.Length < spawnCount)
                {
                    spawnCount = _transformArray.Length;
                }

                //Spawn the wave
                for (int i = 0; i < spawnCount; i++)
                {
                    EnemyController eC = (EnemyController) Instantiate(enemyPrefab, _transformArray[i].position, Quaternion.identity);
                    TrackEnemy(eC);
                    //eC.SetValues(); //Function not implemented yet
                }

                
                
                // -------- Wave has spawned
                _currentWave++;
                _spawnActive = false;
                
                //Reset data collections for new wave
                //Both null-checked singletons, it's fine
                Movement.instance.NewWave();
                Weapons.instance.NewWave();
                
            }


            // Wave has been defeated
            if (_enemyList.Count < 1)
            {
                _waveActive = false;
                _spawnTimer = Time.time + spawnDelay; //Delay until next wave

                //Update difficulty
                spawnCount += _balanceSystem.ModifyEnemySpawnAmount();
                
            }

            
        }

        //----------WAVE IS NOT ACTIVE
        if (!_waveActive && Time.time > _spawnTimer)
        {
            _waveActive = true;
            _spawnActive = true;

        }

        if (!_gameActive && Input.GetKeyDown(KeyCode.R))
        {
            _currentWave = 0;
            _gameActive = true;
        }
    }


    public void TrackEnemy(EnemyController enemy)
    {
        _enemyList.Add(enemy);
    }

    public void UnTrackEnemy (EnemyController enemy)
    {
        _enemyList.Remove(enemy);
    }


}
