using UnityEngine;
using System.Collections;

public class BalanceSystem : MonoBehaviour {


    ///-----STARTING VALUES!
    [SerializeField]
    private float sv_MissileRate;
    [SerializeField]
    private float sv_MissileArc;

    //-------END OF STARTING VALUES!



    //------Structs holding values for different enemy types
    private struct S_MissileValues
    {
        public float _missileRate;
        public float _missileArc;
        public S_MissileValues (float rate, float arc)
        {
            _missileRate = rate;
            _missileArc = arc;
        }
    }

    //---- End of structs

    private S_MissileValues _missileValues;


	void Start () 
    {
      S_MissileValues _missileValues = new S_MissileValues(sv_MissileRate, sv_MissileArc);
	}
	
	
	void Update () 
    {
        
	}


    public S_MissileValues GetMissileValues()
    {
        return  _missileValues;
    }
}
