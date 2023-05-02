using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBodySafeDestroy : MonoBehaviour
{
   [SerializeField] private float destroyHeight;

   private void Awake()
   {
      destroyHeight = Mathf.Abs(destroyHeight);
   }

   private void Update()
   {
      if (transform.position.y < -destroyHeight)
      {
         Destroy(gameObject);
      }
   }
}
