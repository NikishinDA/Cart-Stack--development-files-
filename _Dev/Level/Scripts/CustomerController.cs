using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private Transform cartHolder;
    [SerializeField] private Transform customerTransform;
    [SerializeField] private Animator customerAnimator;
    [SerializeField] private float xDestination;
    [SerializeField] private Rigidbody[] customerRagdoll;
    private CartController _cart;
    [SerializeField] private CustomerHazardDetection detection;

    private void Awake()
    {
        detection.CartDetectedEvent += TakeAwayCart;
    }

    private void TakeAwayCart(CartController cart)
    {
        _cart = cart;
        Transform cartTransform = cart.transform;
        cartTransform.SetParent(cartHolder);
        cartTransform.localPosition = Vector3.zero;
        cartTransform.localRotation = Quaternion.identity;
        StartCoroutine(PullCartAway(1f));
    }

    private IEnumerator PullCartAway(float time)
    {
        customerAnimator.SetTrigger("Pull");
        var position = customerTransform.position;
        int dir = position.x < 0 ? -1 : 1;
        Quaternion rot = Quaternion.LookRotation(-Vector3.right * dir);
        customerTransform.rotation = rot;
        Vector3 oldPos = position;
        Vector3 newPos = position;
        newPos.x = dir * xDestination;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            customerTransform.position = Vector3.Lerp(oldPos,newPos, t/time);
            yield return null;
        }

        customerAnimator.enabled = false;
        foreach (var rb in customerRagdoll)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        customerRagdoll[0].AddForce(-transform.forward * 50f, ForceMode.Impulse);
        _cart.LoseCart();
    }
}
