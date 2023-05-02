using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [Header("Money Counter")]
    [SerializeField] private Text moneyText;
    [SerializeField] private Text totalMoneyText;
    [SerializeField] private Text multiplierText;

    [Header("Skin Progress")] 
    [SerializeField] private float progressPerLevel = 0.4f;
    private int _skinNumber;
    
    [SerializeField] private Image pbBackground;
    [SerializeField] private Image pbImage;
    [SerializeField] private Sprite[] skinsBackgrounds;
    [SerializeField] private Sprite[] skinsSprites;
    private void Awake()
    {
        nextButton.onClick.AddListener(OnNextButtonClick);
        moneyText.text = "$" + VarSaver.MoneyCollected;
        multiplierText.text = "X" + VarSaver.Multiplier.ToString("F1");
        totalMoneyText.text = VarSaver.MoneyCollected.ToString();
        _skinNumber = PlayerPrefs.GetInt(PlayerPrefsStrings.SkinNumber.Name, PlayerPrefsStrings.SkinNumber.DefaultValue);
        progressPerLevel = 1f / (_skinNumber / 10 + 1);
        pbBackground.sprite = skinsBackgrounds[_skinNumber];
        pbBackground.SetNativeSize();
        pbImage.sprite = skinsSprites[_skinNumber];
        pbImage.SetNativeSize();
    }

    private void Start()
    {
        StartCoroutine(MoneyCounter(2f));
        StartCoroutine(SkinProgress(2f));
    }

    private IEnumerator MoneyCounter(float time)
    {
        yield return new WaitForSeconds(1f);
        int money;
        int startMoney = VarSaver.MoneyCollected;
        int endMoney = (int) (startMoney * VarSaver.Multiplier);
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            money = (int) Mathf.Lerp(startMoney, endMoney, t / time);
            
            totalMoneyText.text = money.ToString();
            yield return null;
        }
        money = endMoney;
        totalMoneyText.text = money.ToString();
    }
    private  IEnumerator SkinProgress(float time)
        {
            float weaponProgress = PlayerPrefs.GetFloat(PlayerPrefsStrings.SkinProgress.Name, PlayerPrefsStrings.SkinProgress.DefaultValue);
            float endProgress = weaponProgress + progressPerLevel;
            if (endProgress >= 1)
            {
                endProgress = 1;
                PlayerPrefs.SetFloat(PlayerPrefsStrings.SkinProgress.Name, 0);
                _skinNumber = (_skinNumber + 1) % skinsSprites.Length;
                PlayerPrefs.SetInt(PlayerPrefsStrings.SkinNumber.Name, _skinNumber);
                if (_skinNumber == skinsSprites.Length-1 &&
                    PlayerPrefs.GetInt(PlayerPrefsStrings.SkinsUnlocked.Name,
                        PlayerPrefsStrings.SkinsUnlocked.DefaultValue) == 0)
                {
                    PlayerPrefs.SetInt(PlayerPrefsStrings.SkinsUnlocked.Name, 1);
                }
            }
            else
            {
                PlayerPrefs.SetFloat(PlayerPrefsStrings.SkinProgress.Name, endProgress);
            }
            for (float t = 0; t < time; t += Time.deltaTime)
            {
                pbImage.fillAmount = Mathf.Lerp(weaponProgress, endProgress, t / time);
                yield return null;
            }
            
        }
    private void OnNextButtonClick()
    {
        PlayerPrefs.Save();
        SceneLoader.LoadNextLevel();
    }
}
