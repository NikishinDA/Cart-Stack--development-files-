using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFallDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.CartFallEvent;
        evt.Cart = other.GetComponent<CartController>();
        EventManager.Broadcast(evt);
    }
}
