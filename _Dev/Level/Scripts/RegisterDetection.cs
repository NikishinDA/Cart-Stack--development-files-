using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterDetection : MonoBehaviour
{
    [SerializeField] private ParticleSystem sellEffect;
    [SerializeField] private float costReduction;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CartContentManager>().EmptyCart(costReduction))
            sellEffect.Play();
        var evt = GameEventsHandler.CartStallSellEvent;
        evt.Cart = other.GetComponent<CartController>();
        //evt.Destroying = true;
        EventManager.Broadcast(evt);
        Taptic.Heavy();
    }
}
