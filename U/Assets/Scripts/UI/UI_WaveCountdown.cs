using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_WaveCountdown : MonoBehaviour {


    private WaveController _waveController;
    private PlayerController _playerController;
    private Text _text;

    void Awake ()
    {
        _waveController = WaveController.instance;
        _playerController = PlayerController.instance;
        _text = GetComponent<Text>();
    }

    void Update ()
    {

        if (!_waveController._waveActive)
        {
            string temp = (_waveController._spawnTimer - Time.time).ToString();
            _text.text = "Next wave in " + temp + "s";
        }

        else if(_playerController.currentHealth <= 0)
        {
            _text.text = "Game Over, you died. Press R to restart";

        }

        else if (_waveController._currentWave >= _waveController.numberOfWaves && _waveController._gameActive == false)
        {
            _text.text = "Congratulations you beat the game! Press R to play again.";
        }

        else
        {
            _text.text = "";
        }
    }


}
