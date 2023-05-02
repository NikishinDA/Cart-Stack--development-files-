using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropelForwardHazard : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.CartPropelEvent;
        evt.Cart = other.GetComponent<CartController>();
        evt.Destroying = false;
        EventManager.Broadcast(evt);
        effect.Play();
        Taptic.Heavy();
    }
}
