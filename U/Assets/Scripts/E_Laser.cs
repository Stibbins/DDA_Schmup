using UnityEngine;
using System.Collections;

public class E_Laser : MonoBehaviour {

    public Transform tinyLaser;
    public Transform bigLaser;


    private bool _lockRotation;
    private float _rotationSpeed;
    private float _attackDelay;
    private float _attackDamage;
    private Vector2 _playerPosition;
    private float _attackTime;
    private float _attackLength = 1;
    



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

        if (_lockRotation == false)
        {
            RotateTowardPlayer();
            RaycastHit2D _rayHit = Physics2D.Raycast(transform.position, Vector2.up);
            if (_rayHit != null && _rayHit.transform.CompareTag("Player"))
            {
                _lockRotation = true;
                _attackTime = Time.time + _attackDelay;
            }
        }
        
        
	    
        if (_lockRotation == true && Time.time > _attackTime && Time.time < _attackTime + _attackLength)
        {
            //Display large laser
            //Own prefab?
            Debug.Log("LazoR!");
        }
	}


    private void RotateTowardPlayer()
    {
        Vector2 _ownVector = transform.position;
        Vector2 _selfToPlayerVector = _playerPosition - _ownVector;
        float angle = Vector2.Angle(-transform.up, _selfToPlayerVector);

        Vector3 cross = Vector3.Cross(-transform.up, _selfToPlayerVector);
        if (cross.z > 0)
            angle = 360 - angle;

        if (0 < angle && angle < 180)
        {
            //Rotate.... right?
           // transform.Rotate(Vector3.forward, Mathf.SmoothDampAngle(angle, ))
        }

        if ( 180 < angle && angle < 360)
        {
            //Rotate... left?
        }
    }

}
