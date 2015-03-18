using   UnityEngine;
using   System.Collections;

public class Movement : MonoBehaviour {


    //Singleton implementation
    private static Movement _instance;   
    public static Movement instance
    {
        get
        {
            if (_instance == null) 
            {
                _instance = GameObject.FindObjectOfType<Movement>();
            }
            return _instance;
        }
    }
 
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;

    public float speedModifier;

    /*
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
    */
    private float _currentSpeed;
    private Transform _transform;
    private float _maxDistTravelled;

    //---- FUZZY IMPLEMENTATION

    private Vector3 _previousPosition;
    private float _positionTimer;
    public float _waveDelta { get; private set; }
    public float _maxPossibleDelta { get; private set; }
    public float _waveTimeStart { get; private set; }
    public float _deltaSampleRate { get; private set; }

    //---- END OF FUZZY IMPLEMENTATION

	void Awake () 
    {
        _transform = GetComponent<Transform>();
        _waveTimeStart = Time.time;
        _positionTimer = Time.time;
        _deltaSampleRate = 1;
        _maxPossibleDelta = 0;
    }
	
	
	void FixedUpdate () 
    {
        // -- Store position every second
        if (Time.time > _positionTimer + _deltaSampleRate)
        {
            float _waveDistance = Vector3.Distance(_previousPosition, transform.position);
            _waveDelta += _waveDistance;
            _previousPosition = transform.position;
            _positionTimer = Time.time;
        }

               
        // -- Get input
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
        transform.Translate(acceleration.normalized* speedModifier * Time.deltaTime);

    }




    public void NewWave ()
    {
        _waveDelta = 0;
        _waveTimeStart = Time.time;
    }
}
