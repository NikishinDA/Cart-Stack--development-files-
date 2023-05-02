using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSectorDetection : MonoBehaviour
{
    public event Action<CartController> CartGet;
    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _trigger.enabled = false;
        CartController cartController = other.GetComponent<CartController>();
        var evt = GameEventsHandler.FinisherTakeAwayCartEvent;
        evt.Cart = cartController;
        EventManager.Broadcast(evt);
        CartGet?.Invoke(cartController);
    }
}
