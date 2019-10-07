using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour, IHittableObject, IPoolableObject<Obstacle>
{

    protected Animator anim;
    bool hasBeenHit = false;
    [HideInInspector]
    public PoolableSpawner<Obstacle> spawner;

    // use negative for snowLoss, positive for gain
    [Range(-1f, 1f)]
    public float snowChangePercentage;

    protected virtual void Awake()
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

    public virtual void OnEnable()
    {
        if (anim)
        {
            anim.ResetTrigger("hit");
            anim.Play("idle");
        }

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
        enabled = true;
    }

    public void DisablePoolableObject()
    {
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        enabled = false;
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


