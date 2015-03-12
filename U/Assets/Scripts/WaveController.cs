﻿using UnityEngine;
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

    private bool _spawnActive;
    private float _spawnTimer;
    public Transform waveSpawn;
    private Transform[] _transformArray;



    //---- WAVE MANAGEMENT
    private List<EnemyController> _enemyList = new List<EnemyController>();
    public bool _waveActive {get; private set;}
    private int _currentWave;


    // ---- OTHER

    private BalanceSystem _balanceSystem;


    void Awake ()
    {
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
        if (_waveActive)
        {
            
            if (_spawnActive)
            {
                //Reset data collections for new wave
                //Both null-checked singletons, it's fine
                Movement.instance.NewWave();
                Weapons.instance.NewWave();



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
                
            }


            // Wave has been defeated
            if (_enemyList.Count < 1 && _currentWave > 0)
            {
                _waveActive = false;
                _spawnTimer = Time.time + spawnDelay; //Delay until next wave

                //Update difficulty
                _balanceSystem.ModifyEnemySpawnAmount();
                
            }

            
        }

        //----------WAVE IS NOT ACTIVE
        if (!_waveActive && Time.time > _spawnTimer)
        {
            _waveActive = true;
            _spawnActive = true;
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