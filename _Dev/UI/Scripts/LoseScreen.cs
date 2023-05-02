using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private Button retryButton;

    private void Awake()
    {
        retryButton.onClick.AddListener(OnRetryButtonClick);
    }

    private void OnRetryButtonClick()
    {
        SceneLoader.ReloadLevel();
    }
}
