using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 보스의 HP를 읽어서 조건을 만족하면 Phase 상태를 변경시켜주고
    /// OnPhaseSwitched 이벤트를 발생시킨다.
    ///
    /// BossState가 Evoked이면 작동 시작
    /// </summary>
    public class BattlePhaseChecker : MonoBehaviour
    {
        public enum Phase
        {
            Default = 0,
            A = 100,
            B = 200,
            CheckPoint = 300,
            //Transition = 400, // Boss is Invincible, All attacks are canceled
            Final = 500
        }
        
        private Boss _boss;
        public static Phase CurrentPhase;
        private Phase _newPhase;

        private void Start()
        {
            _boss = FindFirstObjectByType<Boss>();
            EventManager.Instance.PhaseSwitch += Test;
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
            if (CurrentPhase != Phase.Default && CurrentPhase == _newPhase)
            {
                //Debug.Log($"Cannot switch phase! Current phase: {CurrentPhase}");
                return;
            }
            
            // 이 밑으로는 딱 한 번 씩만 실행된다.
            //Debug.Log($"Switched {CurrentPhase} to {_newPhase}");
            // Invoke OnSwitchedPhase event (Listener: Boss)
            // TODO: Boss will change Attack Pattern when OnSwitchedPhase event is invoked
            // 1. Event is invoked
            EventManager.Instance.OnPhaseSwitched(); // OK
            // 지금 이 상태에서 아무것도 구독된 메소드가 없어서 null이라고 뜰 것
            // 2. PhaseChecker emits
        }

        private void Test()
        {
            CurrentPhase = _newPhase;
            //Debug.Log($"Phase switch event invoked!: NOW {CurrentPhase}");
        }
    }
}