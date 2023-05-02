using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesHazardDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.CartDestroyEvent;
        evt.Cart = other.GetComponent<CartController>();
        EventManager.Broadcast(evt);
        Taptic.Heavy();
    }
}
