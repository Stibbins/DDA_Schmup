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
    public EnemyController enemyPrefab;
    public int spawnCount;
    public float spawnDelay;

    private float _spawnTimer;
    public Transform waveSpawn;
    private Transform[] _transformArray;


    //---- END WAVE SPA

    //---- WAVE MANAGEMENT
    private List<EnemyController> _enemyList = new List<EnemyController>();
    public bool _waveActive {get; private set;}
    //---- END WAVE MANAGEMENT

    void Awake ()
    {
        _waveActive = true;
        _spawnTimer = Time.time + spawnDelay;
        Random.seed = (int)Time.time;
        _transformArray = waveSpawn.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        Debug.Log("Time: " + Time.time);
        Debug.Log("SpawnTimer: " + _spawnTimer);
        if (_waveActive)
        {
            if (Time.time > _spawnTimer)
            {
                
                for (int i = 0; i < spawnCount; i++)
                {
                    EnemyController eC = (EnemyController) Instantiate(enemyPrefab, _transformArray[i].position, Quaternion.identity);
                    TrackEnemy(eC);
                }
                _spawnTimer = Time.time + spawnDelay;
            }

            for (int i = 0; i < _enemyList.Count; i++)
            {
                if (!_enemyList[i]._alive)
                {
                    _enemyList.RemoveAt(i);
                    Destroy(_enemyList[i].gameObject);
                }
            }
            if (_enemyList.Count < 1)
            {
                _waveActive = false;
            }

            
        }
       
    }


    public void TrackEnemy(EnemyController enemy)
    {
        _enemyList.Add(enemy);
    }

    public bool IsWaveActive()
    {
        return _waveActive;
    }

}
