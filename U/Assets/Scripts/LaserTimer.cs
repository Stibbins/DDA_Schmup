using UnityEngine;
using System.Collections;

public class LaserTimer : MonoBehaviour {

    public float nextDamageTime;
    public float damageTickDelay;

    void Awake ()
    {
        nextDamageTime = Time.time;
    }

    public void SetNewDamageTick()
    {
        nextDamageTime = Time.time + damageTickDelay;
    }
}
