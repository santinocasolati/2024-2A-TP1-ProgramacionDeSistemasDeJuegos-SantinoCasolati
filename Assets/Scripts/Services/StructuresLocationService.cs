using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuresLocationService : BaseService
{
    private List<Structure> structuresStore = new List<Structure>();

    public event Action OnStoreModified = delegate { };

    public void Register(Structure structure)
    {
        if (structuresStore.Contains(structure))
        {
            Debug.LogError("Structure instance is already registered");
            return;
        }

        structuresStore.Add(structure);
        OnStoreModified?.Invoke();
    }

    public void Unregister(Structure structure)
    {
        if (!structuresStore.Contains(structure))
        {
            Debug.LogError("Structure has not been registered");
            return;
        }

        structuresStore.Remove(structure);
        OnStoreModified?.Invoke();
    }

    public GameObject GetClosestStructureLocation(Vector3 startPoint)
    {
        GameObject closest = null;

        foreach (Structure structure in structuresStore)
        {
            if (closest == null || Vector3.Distance(startPoint, structure.transform.position) < Vector3.Distance(startPoint, closest.transform.position))
            {
                closest = structure.gameObject;
            }
        }

        return closest;
    }
}
