using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AisleController : MonoBehaviour
{
    [SerializeField] private Product type;

    public Product Type => type;

    [SerializeField] private GameObject[] vegetablesPrefabs;
    [SerializeField] private GameObject[] toysPrefabs;
    [SerializeField] private GameObject[] bakeryPrefabs;
    [SerializeField] private Transform[] placements;
    [SerializeField] private float rotationOffset;
    [SerializeField] private int cartsPerAisle;
    private List<GameObject> _placedProducts;
    private int _numToTake;

    private void Awake()
    {
        _placedProducts = new List<GameObject>();
    }

    private void Start()
    {
        int typeNum;
        int unlockedSkins =
            PlayerPrefs.GetInt(PlayerPrefsStrings.SkinNumber.Name, PlayerPrefsStrings.SkinNumber.DefaultValue);
        int availableTypes = GetAvailableTypes(unlockedSkins);
        
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.SkinsUnlocked.Name,
                PlayerPrefsStrings.SkinsUnlocked.DefaultValue) == 0)
        {
            typeNum = Random.Range(0, availableTypes + 1);
        }
        else
        {
            typeNum = Random.Range(0, 3);
            unlockedSkins = vegetablesPrefabs.Length + toysPrefabs.Length + bakeryPrefabs.Length;
        }

        int availableItems = 0;
        GameObject[] chosenProducts;
        switch (typeNum)
        {
            case 0:
            {
                type = Product.Vegetable;
                availableItems = Mathf.Clamp(unlockedSkins, 0, vegetablesPrefabs.Length);
                chosenProducts = vegetablesPrefabs;
            }
                break;
            case 1:
            {
                type = Product.Toy;
                availableItems = Mathf.Clamp(unlockedSkins - vegetablesPrefabs.Length, 0, toysPrefabs.Length);
                chosenProducts = toysPrefabs;
            }
                break;
            case 2:
            {
                type = Product.Bakery;
                availableItems = Mathf.Clamp(unlockedSkins - vegetablesPrefabs.Length - toysPrefabs.Length
                    , 0, bakeryPrefabs.Length);
                
                chosenProducts = bakeryPrefabs;

            }
                break;
            default:
            {
                throw new Exception("Random broke somehow ¯\\_(ツ)_/¯");
            }
        }
        GameObject go;
        foreach (var placement in placements)
        {
            go = Instantiate(chosenProducts[Random.Range(0, availableItems)], placement);
            go.transform.localRotation = Quaternion.Euler(go.transform.localRotation.x,
                Random.Range(-rotationOffset, rotationOffset), go.transform.localRotation.z);
            _placedProducts.Add(go);
        }

        _numToTake = Mathf.FloorToInt((float) _placedProducts.Count / cartsPerAisle);
    }

    private int GetAvailableTypes(int unlocked)
    {
        int availableTypes = 0;
        if (unlocked > vegetablesPrefabs.Length)
        {
            availableTypes++;
        }

        if (unlocked > vegetablesPrefabs.Length + toysPrefabs.Length)
        {
            availableTypes++;
        }

        return availableTypes;
    }
    public List<GameObject> TakeProduct()
    {
        if (_placedProducts.Count == 0) return null;
        int num;
        if (_placedProducts.Count < 2 * _numToTake)
        {
            num = _placedProducts.Count;
        }
        else
        {
            num = Mathf.Clamp(_numToTake, 0, _placedProducts.Count);
        }

        int index;
        List<GameObject> taken = new List<GameObject>();
        for (int i = 0; i < num; i++)
        {
            index = Random.Range(0, _placedProducts.Count);
            taken.Add(_placedProducts[index]);
            //_placedProducts[index].SetActive(false);
            _placedProducts.RemoveAt(index);
        }

        return taken;
    }
}