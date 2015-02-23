using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

    [SerializeField]
    private Object enemyPrefab;
    [SerializeField]
    private int spawnCount;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private float spawnJitterHorizontal;
    [SerializeField]
    private float spawnJitterVertical;

    private float _spawnTimer;
    private Transform[] _transformArray;
    
        
    // Use this for initialization
    void Start()
    {
        _spawnTimer = Time.time + spawnDelay;
        Random.seed = (int)Time.time;
        _transformArray = GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if (Time.time > _spawnTimer)
        {
            _spawnTimer = Time.time + spawnDelay;
            Vector2 _spawnLocation = transform.position;
            _spawnLocation.x = Random.Range(_spawnLocation.x - spawnJitterHorizontal, _spawnLocation.x + spawnJitterHorizontal);
            if (spawnJitterVertical > 0)
                _spawnLocation.y = Random.Range(_spawnLocation.y - spawnJitterVertical, _spawnLocation.y + spawnJitterVertical);
            foreach (Transform spawnPoint in _transformArray)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }

        }
	}
}
