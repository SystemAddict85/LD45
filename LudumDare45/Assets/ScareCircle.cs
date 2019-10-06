using UnityEngine;
// put this onto a child object to be scared, layer must be set to scarecircle to interact with snowball
[RequireComponent(typeof(SphereCollider))]
public class ScareCircle : MonoBehaviour
{
    private Animator anim;
    private bool isScared = false;

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
        GetComponent<SphereCollider>().isTrigger = true;
    }

    private void Update()
    {
        anim.SetBool("isScared", isScared);
    }

    private void OnTriggerEnter(Collider other)
    {
        isScared = true;    
    }

    private void OnTriggerExit(Collider other)
    {
        isScared = false;
    }
}


