using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Health : MonoBehaviour {

    private Text _textComponent;
    private PlayerController _playerController;
    void Awake()
    {
        _textComponent = GetComponent<Text>();
        _playerController = PlayerController.instance;
    }


    void Update()
    {
        _textComponent.text = "Current health: " + _playerController.currentHealth;
    }
}
