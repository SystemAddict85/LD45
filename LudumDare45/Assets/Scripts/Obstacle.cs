using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour, IHittableObject
{

    Animator anim;
    bool hasBeenHit = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public virtual void GotHit()
    {
        if (anim)
        {
            anim.SetTrigger("hit");
        }
    }

    public void OnEnable()
    {
        if (anim)
            anim.ResetTrigger("hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenHit)
        {
            hasBeenHit = true;
            print($"hit {name}");
            GotHit();
        }
    }
}

public interface IHittableObject
{
    void GotHit();
}


