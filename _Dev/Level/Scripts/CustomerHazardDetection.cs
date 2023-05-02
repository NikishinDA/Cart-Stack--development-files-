using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHazardDetection : MonoBehaviour
{
    private Collider _trigger;
    //[SerializeField] private CustomerController customerController;
    public event Action<CartController> CartDetectedEvent; 

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _trigger.enabled = false;
        var evt = GameEventsHandler.CartTailCutEvent;
        evt.Cart = other.GetComponent<CartController>();
        CartDetectedEvent?.Invoke(evt.Cart);
        //customerController.TakeAwayCart(evt.Cart);
        EventManager.Broadcast(evt);
        Taptic.Failure();
    }
}
