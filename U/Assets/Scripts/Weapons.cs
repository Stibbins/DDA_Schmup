using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

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
}
