using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public TestUnitAttackState attackState;
    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }
    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
