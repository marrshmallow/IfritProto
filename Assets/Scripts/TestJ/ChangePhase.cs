using System;
using UnityEngine;

namespace TestJ
{
    // Hp가 100f 이하일 때 ChangePhaseA 이벤트 송출
    // Hp가 75f 이하일 때 ChangePhaseB 이벤트 송출
    // Hp가 45f 이하일 때 ChangePhaseCheckPoint 이벤트 송출
    // HP가 22f 이하일 때 ChangePhaseFinal 이벤트 송출
    public class ChangePhase : MonoBehaviour
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
        private event Action Switch2PhaseA;
        private event Action Switch2PhaseB;
        private event Action Switch2PhaseCheckPoint;
        private event Action Switch2PhaseFinal;
        private event Action Switch2PhaseDefault;
            
        //private delegate void Name(); // 선언부. 델리게이트는 구현부 없이 선언부만 적는다.
        // 여기서 Name은 타입이다.
        //private Name a; // Name 타입 변수 a를 인스턴스화 했다. 이제 a에 메소드를 담을 수 있다.

        //private OnPhaseChanged _onPhaseChanged;

        private void Start()
        {
            _boss = FindFirstObjectByType<Boss>();
        }

        private void PhaseChange()
        {   
            switch (_boss.Hp)
            {
                case <= 0f:
                {
                    _phase = Phase.Default;
                    Switch2PhaseDefault?.Invoke(); // Event is raised
                    Debug.Log("Current phase: " + _phase);
                }
                    break;
                case <= 22f:
                {
                    _phase = Phase.Final;
                    Switch2PhaseFinal?.Invoke();
                    Debug.Log("Current phase: " + _phase);
                }   
                    break;
                case <= 45f:
                {
                    _phase = Phase.CheckPoint;
                    Switch2PhaseCheckPoint?.Invoke();
                    Debug.Log("Current phase: " + _phase);
                }
                    break;
                case <= 75f:
                {
                    _phase = Phase.B;
                    Switch2PhaseB?.Invoke();
                    Debug.Log("Current phase: " + _phase);
                }
                    break;
                case <= 100f:
                {
                    _phase = Phase.A;
                    Switch2PhaseA?.Invoke();
                    Debug.Log("Current phase: " + _phase);
                }
                    break;
                default:
                {
                    _phase = Phase.Default;
                    Switch2PhaseDefault?.Invoke();
                    Debug.Log("Current phase: " + _phase);
                }
                    break;
            }
        }

        protected virtual void OnSwitch2PhaseA()
        {
            Switch2PhaseA?.Invoke();
        }

        protected virtual void OnSwitch2PhaseB()
        {
            Switch2PhaseB?.Invoke();
        }

        protected virtual void OnSwitch2PhaseCheckPoint()
        {
            Switch2PhaseCheckPoint?.Invoke();
        }

        protected virtual void OnSwitch2PhaseFinal()
        {
            Switch2PhaseFinal?.Invoke();
        }

        protected virtual void OnSwitch2PhaseDefault()
        {
            Switch2PhaseDefault?.Invoke();
        }
    }
}