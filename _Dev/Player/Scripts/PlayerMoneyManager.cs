using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    private int _chainCost;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private GameObject moneyCanvas;
    [SerializeField] private TMP_Text addMoneyText;
    [SerializeField] private TMP_Text subMoneyText;
    private void Awake()
    {
        EventManager.AddListener<ChainTotalCostChangeEvent>(OnChainCostChange);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<FinisherStartEvent>(OnFinisherStart);
        VarSaver.MoneyCollected = 0;
        moneyText.text = "$0";
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<ChainTotalCostChangeEvent>(OnChainCostChange);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<FinisherStartEvent>(OnFinisherStart);

    }

    private void OnFinisherStart(FinisherStartEvent obj)
    {
        //BroadcastSellEvent();
    }

    private void OnGameOver(GameOverEvent obj)
    {
        if(obj.IsWin) return;
        moneyCanvas.SetActive(false);
    }

    private void OnChainCostChange(ChainTotalCostChangeEvent obj)
    {
        int change = obj.IsLoss ? -1 : 1;
        ChangeChainCost(change * obj.Type.Cost);
        if (obj.IsLoss)
        {
            Instantiate(subMoneyText, moneyCanvas.transform).text = "-$" + obj.Type.Cost;
        }
        else
        {
            Instantiate(addMoneyText, moneyCanvas.transform).text = "+$" + obj.Type.Cost;
        }
    }

    private void ChangeChainCost(int diff)
    {
        _chainCost += diff;
        if (_chainCost < 0) _chainCost = 0;
        moneyText.text = "$" + _chainCost;
    }
    private void BroadcastSellEvent()
    {
        var evt = GameEventsHandler.CartSellContentsEvent;
        evt.Cost = _chainCost;
        EventManager.Broadcast(evt);
    }
}
