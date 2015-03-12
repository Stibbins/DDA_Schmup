using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

    private static Weapons _instance;
    public static Weapons instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Weapons>();
            }
            return _instance;
        }
    }

    public KeyCode keyFire;

    [SerializeField]
    private Transform cannonTransform;
    [SerializeField]
    private Object bulletPrefab;
    [SerializeField]
    private float fireRate;


    private float lastFire;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKey(keyFire) && Time.time > lastFire + fireRate)
        {
            lastFire = Time.time;
            Instantiate(bulletPrefab, cannonTransform.position, Quaternion.identity);
        }
	}

    public void NewWave()
    {
        Debug.Log("NewWave triggered in Weapons");
    }
}
