using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour, IHittableObject, IPoolableObject<Obstacle>
{

    Animator anim;
    bool hasBeenHit = false;
    private PoolableSpawner<Obstacle> spawner;

    // use negative for snowLoss, positive for gain
    [Range(-1f, 1f)]
    public float snowChangePercentage;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public virtual void GotHit(GameObject objectThatHit)
    {
        if (anim)
        {
            anim.SetTrigger("hit");
        }

        GetComponent<RailableObject>().ResetBaseVerticalSpeed();
        
        if (objectThatHit.GetComponent<Snowball>())
            Snowball.Instance.gotHit(this);
    }

    public void OnEnable()
    {
        if (anim)
            anim.ResetTrigger("hit");

        hasBeenHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenHit)
        {
            hasBeenHit = true;
            GotHit(other.gameObject);
        }
    }

    public void EnablePoolableObject()
    {
        GetComponent<Collider>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    public void DisablePoolableObject()
    {
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        spawner.ReturnToPool(this);
    }

    public void SetSpawner(PoolableSpawner<Obstacle> spawner)
    {
        this.spawner = spawner;
    }
}

public interface IHittableObject
{
    void GotHit(GameObject objectThatHit);
}


