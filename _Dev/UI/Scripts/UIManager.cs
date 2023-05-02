using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject tutorial;

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        if (obj.IsWin)
        {
            StartCoroutine(DelayPopupShow(5f));
        }
        else
        {
            overlay.SetActive(false);
            loseScreen.SetActive(true);
        }
    }

    private void OnGameStart(GameStartEvent obj)
    {
        startScreen.SetActive(false);
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue) == 1)
            tutorial.SetActive(true);
        overlay.SetActive(true);
    }

    void Start()
    {
        startScreen.SetActive(true);
    }

    private IEnumerator DelayPopupShow(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        overlay.SetActive(false);
        winScreen.SetActive(true);
    }
    
}
