using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour // Класс Т должен наследоваться от монобехейвера
{
    private List<T> _pool;

    public T Prefab;
    public Transform PoolContainer { get; private set; }

    public PoolMono(T prefab,  Transform poolContainer)
    {
        _pool = new List<T>();

        CreatePool(prefab, poolContainer);
    }

    public int Count => _pool.Count;

    public virtual void OnSpawn() { }

    public virtual void OnDespawn() { }

    public T Spawn()
    {
        if (HasFreeElement(out var element))
            return element;
        else
            return CreateItem(true);
    }

    public void DeSpawn(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);

        _pool.Add(spawnedObject);
    }

    private void CreatePool(T prefab, Transform poolContainer)
    {
        Prefab = prefab;
        PoolContainer = poolContainer;

        CreateItem();
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
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;

                item.gameObject.SetActive(true);

                return true;
            }
        }

        element = null;

        return false;
    }
}