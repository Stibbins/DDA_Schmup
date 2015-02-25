using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Path;

public class TweenTestScript : MonoBehaviour {

    [SerializeField]
    private float tweenDuration;
    private HOPath _hoPath;


	void Start () 
    {
        _hoPath = GameObject.Find("PathStorage").GetComponent<HOPath>();
        HOTween.To(transform, tweenDuration, new TweenParms()
                            .Prop("position", _hoPath.MakePlugVector3Path().Is2D())
                            .Ease(EaseType.Linear)
                            );
	}
	
	
	void Update () 
    {
	
	}
}
