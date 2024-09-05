using Unity.VisualScripting;
using UnityEngine;

public class EnemyDieState : StateMachineBehaviour
{
    // OnStateExit�� �ִϸ��̼� ���¿��� ���� �� ȣ���
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.ObjectPool.ReturnToPool("Enemy", animator.GetComponent<Enemy>().gameObject);
    }
}
