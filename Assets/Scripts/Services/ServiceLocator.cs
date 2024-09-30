using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static ServiceLocator _instance;

    public static ServiceLocator Instance { get { return _instance; } }

    private Dictionary<System.Type, BaseService> _registeredServices = new();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterService(System.Type serviceType, BaseService service)
    {
        if (_registeredServices.ContainsKey(serviceType))
        {
            Debug.LogError($"ERROR: There is already a service of type {serviceType} registered");
            return;
        }

        _registeredServices.Add(serviceType, service);
    }

    public void UnregisterService(System.Type serviceType)
    {
        if (!_registeredServices.ContainsKey(serviceType))
        {
            Debug.LogError($"ERROR: There is no service of type {serviceType} registered");
            return;
        }

        _registeredServices.Remove(serviceType);
    }

    public T AccessService<T>() where T : class
    {
        if (!_registeredServices.ContainsKey(typeof(T)))
        {
            Debug.LogError($"ERROR: Trying to access unregistered service of type {typeof(T)}");
            return default;
        }

        return _registeredServices[typeof(T)] as T;
    }
}
