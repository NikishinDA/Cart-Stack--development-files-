using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAddMoneyText : MonoBehaviour
{
    private void OnAnimEnd()
    {
        Destroy(gameObject);
    }
}
