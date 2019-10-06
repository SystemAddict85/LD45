using UnityEngine;

public abstract class PoolableSpawner<T> : MonoBehaviour where T : MonoBehaviour {

    [SerializeField]
    protected PrefabPool<T> _pool;

    [SerializeField]
    protected T _prefab;

    [SerializeField]
    private int poolQty = 25;

    protected GameObject _poolableParent;

    protected virtual void Awake()
    {
        _poolableParent = new GameObject("PoolableParent");
        _pool = new PrefabPool<T>(this, _prefab, poolQty, _poolableParent.transform);
        _pool.FillPool();
        _poolableParent.transform.position = Vector3.zero;
    }

    public virtual void ReturnToPool(T obj)
    {
        _pool.ReturnToPool(obj);
    }

    public virtual T GetFromPool()
    {
        var obj = _pool.GetFromPool();
        return obj;
    }
}

