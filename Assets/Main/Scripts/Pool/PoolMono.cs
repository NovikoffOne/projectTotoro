using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T>
    where T : MonoBehaviour
{
    private readonly T Prefab;
    
    private readonly Transform PoolContainer;
    
    public readonly List<T> Pool;

    private List<T> _pool;

    public PoolMono(T prefab)
    {
        Prefab = prefab;

        _pool = new List<T>();

        PoolContainer = new GameObject().transform;

        Object.DontDestroyOnLoad(PoolContainer);
    }

    public int Count => _pool.Count;

    public virtual void OnSpawn(T element)
    {
        element.gameObject.SetActive(true);
    }

    public virtual void OnDespawn(T element)
    {
        element.gameObject.SetActive(false);
    }

    public T Spawn()
    {
        if (HasFreeElement(out var element))
        {
            OnSpawn(element);
            return element;
        }
        else
            return CreateItem(true);
    }

    public void DeSpawn(T spawnedObject)
    {
        OnDespawn(spawnedObject);

        _pool.Add(spawnedObject);
    }

    public void DeSpawnAll()
    {
        _pool.ForEach(element => OnDespawn(element));
    }

    private T CreateItem(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(Prefab, PoolContainer.position, Quaternion.identity, PoolContainer);

        createdObject.gameObject.SetActive(isActiveByDefault);

        _pool.Add(createdObject);

        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        if (element = _pool.Find(element => !element.gameObject.activeInHierarchy))
        {
            OnSpawn(element);
            return true;
        }
        else
        {
            return false;
        }
    }
}