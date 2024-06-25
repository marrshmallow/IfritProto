using System;
using System.Collections;
using UnityEngine;

namespace TestJ.Event
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
        private Boss _boss;

        //private EventHandler OnPhaseChanged;
        private Action PhaseChanged;
        private Action Default;
        private Action StartPhaseA;
        private Action StartPhaseB;
        private Action StartCheckPoint;
        private Action StartFinalPhase;

        private void Start()
        {
            _boss = FindFirstObjectByType<Boss>().GetComponent<Boss>();
        }

        /*private IEnumerator CheckPhase()
        {
            if (_phaseA)
            {
                StartCoroutine(nameof(ChangePhase2A));
            }
            else if (_phaseB)
            {
                StartCoroutine(nameof(ChangePhase2B));
            }
            else if (_checkPoint)
            {
                StartCoroutine(nameof(Change2CheckPointPhase));
            }
            else if (_phaseFinal)
            {
                StartCoroutine(nameof(ChangePhase2Final));
            }
            else
            {
                StartCoroutine(nameof(ChangePhase2Default));
            }
            yield return null;
        }
        
        private IEnumerator ChangePhase2Default()
        {
            _phase = Phase.Default;
            Default?.Invoke();
            Debug.Log($"Boss HP: {_boss.Hp}, Current Phase: {_phase}");
            yield break;
        }

        private IEnumerator ChangePhase2A()
        {
            _phase = Phase.A;
            StartPhaseA?.Invoke();
            Debug.Log($"Boss HP: {_boss.Hp}, Current Phase: {_phase}");
            yield break;
        }

        private IEnumerator ChangePhase2B()
        {
            _phase = Phase.B;
            StartPhaseB?.Invoke();
            Debug.Log($"Boss HP: {_boss.Hp}, Current Phase: {_phase}");
            yield break;
        }

        private IEnumerator Change2CheckPointPhase()
        {
            _phase = Phase.CheckPoint;
            StartCheckPoint?.Invoke();
            Debug.Log($"Boss HP: {_boss.Hp}, Current Phase: {_phase}");
            yield break;
        }

        private IEnumerator ChangePhase2Final()
        {
            _phase = Phase.Final;
            StartFinalPhase?.Invoke();
            Debug.Log($"Boss HP: {_boss.Hp}, Current Phase: {_phase}");
            yield break;
        }*/
    }
}