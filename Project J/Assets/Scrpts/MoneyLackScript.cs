using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyLackScript : MonoBehaviour
{
    float _timer;

    void OnEnable()
    {
        Invoke("DisableThis", 1);
    }

    void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
