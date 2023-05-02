using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChainManager : MonoBehaviour
{
    private List<CartController> _cartChain;
    [SerializeField] private CartController cartPrefab;
    [SerializeField] private Transform firstJoint;
    [SerializeField] private float chainLinkLength;
    [SerializeField] private AnimationCurve followCoefCurve;
    [SerializeField] private int maxEffectLength;
    [SerializeField] private float scalingMultiplier;
    [SerializeField]
    private Transform chainParent;

    public List<CartController> CartChain => _cartChain;


    private void Awake()
    {
        EventManager.AddListener<CartCollectEvent>(OnCartCollect);
        EventManager.AddListener<CartTailCutEvent>(OnTailCut);
        EventManager.AddListener<CartPropelEvent>(OnCartPropel);
        EventManager.AddListener<CartDestroyEvent>(OnCartDestroy);
        EventManager.AddListener<CartFallEvent>(OnCartFall);
        EventManager.AddListener<CartStallSellEvent>(OnCartSell);
        EventManager.AddListener<FinisherTakeAwayCartEvent>(OnFinisherTakeAwayCart);
        _cartChain = new List<CartController>();

    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<CartCollectEvent>(OnCartCollect);
        EventManager.RemoveListener<CartTailCutEvent>(OnTailCut);
        EventManager.RemoveListener<CartPropelEvent>(OnCartPropel);
        EventManager.RemoveListener<CartDestroyEvent>(OnCartDestroy);
        EventManager.RemoveListener<CartFallEvent>(OnCartFall);
        EventManager.RemoveListener<CartStallSellEvent>(OnCartSell);

        EventManager.RemoveListener<FinisherTakeAwayCartEvent>(OnFinisherTakeAwayCart);
    }

    private void OnCartSell(CartStallSellEvent obj)
    {
        int cutIndex = _cartChain.LastIndexOf(obj.Cart);
        if (cutIndex == -1)
            return;//throw new Exception("Something went wrong with propelling");
        if (_cartChain.Count > 1)
        {
            _cartChain.RemoveAt(cutIndex);
            obj.Cart.DestroyCart();
            PropelCartsForward(cutIndex);
        }
    }

    private void OnFinisherTakeAwayCart(FinisherTakeAwayCartEvent obj)
    {
        obj.Cart.StopFollowing(false);
        _cartChain.Remove(obj.Cart);
        if (_cartChain.Count == 0)
        {
            var evt = GameEventsHandler.GameOverEvent;
            evt.IsWin = true;
            EventManager.Broadcast(evt);
        }
    }

    private void OnCartFall(CartFallEvent obj)
    {
        int cutIndex = _cartChain.LastIndexOf(obj.Cart);
        if (cutIndex == -1)
                return; //throw new Exception("Something went wrong with destroying");
        _cartChain.RemoveAt(cutIndex);
        obj.Cart.DropCart();
        PropelCartsForward(cutIndex);//TailCut(cutIndex);
    }

    private void Start()
    {
        CartController cartController = Instantiate(cartPrefab);
        _cartChain.Add(cartController);
        cartController.Initialize(firstJoint, CalculateWhiplashSpeed(0));
    }

    private void OnCartDestroy(CartDestroyEvent obj)
    {
        int cutIndex = _cartChain.LastIndexOf(obj.Cart);
        if (cutIndex == -1)
             return;//throw new Exception("Something went wrong with destroying");
        _cartChain.RemoveAt(cutIndex);
        obj.Cart.DestroyCart();
        PropelCartsForward(cutIndex);//TailCut(cutIndex);
    }

    private void OnCartPropel(CartPropelEvent obj)
    {
        int cutIndex = _cartChain.LastIndexOf(obj.Cart);
        if (cutIndex == -1)
            return;//throw new Exception("Something went wrong with propelling");
        if (obj.Destroying)
        {
            _cartChain.RemoveAt(cutIndex);
            obj.Cart.DestroyCart();
        }
        PropelCartsForward(cutIndex);
    }

    private void OnTailCut(CartTailCutEvent obj)
    {
        int cutIndex = _cartChain.LastIndexOf(obj.Cart);
        if (cutIndex == -1)
            return;//throw new Exception("Something went wrong with cutting");
        _cartChain.RemoveAt(cutIndex);
        obj.Cart.StopFollowing();
        PropelCartsForward(cutIndex);
        //TailCut(cutIndex);
    }

    private void PropelCartsForward(int cutIndex)
    {
        int count = _cartChain.Count;
        for (int i = cutIndex; i < count; i++)
        {
            _cartChain[_cartChain.Count - 1].PropelCartForward();
            _cartChain.RemoveAt(_cartChain.Count - 1);
        }
        CheckLoseCondition();

    }

    private void TailCut(int cutIndex)
    {
        int count = _cartChain.Count;
        for (int i = cutIndex; i < count; i++)
        {
            _cartChain[_cartChain.Count - 1].LoseCart();
            _cartChain.RemoveAt(_cartChain.Count - 1);
        }

        CheckLoseCondition();
    }

    private void OnCartCollect(CartCollectEvent obj)
    {
        var linkingJoint = _cartChain.Count > 0 ? _cartChain[_cartChain.Count - 1].LinkJoint : firstJoint;
        obj.Cart.transform.SetParent(chainParent);
        _cartChain.Add(obj.Cart);
        obj.Cart.Initialize(linkingJoint, CalculateWhiplashSpeed(_cartChain.Count));
    }

    private float CalculateWhiplashSpeed(int number)
    {
        float t = 1 - Mathf.Clamp((float) number, 0, maxEffectLength) / (maxEffectLength * scalingMultiplier);
        //Debug.Log(followCoefCurve.Evaluate(t));
        return  followCoefCurve.Evaluate(t);
    }

    private void CheckLoseCondition()
    {
        if (_cartChain.Count == 0)
        {
            var evt = GameEventsHandler.GameOverEvent;
            evt.IsWin = false;
            EventManager.Broadcast(evt);
        }
    }
}
