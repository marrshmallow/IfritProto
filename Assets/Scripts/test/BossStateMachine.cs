using UnityEngine;

namespace test
{
    /// <summary>
    /// 보스의 공격 상태 머신
    ///
    /// 여기에다가 웬만하면 기능은 안 넣는 게 좋다.
    /// 기능 구현은 바깥에서 해 두기.
    ///
    /// </summary>
    public class BossStateMachine : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 플레이어를 인식하면 공격 상태에 돌입한다
            // 플레이어를 향해 이동
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 공격 상태 중일 때 할 것들
            // 공격한다
        }
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 공격 상태가 끝나면 할 것들
            // 모든 공격을 멈춘다
            // 이동을 멈춘다
            // 보스전을 처음부터 시작한다 (단순 재시작이 아니고 재도전 카운트를 1 늘려준다. UI 표시: Restart)
        }
    }
}
