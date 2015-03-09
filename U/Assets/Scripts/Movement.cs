using   UnityEngine;
using   System.Collections;

public class Movement : MonoBehaviour {

 
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;
   
    
    [SerializeField]
    private float topSpeed;
    [SerializeField]
    private AnimationCurve accelerationCurve;
    [SerializeField]
    private float accelerationFactor;
    [SerializeField]
    private AnimationCurve frictionCurve;
    [SerializeField]
    private float frictionFactor;
    
    private float _currentSpeed;
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    //---- FUZZY IMPLEMENTATION

    private Vector3 _previousPosition;
    private float _positionTimer;
    public float _waveDelta = 0;

    public float _waveTimeStart;

    //---- END OF FUZZY IMPLEMENTATION

	void Awake  () 
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _waveTimeStart = Time.time;
    }
	
	
	void FixedUpdate () 
    {
        if (Time.time > _positionTimer + 1)
        {
            _previousPosition = transform.position;
            _positionTimer = Time.time;
        }
        Vector3 acceleration = Vector3.zero;
        if (Input.GetKey(keyUp))
            acceleration += _transform.up;
        if (Input.GetKey(keyDown))
            acceleration -= _transform.up;
        if (Input.GetKey(keyRight))
            acceleration += _transform.right;
        if (Input.GetKey(keyLeft))
            acceleration -= _transform.right;

        acceleration.z = 0;
        Vector3 normalizedVector = _rigidbody.velocity.normalized;
        float velocityMagnitude = _rigidbody.velocity.magnitude / topSpeed;
        acceleration *= accelerationCurve.Evaluate(velocityMagnitude) * accelerationFactor;

        _rigidbody.AddForce(acceleration);
        _rigidbody.AddForce(frictionCurve.Evaluate(velocityMagnitude) * frictionFactor * -normalizedVector);
        //_transform.Translate(acceleration);
        _waveDelta += Vector3.Distance(_previousPosition, transform.position);
        
    }
}
