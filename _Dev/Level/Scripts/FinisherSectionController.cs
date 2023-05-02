using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherSectionController : MonoBehaviour
{
    [SerializeField] private Transform cartHolder;
    [SerializeField] private Transform productHolder;
    private CartController _cart;
    private CartContentManager _cartContentManager;
    [SerializeField] private Transform fillingHolder;
    private Animator _animator;

    [SerializeField] private Transform deliveryTransform;
    [SerializeField] private Animator deliveryAnimator;
    [SerializeField] private ParticleSystem moneyEffect;
    [SerializeField] private Transform bagCapTransform;
    private bool _isEmpty;
    [SerializeField] private FinishSectorDetection sectorDetection;
    [SerializeField] private float flyHeight = 2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        sectorDetection.CartGet += SectorDetectionOnCartGet;
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent obj)
    {
        StartCoroutine(DelayAnim(0.1f));
    }

    private IEnumerator DelayAnim(float time)
    {
        if (_cart) yield break;

        yield return new WaitForSeconds(time);
        if (!_cart)
            deliveryAnimator.SetTrigger("Disapp");
    }

    private void SectorDetectionOnCartGet(CartController obj)
    {
        SetCart(obj, 1f);
    }

    public void SetCart(CartController cart, float time)
    {
        _cart = cart;
        _cartContentManager = _cart.GetComponent<CartContentManager>();
        cart.transform.SetParent(cartHolder);
        if (_cartContentManager.GetProductType().Type == ProductType.None)
        {
            _isEmpty = true;
        }
        else
        {
            _isEmpty = false;
            //VarSaver.MoneyCollected += _cartContentManager.GetProductType().Cost;
        }

        StartCoroutine(CorCartSetInPosition(time, 1f, 0.5f, 1f));
    }

    private IEnumerator CorCartSetInPosition(float cartTime, float walkTime, float turnTime, float flyObjectTime)
    {
        Vector3 oldPos = _cart.transform.localPosition;
        Vector3 newPos = Vector3.zero;
        for (float t = 0; t < cartTime; t += Time.deltaTime)
        {
            _cart.transform.localPosition = Vector3.Lerp(oldPos, newPos, t / cartTime);
            yield return null;
        }

        _cart.transform.localPosition = newPos;
        _cart.transform.localRotation = Quaternion.identity;
        /*deliveryAnimator.SetTrigger("Run");
        oldPos = deliveryTransform.localPosition;
        newPos = oldPos;
        newPos.x = 1.34f;
        for (float t = 0; t < walkTime; t += Time.deltaTime)
        {
           deliveryTransform.localPosition = Vector3.Lerp(oldPos, newPos, t/walkTime);
           yield return null;
        }

        deliveryAnimator.SetTrigger("Idle");*/
        if (_isEmpty)
        {
            deliveryAnimator.SetTrigger("Disapp");
        }
        else
        {
            Quaternion oldRot = deliveryTransform.rotation;
            Quaternion newRot = Quaternion.LookRotation(Vector3.right);

            for (float t = 0; t < turnTime; t += Time.deltaTime)
            {
                deliveryTransform.rotation = Quaternion.Lerp(oldRot, newRot, t / turnTime);
                yield return null;
            }

            foreach (var o in _cartContentManager.Filling)
            {
                o.transform.SetParent(fillingHolder);
            }

            oldPos = fillingHolder.position;
            newPos = productHolder.position;
            Vector3 newPosVector3;
            for (float t = 0; t < flyObjectTime; t += Time.deltaTime)
            {
                newPosVector3 = Vector3.Lerp(oldPos, newPos, t / flyObjectTime);
                if (t < flyObjectTime / 2)
                    newPosVector3.y = Mathf.Lerp(oldPos.y, flyHeight, t / flyObjectTime);
                else
                {
                    newPosVector3.y = Mathf.Lerp(flyHeight, newPos.y, t / flyObjectTime);
                    fillingHolder.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, 2 * (t / flyObjectTime - 0.5f));
                }

                fillingHolder.position = newPosVector3;
                yield return null;
            }

            //yield return new WaitForSeconds(1.0f);

            _cartContentManager.EmptyCart();
            moneyEffect.Play();
            oldRot = bagCapTransform.localRotation;
            newRot = Quaternion.Euler(0, 0, 0);
            for (float t = 0; t < 0.5f; t += Time.deltaTime)
            {
                bagCapTransform.localRotation = Quaternion.Lerp(oldRot, newRot, t / 0.5f);
                yield return null;
            }

            deliveryAnimator.SetTrigger("Dance");
        }
    }
}