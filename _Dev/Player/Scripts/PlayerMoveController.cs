using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMoveController : MonoBehaviour
{
    [Header("X Movement")] [SerializeField]
    private float maxSpeedX;

    [SerializeField] private float movementBoundary;

    [Header("Z Movement")] [SerializeField]
    private float startSpeedZ = 10;

    [SerializeField] private float slowdownSpeed;
    [SerializeField] private float acceleration;
    private float _speedZ;

    private float _newX;
    private float _oldX;
    private Vector3 _newPosition;
    private PlayerInputManager _inputManager;
    private CharacterController _cc;
    private bool _move = false;
    private bool _finisherLock = false;
    //[SerializeField] private float gravityForce;
    //[SerializeField] private CinemachineVirtualCamera vCamera;
    private void Awake()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        _cc = GetComponent<CharacterController>();
        _newPosition = new Vector3();
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<DebugCallEvent>(OnDebugCall);
        EventManager.AddListener<FinisherStartEvent>(OnFinisherStart);
        EventManager.AddListener<TutorialToggleEvent>(OnTutorialToggle);
        EventManager.AddListener<CartTailCutEvent>(OnTailCut);
        EventManager.AddListener<CartPropelEvent>(OnCartPropel);
        EventManager.AddListener<CartDestroyEvent>(OnCartDestroy);
        _speedZ = startSpeedZ;
    }

    private void OnDebugCall(DebugCallEvent obj)
    {
        _speedZ = obj.Speed;
        maxSpeedX = obj.Strafe;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<DebugCallEvent>(OnDebugCall);
        EventManager.RemoveListener<FinisherStartEvent>(OnFinisherStart);
        EventManager.RemoveListener<TutorialToggleEvent>(OnTutorialToggle);
        EventManager.RemoveListener<CartTailCutEvent>(OnTailCut);
        EventManager.RemoveListener<CartPropelEvent>(OnCartPropel);
        EventManager.RemoveListener<CartDestroyEvent>(OnCartDestroy);

    }

    private void OnTailCut(CartTailCutEvent obj)
    {
        SlowDownEffect();
    }

    private void OnCartPropel(CartPropelEvent obj)
    {
        SlowDownEffect();
    }

    private void OnCartDestroy(CartDestroyEvent obj)
    {
        SlowDownEffect();
    }

    private void OnFinisherStart(FinisherStartEvent obj)
    {
        _finisherLock = true;
        _newPosition.x = 0;
    }

    private void OnTutorialToggle(TutorialToggleEvent obj)
    {
        _move = !obj.Toggle;
    }


    private void OnGameOver(GameOverEvent obj)
    {
        _move = false;
    }

    private void OnGameStart(GameStartEvent obj)
    {
        _move = true;
    }

    private void FixedUpdate()
    {
        if (!_move) return;
        if (!_finisherLock)
        {
            float touchDelta = _inputManager.GetTouchDelta();
            _newX = maxSpeedX * touchDelta;
            if (Mathf.Abs(transform.position.x + _newX) >= movementBoundary)
            {
                _newX = 0;
            }

            _newPosition.x = _newX;
        }
        else
        {
            _newPosition.x = -transform.position.x * maxSpeedX * Time.fixedDeltaTime * 0.2f;
        }

        _newPosition.z = _speedZ * Time.deltaTime;
        _cc.Move(_newPosition);
        if (_speedZ < startSpeedZ)
        {
            _speedZ = Mathf.Clamp(_speedZ + acceleration * Time.fixedDeltaTime, 0, startSpeedZ);
        }
    }

    private void SlowDownEffect()
    {
        _speedZ = slowdownSpeed;
    }
}