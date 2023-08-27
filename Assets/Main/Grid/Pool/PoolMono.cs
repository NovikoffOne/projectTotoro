using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour // Класс Т должен наследоваться от монобехейвера
{
    private List<T> _pool;

    public T Prefab;
    public Transform PoolContainer { get; private set; }

    public PoolMono(T prefab,  Transform poolContainer, int count)
    {
        _pool = new List<T>();

        AddPool(prefab, poolContainer, count);
    }

    public int Count => _pool.Count;

    public void ActiveAll()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            _pool[i].gameObject.SetActive(true);
        }
    }

    public void DeactiveAll()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            _pool[i].gameObject.SetActive(false);
        }
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

    public T GetFree()
    {
        if (HasFreeElement(out var element))
            return element;

        throw new System.Exception("Нет свободных ячеек!");
    }

    public void AddPool(T prefab, Transform poolContainer, int count)
    {
        Prefab = prefab;
        PoolContainer = poolContainer;

        for (int i = 0; i < count; i++)
            CreateItem();
    }

    private T CreateItem(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(Prefab, PoolContainer.position, Quaternion.identity, PoolContainer);

        createdObject.gameObject.SetActive(isActiveByDefault);

        _pool.Add(createdObject);

        return createdObject;
    }
}
