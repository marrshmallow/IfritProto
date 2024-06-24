using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 보스의 공격 상태 머신
    ///
    /// 여기에다가 웬만하면 기능은 안 넣는 게 좋다.
    /// 기능 구현은 바깥에서 해 두기.
    ///
    /// </summary>
    public class BossFSM : StateMachineBehaviour
    {
        private enum BossState
        {
            Idle,
            Invoked,
            FindPlayer, // 플레이어 위치 감지 후 제자리에서 방향 전환
            PursuePlayer,
            NormalAttack, // 일반 공격 (자동공격)
            SkillAttack, // 특수 기술
            FinalAttack, // 전멸기
            Dead
        }
    }
}
