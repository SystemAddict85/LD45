using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballController : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField]
    private float verticalSpeed = 1f;
    [SerializeField]
    private float turnSpeed = 5f;

    [Header("Growth Properties")]
    [SerializeField]
    float speedOfGrowth = .02f;
    [SerializeField]
    float maxSize = 3f;
    Rigidbody rb;

    [Header("Control Flags")]
    public bool canGrow = true;
    public bool canMoveHorizontal = true;
    public bool canMoveVertical = true;

    [Header("Spinning Aesthetic")]
    private ConstantForce constantForce;
    [SerializeField]
    private float baseTorqueSpeed = 20f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        constantForce = GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGrow();
        MoveDownhill();
        SteerSnowball();
    }

    private void SteerSnowball()
    {
        if (canMoveHorizontal)
        {
            var hor = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(hor) > 0f)
            {
                ObjectRail.Instance.MoveSnowballHorizontally(hor * turnSpeed);
            }
        }
    }

    private void MoveDownhill()
    {
        if (canMoveVertical)
        {
            ObjectRail.Instance.MoveSnowballVertically(verticalSpeed);
        }
    }

    private void CheckForGrow()
    {
        if (canGrow && transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(speedOfGrowth, speedOfGrowth, speedOfGrowth);
        }
    }

    public void ToggleMovement(bool enabled)
    {
        canMoveHorizontal = enabled;
        canMoveVertical = enabled;
        constantForce.torque = enabled ? new Vector3(baseTorqueSpeed, 0f , 0f ) : Vector3.zero;
    }

    
}
