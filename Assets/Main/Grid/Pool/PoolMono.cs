using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour // ����� � ������ ������������� �� �������������
{
    private List<T> _pool;

    public T Prefab { get; }
    public Transform PoolContainer { get; }


    public PoolMono(T prefab, int count, Transform poolContainer)
    {
        Prefab = prefab;
        PoolContainer = poolContainer;

        CreatePool(count);
    }

    //public PoolMono(List<T> zonePrefab, Transform poolContainer)
    //{
    //    PoolContainer = poolContainer;

    //    CreateZone(zonePrefab, PoolContainer, 4);
    //}
    

    // ���� ����� � ����, ����� �������������� ������������ Tile � ZonePrefab, ��� �� ��������� ��������

    // ���� ���������� ���, ��� �� ���� ����������� � ����
    // ����� �� ���������� ����� � SetActive
    public void ArrangeTriggerZone(IReadOnlyList<Vector3> zonePositions, TriggerZone triggerZone, Transform poolContainer)
    {
        if (zonePositions != null && zonePositions.Count > 0)
        {
            for (int i = 0; i < zonePositions.Count; i++)
            {
                var spawnedBarrier = Object.Instantiate(triggerZone, zonePositions[i], Quaternion.identity, poolContainer);

                spawnedBarrier.name = $"Trigger Zone {spawnedBarrier.transform.position.x} {spawnedBarrier.transform.position.y}";
            }
        }
        else
            return;
    }

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
        foreach (var tile in _pool)
        {
            if (!tile.gameObject.activeInHierarchy)
            {
                element = tile;

                tile.gameObject.SetActive(true);

                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeTile()
    {
        if (HasFreeElement(out var element))
            return element;

        throw new System.Exception("��� ��������� �����!");
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateTile();
        }
    }

    private T CreateTile(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(Prefab, PoolContainer.position, Quaternion.identity, PoolContainer);

        createdObject.gameObject.SetActive(isActiveByDefault);

        _pool.Add(createdObject);

        return createdObject;
    }
}
