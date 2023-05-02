using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLane : MonoBehaviour
{
    [SerializeField] private FinisherController finisherController;

    private void OnTriggerEnter(Collider other)
    {
        /*var evt = GameEventsHandler.GameOverEvent;
        evt.IsWin = true;
        EventManager.Broadcast(evt);*/
        EventManager.Broadcast(GameEventsHandler.FinisherStartEvent);
        finisherController.ActivateFinisher(other.transform);
    }
}
