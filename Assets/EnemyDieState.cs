using Unity.VisualScripting;
using UnityEngine;

public class EnemyDieState : StateMachineBehaviour
{
    // OnStateExit는 애니메이션 상태에서 나갈 때 호출됨
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.ObjectPool.ReturnToPool("Enemy", animator.GetComponent<Enemy>().gameObject);
    }
}
