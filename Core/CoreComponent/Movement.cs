using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }

    private Vector3 workspace;
    public Vector3 CurrentVelocity { get; private set; }
    private Vector3 velocityVector;
    public int FacingDirection { get; private set; }
    public Vector2 unitDirection;


    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
        unitDirection = GetComponentInParent<Transform>().position;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
        //Debug.Log(FacingDirection);
    }

    #region Set Functions

    public void Velocity(Vector3 velocityVector, float moveSpeed)
    {
        RB.velocity = velocityVector * moveSpeed;
    }
    public void SetPosition(Vector3 targetPostion)
    {
        //return targetPosition;
        velocityVector = (targetPostion - transform.position).normalized;
        //Velocity(velocityVector);
    }
    public void CheckIfShouldFlip(float velocity)
    {
        if (RB.velocity.x != 0 && velocity != FacingDirection)
        {
            Flip();
        }
        //if (xInput != 0 && xInput != FacingDirection)
    }
    public void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0f, 180f, 0f);
    }

    //public void FlipFacingDirection()
    //{
    //    FacingDirection *= -1;
    //}

    #endregion
}
