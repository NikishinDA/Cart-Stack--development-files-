using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceManager : MonoBehaviour
{
    private int _ambianceNum;
    //[SerializeField] private Color[] fogColor;
    [SerializeField] private int levelsPerChange;
    [SerializeField] private Material[] skyboxMaterials;
    private void Awake()
    {
        _ambianceNum =
            PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue) 
            / levelsPerChange % skyboxMaterials.Length;
        RenderSettings.skybox = skyboxMaterials[_ambianceNum];
        //RenderSettings.fogColor = fogColor[_ambianceNum];
    }
}
