using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    [SerializeField] private Image levelProgressBar;
    [SerializeField] private Text levelText;
    [SerializeField] private Text moneyText;
    private float _levelProgress;
    private float _progressPerLevel;
    private int _earnedMoney;
    [SerializeField] private TMP_Text addMoneyText;
    [SerializeField] private Transform moneyCanvasTransform;
    [Header("Debug")] [SerializeField] private Button restartButton;
    private void Awake()
    {
        EventManager.AddListener<PlayerProgressEvent>(OnPlayerProgress);
        EventManager.AddListener<CartSellContentsEvent>(OnCartSell);
        levelText.text = PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue).ToString();
        
        moneyText.text = "$" + _earnedMoney.ToString();
        restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        SceneLoader.ReloadLevel();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerProgressEvent>(OnPlayerProgress);
        EventManager.RemoveListener<CartSellContentsEvent>(OnCartSell);

    }

    private void OnCartSell(CartSellContentsEvent obj)
    {
        _earnedMoney += obj.Cost;
        VarSaver.MoneyCollected = _earnedMoney;
        moneyText.text = "$" + _earnedMoney.ToString();
        
        Instantiate(addMoneyText, moneyCanvasTransform).text = "+$" + obj.Cost;
    }

    private void Start()
    {
        _progressPerLevel = 1f / VarSaver.LevelLength;
    }
    private void OnPlayerProgress(PlayerProgressEvent obj)
    {
        _levelProgress += _progressPerLevel;
    }
    private void Update()
    {
        levelProgressBar.fillAmount = Mathf.Lerp(levelProgressBar.fillAmount, _levelProgress, Time.deltaTime);
    }
}
