using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 보스의 HP를 읽어서 조건을 만족하면 Phase 상태를 변경시켜주고
    /// OnPhaseChanged 이벤트를 발생시킨다.
    ///
    /// 보스는 OnPhaseChanged 이벤트를 구독하며,
    /// 이벤트가 발생할 때마다 Phase 상태를 체크하고 공격모드를 변경한다.
    /// </summary>
    public class BattlePhaseChecker : MonoBehaviour
    {
        private enum Phase
        {
            Default,
            A,
            B,
            CheckPoint,
            Final
        }
        
        private Boss _boss;
        private Phase _phase;
        private Phase _newPhase;
        
        private void Start()
        {
            _boss = FindFirstObjectByType<Boss>();
        }

        private void Update()
        {
            SwitchPhase();
        }

        private void SwitchPhase()
        {
            switch (_boss.Hp)
            {
                case <= 0f:
                {
                    _newPhase = Phase.Default;
                }
                    break;
                case <= 22f:
                {
                    _newPhase = Phase.Final;
                }
                    break;
                case <= 45f:
                {
                    _newPhase = Phase.CheckPoint;
                }
                    break;
                case <= 75f:
                {
                    _newPhase = Phase.B;
                }
                    break;
                case <= 100f:
                {
                    _newPhase = Phase.A;
                }
                    break;
            }
            
            // 매 프레임마다 실행된다 (이 사이에 뭔가 값을 주면 평생 고정)
            if (_phase != Phase.Default && _phase == _newPhase)
            {
                Debug.Log($"Cannot switch phase! Current phase: {_phase}");
                return;
            }
            
            // 이 밑으로는 딱 한 번 씩만 실행된다.
            Debug.Log($"Switched {_phase} to {_newPhase}");
            _phase = _newPhase;
            // TODO: Invoke OnSwitchedPhase event (Listener: Boss)
            // TODO: Boss will change Attack Pattern when OnSwitchedPhase event is invoked
            // 1. Event is invoked
            // 2. PhaseChecker emits
            // TODO: How to design Attack Pattern
        }
    }
}