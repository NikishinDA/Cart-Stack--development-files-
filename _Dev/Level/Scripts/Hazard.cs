using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.CartTailCutEvent;
        evt.Cart = other.GetComponent<CartController>();
        evt.Cart.transform.SetParent(null);
        evt.Cart.transform.position = transform.position;
        EventManager.Broadcast(evt);
    }
}
