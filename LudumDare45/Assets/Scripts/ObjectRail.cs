using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRail : SimpleSingleton<ObjectRail>
{
    public List<IVerticalRailOnlyObject> verticalOnlyObjects = new List<IVerticalRailOnlyObject>();
    public List<IHorizontalRailObject> horizontalRailObjects = new List<IHorizontalRailObject>();

    public void AddObjectToRail(RailableObject railObject) {
        
        var horizontal = railObject.GetComponent<IHorizontalRailObject>();
        if (horizontal != null)
        {
            horizontalRailObjects.Add(horizontal);
        }
        else
        {
            verticalOnlyObjects.Add(railObject.GetComponent<IVerticalRailOnlyObject>());
        }        
    }

    private void MoveObjectsInHorizontalRail(float horizontalMove)
    {
        foreach(var h in horizontalRailObjects)
        {
            h.MoveHorizontallyOnRail(horizontalMove);
        }
    }

    private void MoveObjectsInVerticalRail(float verticalSpeed)
    {
        foreach(var v in verticalOnlyObjects)
        {
            v.MoveVerticallyOnRail(verticalSpeed);
        }
        foreach (var h in horizontalRailObjects)
        {
            h.MoveVerticallyOnRail(verticalSpeed);
        }
    }

    public void MoveSnowballHorizontally(float horizontalMovement)
    {
        MoveObjectsInHorizontalRail(-horizontalMovement);
    }

    public void MoveSnowballVertically(float verticalMovement)
    {
        MoveObjectsInVerticalRail(verticalMovement);
    }
}

