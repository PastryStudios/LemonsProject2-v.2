using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnit : MonoBehaviour
{
    #region State Machine
    public EnemyStateMachine EnemyStateMachine { get; private set; }
    public EnemyIdleState EnemyIdleState { get; private set; }
    //public TestUnitMove UnitMoveState { get; private set; }
    //public TestUnitMeleeAttack UnitMeleeAttackState { get; private set; }
    //public TestUnitData UnitData {get;private set;}
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    //public GameObject Healthbar;
    //public HealthBar Healthbar { get; private set; }
    //public Slider slider;

    #endregion

    #region Data
    [SerializeField] EnemyData EnemyData;
    [SerializeField] HealthBar HealthBar;
    public float currentHealth;// = 100f;
    #endregion

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        EnemyStateMachine = new EnemyStateMachine();
        EnemyIdleState = new EnemyIdleState(this, EnemyStateMachine, EnemyData, "idle");
        
        //UnitMoveState = new TestUnitMove(this, UnitStateMachine, UnitData, "move");
        //UnitMeleeAttackState = new TestUnitMeleeAttack(this, UnitStateMachine, UnitData, "melee");

    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        //currentHealth = Core.Stats.maxHealth;

        //HealthBar.SetHealth(Core.Stats.currentHealth, Core.Stats.maxHealth);

        EnemyStateMachine.Initialize(EnemyIdleState);
       // slider.value = currentHealth;
        //slider = GetComponent<Slider>();
    }
    private void Update()
    {
        Core.LogicUpdate();
        EnemyStateMachine.CurrentState.LogicUpdate();

        //HealthBar.SetHealth(Core.Stats.currentHealth, Core.Stats.maxHealth);
        //Debug.Log("Health is " + currentHealth);
    }
    public void SetSelectedVisible(bool visibilty)
    {

    }
    public void SetHealthbarVisible(bool visibility)
    {
        //HealthBar.gameObject.SetActive(visibility);
        //HealthBar.SetHealthbarVisible(visibility);
    }
    //public void Damage()
    //{
    //    currentHealth -= 1f;
    //    //slider.value = currentHealth;
    //    Debug.Log("Health " + currentHealth);
    //}
    
}
