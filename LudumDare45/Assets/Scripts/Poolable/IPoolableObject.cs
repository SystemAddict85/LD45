using UnityEngine;

public interface IPoolableObject<T> where T : MonoBehaviour
{
    void EnablePoolableObject();
    void DisablePoolableObject();    
    void SetSpawner(PoolableSpawner<T> spawner);
}

