using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CartLauncher : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [Header("Forward Motion")]
    [SerializeField] private float forwardForce;
    [SerializeField] private float sideForce;
    [SerializeField] private float spinningTorque;
    [Header("Upward Motion")]
    [SerializeField] private float upwardForce;
    [SerializeField] private float sideUpForce;
    [SerializeField] private float spinUpForce;
    private PhysicalBodySafeDestroy _safeDestroy;

    private readonly float[] _side = {-1f, 1f};
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _safeDestroy = GetComponent<PhysicalBodySafeDestroy>();
    }

    private void Start()
    {
        TogglePhysics(false);
    }

    public void LunchForward()
    {
        TogglePhysics(true);
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rigidbody.AddForce(Vector3.forward * forwardForce +
                            Random.Range(-1f, 1f) * sideForce * Vector3.right, ForceMode.Impulse);
        _rigidbody.AddTorque(0,Random.Range(-1f, 1f) * spinningTorque,0, ForceMode.Impulse);
    }

    public void LunchUp()
    {
        TogglePhysics(true);
        _rigidbody.AddForce(Vector3.up * upwardForce + 
                            Vector3.right * (sideForce * _side[Random.Range(0, _side.Length)]), ForceMode.Impulse);
        _rigidbody.AddTorque(Random.insideUnitSphere * spinUpForce);
    }
    public void DisablePhysics()
    {
        TogglePhysics(false);
    }

    public void DropDown()
    {
        TogglePhysics(true);
        _rigidbody.AddForce(10f * Vector3.forward + Vector3.down, ForceMode.Impulse);
    }
    private void TogglePhysics(bool enable)
    {
        _rigidbody.useGravity = enable;
        _rigidbody.isKinematic = !enable;
        _safeDestroy.enabled = enable;
        _rigidbody.constraints = RigidbodyConstraints.None;
    }
}
