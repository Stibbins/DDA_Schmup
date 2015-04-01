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

    public EnemyController missilePrefab;
    public EnemyController laserPrefab;
    public EnemyController leechPrefab;
    public int spawnCount;
    private int _startingSpawnCount;
    public float spawnDelay;

    public bool _gameActive;
    private bool _spawnActive;
    private float _spawnTimer;
    public Transform waveSpawn;
    private Transform[] _transformArray;
    private EnemyController[] _waveArray;


    //---- WAVE MANAGEMENT
    private List<EnemyController> _enemyList = new List<EnemyController>();
    public bool _waveActive {get; private set;}
    public int _currentWave { get; private set; }


    // ---- OTHER
    public int numberOfWaves;
    private BalanceSystem _balanceSystem;
    private PlayerController _playerController;


    void Awake ()
    {
        _balanceSystem = BalanceSystem.instance;
        _playerController = PlayerController.instance;
        _startingSpawnCount = spawnCount;
        SetStartingValues();
        
        Random.seed = (int)Time.time;

        _transformArray = waveSpawn.GetComponentsInChildren<Transform>();
        
        
    }

    void Update()
    {
        if (_currentWave >= numberOfWaves)
        {
            _gameActive = false;
        }

        if (_playerController.currentHealth <= 0)
        {
            _gameActive = false;
        }

        if (_waveActive && _gameActive)
        {
            
            if (_spawnActive)
            {

                SetSpawns();

                //Make sure the number of spawns does not exceed possible
                //spawn locations
                if (_transformArray.Length < spawnCount)
                {
                    Debug.Log("Too many enemies!");
                }

                

                //Spawn the wave
                for (int i = 0; i < spawnCount; i++)
                {
                    EnemyController eC = (EnemyController) Instantiate(_waveArray[i], _transformArray[i].position, Quaternion.identity);
                    TrackEnemy(eC);
                    
                }
                _balanceSystem.SetValues();
                
                
                // -------- Wave has spawned
                _currentWave++;
                _spawnActive = false;
                
                //Reset data collections for new wave
                //Both null-checked singletons, it's fine
                Movement.instance.NewWave();
                Weapons.instance.NewWave();
                PlayerController.instance.NewWave();
                
            }


            // Wave has been defeated
            if (_enemyList.Count < 1)
            {
                _waveActive = false;
                _spawnTimer = Time.time + spawnDelay; //Delay until next wave

                //Update difficulty
                //spawnCount += _balanceSystem.ModifyEnemySpawnAmount();
                _balanceSystem.ModifyEnemyValues();
                
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
            SetStartingValues();            
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

    private void SetStartingValues ()
    {
        _gameActive = true;
        _waveActive = false;
        _spawnActive = true;
        _spawnTimer = Time.time + spawnDelay;
        _balanceSystem.ResetValues();
        _playerController.ResetValues();
        spawnCount = _startingSpawnCount;
        _currentWave = 0;
    }

    private void SetSpawns()
    {
        if (_currentWave == 0)
        {
            _waveArray = new EnemyController[]
            {
                missilePrefab,
                laserPrefab
            };
        }

        if (_currentWave == 1)
        {
            _waveArray = new EnemyController[]
            {
                leechPrefab,
                laserPrefab
            };
        }


        if (_currentWave == 2)
        {
            _waveArray = new EnemyController[]
            {
                missilePrefab,
                missilePrefab,
                laserPrefab
            };
        }

        if (_currentWave == 3)
        {
            _waveArray = new EnemyController[]
            {
                laserPrefab,
                laserPrefab,
                laserPrefab,
                laserPrefab
            };
        }

        if (_currentWave == 4)
        {
            _waveArray = new EnemyController[]
            {
                missilePrefab,
                missilePrefab,
                missilePrefab
            };
        }

        if (_currentWave == 5)
        {
            _waveArray = new EnemyController[]
            {
                laserPrefab,
                missilePrefab,
                laserPrefab,
                missilePrefab,
                leechPrefab,
                leechPrefab
            };
        }
        spawnCount = _waveArray.Length;
    }
}
