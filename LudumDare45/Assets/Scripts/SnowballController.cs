using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballController : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField]
    private float maxVerticalSpeed = 1f;
    [SerializeField]
    private float turnSpeed = 5f;
    private float currentSpeed = 0f;

    [Header("Growth Properties")]
    [SerializeField]
    float speedOfGrowth = .02f;
    [SerializeField]
    float maxSize = 3f;
    [SerializeField]
    [Range(0.001f, 1f)]
    float minSizePercentage = .02f;
    Rigidbody rb;

    public float MaxSizePercentage { get { return transform.localScale.x / maxSize; } }

    [Header("Control Flags")]
    public bool canGrow = true;
    public bool canMoveHorizontal = true;
    public bool canMoveVertical = true;

    [Header("Spinning Aesthetic")]
    private ConstantForce constantTorqueForce;
    [SerializeField]
    private float baseTorqueSpeed = 20f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        constantTorqueForce = GetComponent<ConstantForce>();
        transform.localScale = maxSize * minSizePercentage * Vector3.one;
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
            var hor = Input.GetAxis("Horizontal");
            if (Mathf.Abs(hor) > 0f)
            {
                ObjectRail.Instance.MoveSnowballHorizontally(hor * turnSpeed * MaxSizePercentage);
            }
        }
    }

    private void MoveDownhill()
    {
        if (canMoveVertical)
        {
            ObjectRail.Instance.MoveSnowballVertically(maxVerticalSpeed * MaxSizePercentage);
            constantTorqueForce.torque = new Vector3(baseTorqueSpeed * MaxSizePercentage, 0f, 0f);
        }
    }

    private void CheckForGrow()
    {
        if (canGrow && canMoveVertical && MaxSizePercentage <= 1f)
        {
            ChangeSnow(speedOfGrowth);
        }
    }

    public void ToggleMovement(bool enabled)
    {
        canMoveHorizontal = enabled;
        canMoveVertical = enabled;
        constantTorqueForce.torque = enabled ? new Vector3(baseTorqueSpeed * MaxSizePercentage, 0f, 0f) : Vector3.zero;
    }

    public void ChangeSnow(float snowChangePercentage)
    {
        transform.localScale += Vector3.one * snowChangePercentage * maxSize;

        if (MaxSizePercentage > 1f)
        {
            transform.localScale = Vector3.one * maxSize;
        }
        else if(transform.localScale.x < minSizePercentage * maxSize)
        {
            transform.localScale = Vector3.one * minSizePercentage * maxSize;
        }

        UIManager.Instance.UpdateSnowbar(MaxSizePercentage);
    }


}
