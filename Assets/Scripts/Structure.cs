using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    private StructuresLocationService structuresLocationService;

    private void OnEnable()
    {
        StartCoroutine(WaitForService());
    }

    private IEnumerator WaitForService()
    {
        while (ServiceLocator.Instance == null)
        {
            yield return null;
        }

        while (structuresLocationService == null)
        {
            FetchComponents();
        }

        structuresLocationService.Register(this);
    }

    public void FetchComponents()
    {
        structuresLocationService ??= ServiceLocator.Instance.AccessService<StructuresLocationService>();
    }

    private void OnDisable()
    {
        if (structuresLocationService)
            structuresLocationService.Unregister(this);
    }
}
