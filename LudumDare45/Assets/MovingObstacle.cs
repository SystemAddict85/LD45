using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : Obstacle
{
    [SerializeField]
    private float horizontalMoveSpeed = 1f;

    [SerializeField]
    [MinMaxRange(2f, 6f)]
    private RangedFloat timeBetweenDirectionChanges;
    private float currentTime = 0f;
    private float timeToSwitch = 0f;
    private Vector3 moveDirection = Vector3.right;

    private Rigidbody rb;

    private bool canMove = true;
       
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        timeToSwitch = Global.GetRandomNumberInRange(timeBetweenDirectionChanges);
    }

    private void Update()
    {
       MakeDirectionDecision();
    }

    public void ToggleMove(bool enabled)
    {
        canMove = enabled;
    }

    public override void GotHit(GameObject objectThatHit)
    {
        base.GotHit(objectThatHit);
        ToggleMove(false);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        ToggleMove(true);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveObstacle();
            if (anim)
                anim.SetFloat("moveX", moveDirection.x);
        }
    }

    private void MoveObstacle()
    {
        rb.MovePosition(rb.position + moveDirection * horizontalMoveSpeed * Time.fixedDeltaTime);
    }

    private void MakeDirectionDecision()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToSwitch)
        {
            var lOrR = UnityEngine.Random.value;
            moveDirection = lOrR >= .5f ? Vector3.right : Vector3.left;

            currentTime = 0f;
            timeToSwitch = Global.GetRandomNumberInRange(timeBetweenDirectionChanges);
        }
    }
}


