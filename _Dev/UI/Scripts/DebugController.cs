using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    [SerializeField] private InputField speedInput;
    
    [SerializeField] private InputField strafeInput;
    [SerializeField] private Button startButton;
    private float _speed;
    private float _strafe;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        _speed = PlayerPrefs.GetFloat("DebugSpeed", 10);
        _strafe = PlayerPrefs.GetFloat("DebugStrafe", 15);
        speedInput.text = _speed.ToString();
        strafeInput.text = _strafe.ToString();
    }

    private void OnStartButtonClick()
    {
        var evt = GameEventsHandler.DebugCallEvent;
        Single.TryParse(speedInput.text, out _speed);
        Single.TryParse(strafeInput.text, out _strafe);
        evt.Speed = _speed;
        evt.Strafe = _strafe;
        PlayerPrefs.SetFloat("DebugSpeed", _speed);
        PlayerPrefs.SetFloat("DebugStrafe", _strafe);
        PlayerPrefs.Save();
        EventManager.Broadcast(evt);
    }
}
