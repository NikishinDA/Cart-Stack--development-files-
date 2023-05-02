using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreference<T>
{
    public string Name;
    public T DefaultValue;
}
public class PlayerPrefsStrings
{
    //public static readonly string Level = "Level";
    public static readonly string PlayedLevels = "PlayedLevels";
    public static readonly string CurrentHealth = "CurrentHealth";
    public static readonly string MaxHealth = "MaxHealth";
    public static readonly string WeaponUpgradeLevel = "WeaponUpgradeLevel";
    public static readonly string MoneyTotal = "MoneyTotal";
    public static readonly string MoneyUpgradeLevel = "MoneyUpgradeLevel";
    public static readonly string SectionNumber = "SectionNumber";
    public static readonly string Ambience = "Ambience";
    public static readonly string FirstLaunch = "FirstLaunch";
    public static readonly string Gen = "Gen";
    
    public static readonly PlayerPreference<int> Level = new PlayerPreference<int> {Name = "Level", DefaultValue = 1};

    public static readonly PlayerPreference<float> SkinProgress = new PlayerPreference<float>
        {Name = "SkinProgress", DefaultValue = 0.01f};
    public static readonly PlayerPreference<int> SkinNumber = new PlayerPreference<int> {Name = "SkinNumber", DefaultValue = 2};
    public static readonly PlayerPreference<int> SkinsUnlocked = new PlayerPreference<int> {Name = "SkinsUnlocked", DefaultValue = 0};

}
