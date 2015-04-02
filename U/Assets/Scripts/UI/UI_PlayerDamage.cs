using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_PlayerDamage : MonoBehaviour {

    private static UI_PlayerDamage _instance;
    public static UI_PlayerDamage instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UI_PlayerDamage>();
            }
            return _instance;
        }
    }
    
    
    
    private Text _text;
    public float fadeTimer;
    private Color _color;

    void Awake()
    {
        _text = GetComponent<Text>();
        _color = Color.white;
        _color.a = 255f;
    }

    void Update ()
    {

        //Fade text
        _text.CrossFadeAlpha(0.0f, fadeTimer, false);


    }


    public void UpdateText(float damageTaken)
    {
        _text.text = "Lost " + damageTaken + " hp";
        _text.CrossFadeAlpha(1f, 0, false);
    }

}
