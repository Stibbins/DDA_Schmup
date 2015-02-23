using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

    [SerializeField]
    public float damageAmount;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DestroyInstance ()
    {
        Destroy(gameObject);
        return;
    }

}
