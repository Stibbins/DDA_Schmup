using UnityEngine;
using System.Collections;

public class E_Laser : MonoBehaviour {

    private Texture2D _laserSprite;


    private bool _lockRotation;
    private float _rotationSpeed;
    private float _attackDelay;
    private float _attackDamage;
    private Vector2 _playerPosition;
    private float _attackTime;
    



	// Use this for initialization
	void Awake () 
    {
        _lockRotation = false;
        _attackTime = Time.time - 1;
	}
	
	// Update is called once per frame
	void Update () {
        _playerPosition = Movement.instance.transform.position;
        Vector2 _2dPosition = new Vector2();
        _2dPosition.x = transform.position.x;
        _2dPosition.y = transform.position.y;
        
        
        transform.LookAt(_playerPosition);
        RaycastHit2D _rayHit = Physics2D.Raycast(transform.position, Vector2.up);
        if (_rayHit != null && _rayHit.transform.CompareTag("Player"))
        {
            _lockRotation = false;
            _attackTime = Time.time + _attackDelay;
        }
	    

	}
}
