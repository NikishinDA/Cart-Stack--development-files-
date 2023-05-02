using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AisleDetection : MonoBehaviour
{
    [SerializeField] private AisleController aisleController;
    private void OnTriggerEnter(Collider other)
    {
        CartContentManager cartContentManager = other.GetComponent<CartContentManager>();
        if (cartContentManager.IsFull) return;
        List<GameObject> products = aisleController.TakeProduct();
        if (products != null)
            cartContentManager.FillCart(aisleController.Type, products);
        Taptic.Medium();
    }
}
