using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CartLevelObjectDetection : MonoBehaviour
{
    [SerializeField] private CartController cart;
    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.CartCollectEvent;
        evt.Cart = cart;
        EventManager.Broadcast(evt);
    }
}
