using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMover : MonoBehaviour
{
    [SerializeField] private Animator customerAnimator;
    private int _direction;
    [SerializeField] private float constraintX;
    [SerializeField] private float speed;
    private Vector3 _startPosition;
    [SerializeField] private CustomerHazardDetection detection;
    private static readonly int s_right = Animator.StringToHash("Right");

    void Start()
    {
        _direction = Random.value < 0.5f ? -1 : 1;
        customerAnimator.SetBool(s_right, _direction == 1);
        _startPosition = transform.position;
        detection.CartDetectedEvent += DetectionOnCartDetectedEvent;
    }

    private void DetectionOnCartDetectedEvent(CartController obj)
    {
        this.enabled = false;
    }

    void Update()
    {
        transform.Translate(_direction * speed * Time.deltaTime,0,0);
        if (transform.position.x < _startPosition.x - constraintX)
        {
            _direction = 1;
            customerAnimator.SetBool(s_right, _direction == 1);

        }

        if (transform.position.x > _startPosition.x + constraintX)
        {
            _direction = -1;
            customerAnimator.SetBool(s_right, _direction == 1);

        }
    }
}
