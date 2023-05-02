using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCanvasController : MonoBehaviour
{
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        transform.forward = _cameraTransform.forward;
    }
}
