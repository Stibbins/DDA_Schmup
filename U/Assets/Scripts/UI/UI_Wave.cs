using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Wave : MonoBehaviour {

    private Text _textComponent;
    private WaveController _waveController;
    void Awake ()
    {
        _textComponent = GetComponent<Text>();
        _waveController = WaveController.instance;
    }


	void Update () 
    {
        _textComponent.text = "Current wave: " + _waveController._currentWave + " of " + _waveController.numberOfWaves;
	}
}
