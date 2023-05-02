using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    [SerializeField] private Transform linkJoint;
    [SerializeField] private float rotationModifier;

    [SerializeField]
    private GameObject playerDetection;

    [Header("Effects")] [SerializeField] private ParticleSystem destroyEffect;
    private CartLauncher _cartLauncher;

    private int _gameObjectLayer;
    private int _defaultLayer;
    private readonly WaitForSeconds _safeDetectionWait = new WaitForSeconds(0.5f);
    private CartContentManager _contentManager;

    private bool _isFollowing;
    private Transform _leadingJoint;
    private float _whiplashModifier;
    public Transform LinkJoint => linkJoint;

    private void Awake()
    {
        _cartLauncher = GetComponent<CartLauncher>();
        _gameObjectLayer = LayerMask.NameToLayer("Cart");
        _defaultLayer = LayerMask.NameToLayer("Default");
        _contentManager = GetComponent<CartContentManager>();
    }

    public void Initialize(Transform joint, float whiplashSpeed)
    {
        playerDetection.SetActive(false);
        _cartLauncher.DisablePhysics();
        _leadingJoint = joint;
        _whiplashModifier = whiplashSpeed;
        _isFollowing = true;
        gameObject.layer = _gameObjectLayer;
        _contentManager.CartAddedToChain();
        
    }

    public void StopFollowing(bool balanceChange = true)
    {
        _isFollowing = false;
        gameObject.layer = _defaultLayer;
        if (balanceChange)
            _contentManager.CartLost();
    }
    public void LoseCart()
    {
        _isFollowing = false;
        _cartLauncher.LunchUp();
        _contentManager.CartLost();
    }

    public void DestroyCart()
    {
        _isFollowing = false;
        _contentManager.CartLost();
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void PropelCartForward()
    {
        _isFollowing = false;
        gameObject.layer = _defaultLayer;
        _cartLauncher.LunchForward();
        _contentManager.CartLost();
        StartCoroutine(SafeDetectionEnable());
    }

    public void DropCart()
    {
        _isFollowing = false;
        gameObject.layer = _defaultLayer;
        _cartLauncher.DropDown();
        _contentManager.CartLost();
    }

   /* public void SetCartInPosition(float time)
    {
        StartCoroutine(CorCartSetInPosition(time));
    }*/
    private void FixedUpdate()
    {
        if (_isFollowing)
        {

            transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, _leadingJoint.position.x, _whiplashModifier),
                _leadingJoint.position.y,
                _leadingJoint.position.z);
            transform.localRotation = Quaternion.Euler(0,rotationModifier *(transform.position.x - _leadingJoint.position.x), 0);

        }
    }

    private IEnumerator SafeDetectionEnable()
    {
        yield return _safeDetectionWait;
        playerDetection.SetActive(true);
    }

}
