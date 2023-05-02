using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class GameManager : MonoBehaviour
{private float _playTimer;
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        GameAnalytics.Initialize();
        
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameStart(GameStartEvent obj)
    {
        int level = PlayerPrefs.GetInt("Level", 1);
        GameAnalytics.NewProgressionEvent (
            GAProgressionStatus.Start,
            "Level_" + level);
        StartCoroutine(Timer());
    }

    private void OnGameOver(GameOverEvent obj)
    {
        int level = PlayerPrefs.GetInt("Level", 1);
        var status = obj.IsWin? GAProgressionStatus.Complete : GAProgressionStatus.Fail;
        GameAnalytics.NewProgressionEvent(
            status,
            "Level_" + level,
            "PlayTime_" + Mathf.RoundToInt(_playTimer));
        
    }
    private IEnumerator Timer()
    {
        for (;;)
        {
            _playTimer += Time.deltaTime;
            yield return null;
        }
    }
    #if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            SceneLoader.ReloadLevel();
        }
    }
    #endif
}
