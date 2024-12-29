using UnityEngine;

public class IntParameterRandomSetter : StateMachineBehaviour
{
    public string IntParameterName;
    public int StateCount = 0;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(IntParameterName, Random.Range(0, StateCount));
    }
}