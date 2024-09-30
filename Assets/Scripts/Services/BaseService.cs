using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseService : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitForServiceLocator());
    }

    private void OnDisable()
    {
        Unregister();
    }

    private IEnumerator WaitForServiceLocator()
    {
        while (ServiceLocator.Instance == null)
        {
            yield return null;
        }

        Register();
    }

    private void Register()
    {
        ServiceLocator.Instance.RegisterService(this.GetType(), this);
    }

    private void Unregister()
    {
        ServiceLocator.Instance.UnregisterService(this.GetType());
    }
}
