using UnityEngine;

public class DeathSequece : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Uništavaš objekat kojem animator pripada
        Destroy(animator.gameObject);
    }
}
