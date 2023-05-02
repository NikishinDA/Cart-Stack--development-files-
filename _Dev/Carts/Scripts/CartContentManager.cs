using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CartContentManager : MonoBehaviour
{
    [SerializeField] private Transform[] placements;
    [SerializeField] private GameObject[] vegetablesPrefabs;
    [SerializeField] private GameObject[] toysPrefabs;
    [SerializeField] private GameObject[] bakeryPrefabs;
    private List<GameObject> _filling;
    private bool _isFull;
    [SerializeField] private float flyTime;

    public bool IsFull => _isFull;

    public List<GameObject> Filling => _filling;

    private Product _type = Product.Empty;

    private void Awake()
    {
        _filling = new List<GameObject>();
    }

    public void CartAddedToChain()
    {
        if (_isFull)
            BroadcastCostChangeEvent(false);
    }

    public void CartLost()
    {
        if (_isFull)
            BroadcastCostChangeEvent(true);
    }
    public void FillCart(Product type, List<GameObject> products)
    {
        if (_isFull) return;
        foreach (var product in products)
        {
            StartCoroutine(TakeProduct(product.transform, flyTime));
        }

        _type = type;
        StartCoroutine(DelaySpawn(products, flyTime));
    }

    private void SpawnContents(Product type)
    {
        GameObject[] productsPrefabs;
        switch (type.Type)
        {
            case ProductType.Vegetables:
                productsPrefabs = vegetablesPrefabs;
                break;
            case ProductType.Toys:
                productsPrefabs = toysPrefabs;
                break;
            case ProductType.Bakery:
                productsPrefabs = bakeryPrefabs;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        Transform goTransform;
        _type = type;
        for (int i = 0; i < placements.Length; i++)
        {
            goTransform = Instantiate(productsPrefabs[Random.Range(0, productsPrefabs.Length)], placements[i]).transform;
            goTransform.localPosition = Vector3.zero;
            goTransform.localRotation = Quaternion.identity;
            _filling.Add(goTransform.gameObject);
        }

        _isFull = true;

        BroadcastCostChangeEvent(false);
    }

    private void SpawnContents(List<GameObject> products)
    {
        int i;
        for (i = 0; i < products.Count; i++)
        {
            products[i].transform.SetParent(placements[i]);
            products[i].transform.localPosition = Vector3.zero;
            products[i].transform.localRotation = Quaternion.identity;
            _filling.Add(products[i]);
        }

        GameObject go;
        for (int j = i; j < placements.Length; j++)
        {
            go = Instantiate(products[Random.Range(0, products.Count)], placements[j]);
            _filling.Add(go);
        }
        
        _isFull = true;
        BroadcastCostChangeEvent(false);
    }
    public bool EmptyCart(float penalty = 1f)
    {
        if (!_isFull) return false;
        foreach (var product in _filling)
        {
            Destroy(product);
        }
        _filling.Clear();
        BroadcastSellEvent(penalty);
        BroadcastCostChangeEvent(true);
        _isFull = false;
        _type = Product.Empty;
        return true;
    }

    public Product GetProductType()
    {
        return _type;
    }

    private void BroadcastCostChangeEvent(bool isLoss)
    {
        var evt = GameEventsHandler.ChainTotalCostChangeEvent;
        evt.IsLoss = isLoss;
        evt.Type = _type;
        EventManager.Broadcast(evt);
    }

    private void BroadcastSellEvent(float penalty = 1f)
    {
        var evt = GameEventsHandler.CartSellContentsEvent;
        if (penalty > 1f)
            evt.Cost = (int) (_type.Cost / penalty);
        else
        {
            evt.Cost = _type.Cost;
        }
        EventManager.Broadcast(evt);
    }

    private IEnumerator TakeProduct(Transform productTransform, float time)
    {
        productTransform.SetParent(transform);
        Vector3 oldPos = productTransform.localPosition;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            productTransform.localPosition = Vector3.Lerp(oldPos, placements[1].localPosition, t/time);
            yield return null;
        }
        //productTransform.gameObject.SetActive(false);
    }

    private IEnumerator DelaySpawn(List<GameObject> products,float time)
    {
        yield return new WaitForSeconds(time);
        
        SpawnContents(products);
    }
}
