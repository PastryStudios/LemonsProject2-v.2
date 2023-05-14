using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectPath : CoreComponent
{
    private Vector3 targetPosition;
    private float moveSpeed;
    [SerializeField] private Movement movement;
    public float FacingDirection { get; private set; }
    public Rigidbody2D RB { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
    }

    private void Start()
    {

    }

    public void SetMovePosition(Vector3 targetPosition, float moveSpeed)
    {
        this.targetPosition = targetPosition;
        this.moveSpeed = moveSpeed;
    }

    private void Update()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        if (Vector3.Distance(targetPosition, transform.position) < 0.5f) moveDirection = Vector3.zero;
        movement.Velocity(moveDirection, moveSpeed);
        //movement.CheckIfShouldFlip(moveDirection.x);
    }
    
}
