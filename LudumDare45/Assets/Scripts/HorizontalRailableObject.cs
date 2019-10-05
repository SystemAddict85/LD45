using UnityEngine;

public class HorizontalRailableObject : RailableObject, IHorizontalRailObject
{
    [SerializeField]
    protected float horizontalBaseSpeed = 1f;

    public void MoveHorizontallyOnRail(float horizontalMovement)
    {
        transform.localPosition += Vector3.right * horizontalBaseSpeed * Time.deltaTime * horizontalMovement;
    }
}

