using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnit : MonoBehaviour
{
    #region State Machine
    public UnitStateMachine UnitStateMachine { get; private set; }
    public TestUnitIdle UnitIdleState { get; private set; }
    public TestUnitMove UnitMoveState { get; private set; }
    public TestUnitAttackState UnitAttackState { get; private set; }
    public TestUnitMeleeAttack UnitMeleeAttackState { get; private set; }
    public TestUnitMoveToAttack UnitMoveToAttack { get; private set; }

    #endregion

    #region Components
    public Core Core {get;private set;}
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public AnimationToStateMachine Atsm { get; private set; }
    public GameObject selectedSprite;
    protected Movement moveToPosition;

    #endregion

    #region Data
    [SerializeField] TestUnitData UnitData;
    [SerializeField] Transform attackPosition;
    private float attackTimer;
    #endregion

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        UnitStateMachine = new UnitStateMachine();
        UnitIdleState = new TestUnitIdle(this, UnitStateMachine, UnitData, "idle");
        UnitMoveState = new TestUnitMove(this, UnitStateMachine, UnitData, "move");
        UnitAttackState = new TestUnitAttackState(this, UnitStateMachine, UnitData, "melee", attackPosition);
        UnitMeleeAttackState = new TestUnitMeleeAttack(this, UnitStateMachine, UnitData, "melee", attackPosition);
        UnitMoveToAttack = new TestUnitMoveToAttack(this, UnitStateMachine, UnitData, "move", attackPosition);

        moveToPosition = GetComponent<Movement>();
        //UnitAttackState.attackFinishTime = Time.unscaledTime;
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        Atsm = GetComponent<AnimationToStateMachine>();

        UnitStateMachine.Initialize(UnitIdleState);
        selectedSprite.gameObject.SetActive(false);
        UnitAttackState.CanAttack = true;
        attackTimer = UnitData.attackDelay;
    }

    private void Update()
    {
        Core.LogicUpdate();
        UnitStateMachine.CurrentState.LogicUpdate();

        #region Attack Timers
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            UnitAttackState.CanAttack = true;
            attackTimer = UnitData.attackDelay;
        }
        else
        {
            UnitAttackState.CanAttack = false;
        }
        #endregion
    }
    public void SetSelectedVisible(bool visibilty)
    {
        selectedSprite.SetActive(visibilty);
    }
    public void CountDown()
    {
        //UnitData.attackDelay -= Time.deltaTime;
        if(UnitData.attackDelay == 0f)
        {
            UnitAttackState.CanAttack = true;
        }
        else
        {
            UnitAttackState.CanAttack = false;
        }
    }
    public void MoveTo(Vector3 targetPosition)
    {
        moveToPosition.SetPosition(targetPosition);
        //moveToPosition = new Vector3 Core.Movement.SetPosition(targetPosition);
    }
}
