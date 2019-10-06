using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PrefabPool<T> where T : MonoBehaviour
{

    public PrefabPool(PoolableSpawner<T> spawner, T prefab, int poolQty, Transform poolableParent)
    {
        _prefab = prefab;
        _poolHolding = poolQty;
        _spawner = spawner;
        _poolableParent = poolableParent;
    }      

    private T _prefab;
    private int _poolHolding;
    private PoolableSpawner<T> _spawner;
    private Transform _poolableParent;

    private Queue<T> inactivePool = new Queue<T>();
    [SerializeField]
    private List<T> activePool = new List<T>();

    private T SpawnPrefab()
    {
        T obj = MonoBehaviour.Instantiate(_prefab) as T;
        obj.transform.parent = _poolableParent;
        return obj;
    }

    public void FillPool()
    {
        for(int i = 0; i < _poolHolding; ++i)
        {
            var obj = SpawnPrefab().GetComponent<IPoolableObject<T>>();
            obj.SetSpawner(_spawner);
            obj.DisablePoolableObject();
        }
    }

    public T GetFromPool()
    {
        T obj;

        if (inactivePool.Count < _poolHolding * .25f)
            FillPool();

        obj = inactivePool.Dequeue();
        activePool.Add(obj);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        if(activePool.Contains(obj))
            activePool.Remove(obj);
        inactivePool.Enqueue(obj);
    }
}

