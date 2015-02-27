using UnityEngine;
using System.Collections;

public class E_Missile : MonoBehaviour {

    [SerializeField]
    private Object missilePrefab;
    [SerializeField]
    private BalanceSystem _balanceSystem;
    
    [SerializeField]
    private float _arcSize;
    [SerializeField]
    private float _fireRate;
    private float _lastShot;
    private Transform _playerTransform;
	
	void Start () 
    {
        _playerTransform = GameObject.Find("Player").transform;
        _lastShot = Time.time;
	}
	
	
	void Update () 
    {
        Vector2 _selfToPlayerVector = (_playerTransform.position - transform.position).normalized;
        if ((Vector2.Dot(_selfToPlayerVector, -transform.up) > _arcSize) && Time.time > _lastShot + _fireRate)
        {
            _lastShot = Time.time;
            Debug.Log("Shot fired");
        }
	}
}
