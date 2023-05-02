using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartFinishLane : MonoBehaviour
{
    private Collider _trigger;
    [SerializeField] private ParticleSystem[] effects;
    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _trigger.enabled = false;
        foreach (var effect in effects)
        {
            effect.Play();
        }
    }
}
