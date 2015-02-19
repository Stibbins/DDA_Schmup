using   UnityEngine;
using   System.Collections;

public class Movement : MonoBehaviour {

 
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;
   
    
    [SerializeField]
    private float _TopSpeed;
    [SerializeField]
    private AnimationCurve _Acceleration;
    [SerializeField]
    private float _AccelerationFactor;
    

	
	void Start () 
    {
	
        
    }
	
	
	void Update () 
    {
	
    }
}
