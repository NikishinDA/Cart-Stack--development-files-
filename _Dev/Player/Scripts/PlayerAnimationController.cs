using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody[] ragdoll;
    [SerializeField] private PhysicalBodySafeDestroy safeDestroy;
    [SerializeField] private float deathPropelForce = 10f;
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<FinisherStartEvent>(OnFinisherStart);
    }

    private void OnDestroy()
    {        
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);

        EventManager.RemoveListener<FinisherStartEvent>(OnFinisherStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void OnFinisherStart(FinisherStartEvent obj)
    {
       // animator.SetTrigger("Idle");
    }

    private void OnGameStart(GameStartEvent obj)
    {
        animator.SetTrigger("Move");
    }

    private void OnGameOver(GameOverEvent obj)
    {
        if (obj.IsWin)
        {
            animator.SetTrigger("Dance");
            return;
        }
        animator.enabled = false;
        safeDestroy.enabled = true;
        foreach (var rb in ragdoll)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
        ragdoll[0].AddForce(deathPropelForce * Vector3.forward,ForceMode.Impulse);
    }
}
