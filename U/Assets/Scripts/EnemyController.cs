using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Path;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private float health;
    [SerializeField]
    private float tweenDuration;
    private HOPath _hoPath;



    //TODO: Movement patterns
    //TODO: Attack patterns
    //TODO: List tracking thing

	void Start ()  
    {

        _hoPath = GameObject.Find("PathStorage").GetComponent<HOPath>();
        HOTween.To(transform, tweenDuration, new TweenParms()
                            .Prop("position", _hoPath.MakePlugVector3Path().Is2D())
                            .Ease(EaseType.Linear)
                            );
	}
	

	void Update () {

        if (health <= 0)
        {
            Destroy(gameObject);
            //TODO: Remove from list
        }
	
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShot"))
        {
            PlayerDamage playerShot = other.GetComponent<PlayerDamage>();
            health -= playerShot.damageAmount;
            playerShot.DestroyInstance();
        }
    }




}

