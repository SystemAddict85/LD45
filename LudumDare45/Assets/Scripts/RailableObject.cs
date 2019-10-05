using UnityEngine;

public abstract class RailableObject : MonoBehaviour
{
    [SerializeField]
    protected float baseSpeed = 1f;
    
    protected void Start()
    {
        AddObjectToRail();
    }

    public void AddObjectToRail()
    {
        ObjectRail.Instance.AddObjectToRail(this);
    }

    public void MoveVerticallyOnRail(float verticalSpeed)
    {
        transform.localPosition += Vector3.back * baseSpeed * verticalSpeed * Time.deltaTime;
    }
}

