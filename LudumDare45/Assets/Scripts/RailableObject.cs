using UnityEngine;

public abstract class RailableObject : MonoBehaviour
{
    [SerializeField]
    protected float verticalBaseSpeed = 1f;
    private float startingVerticalSpeed = 0f;


    protected virtual void Awake()
    {
        startingVerticalSpeed = verticalBaseSpeed;
    }
    protected virtual void Start()
    {
        AddObjectToRail();        
    }

    private void OnEnable()
    {
        verticalBaseSpeed = startingVerticalSpeed;
    }

    public void AddObjectToRail()
    {
        ObjectRail.Instance.AddObjectToRail(this);
    }

    public void MoveVerticallyOnRail(float verticalSpeed)
    {
        transform.localPosition += Vector3.back * verticalBaseSpeed * verticalSpeed * Time.deltaTime;
    }

    public void ResetBaseVerticalSpeed()
    {
        verticalBaseSpeed = 1f;
    }
}

