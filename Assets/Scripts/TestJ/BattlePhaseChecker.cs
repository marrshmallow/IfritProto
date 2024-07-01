using System.Collections;
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
        public DebugTest debugTest; // TODO: Delete afterwards
        
        public enum Phase
        {
            Default = 0,
            A = 100,
            B = 200,
            CheckPoint = 300,
            Transition = 400, // Boss is Invincible, All attacks are canceled
            Final = 500,
            GameOver = 600,
            GameClear = 650
        }
        
        private Boss _boss;
        private const float MaxHp = 13900f;
        private const float ThresholdB = 0.76f;
        private const float ThresholdCheckpoint = 0.51f;
        private const float ThresholdFinal = 0.25f;

        public static Phase CurrentPhase;
        private Phase _newPhase;
        public static float timeLimit = 60f; // TODO: Set to private

        private void Start()
        {
            _boss = FindFirstObjectByType<Boss>();
            
            EventManager.Instance.PhaseSwitch += Test;
            EventManager.Instance.PhaseSwitch += SetTimer;
            Debug.Log($"Start. Now @ {CurrentPhase} / Boss HP: {debugTest.i} / Battle State: {debugTest.currentGameState} / Boss State: {debugTest.currentBossState} / Nail survived: {debugTest.infernalNail}");
        }

        private void Update()
        {
            Debug.Log($"Current phase: {CurrentPhase} / New phase: {_newPhase}");
            SwitchPhase();
        }

        private void SwitchPhase()
        {
            switch (_boss.Hp)
            {
                case <= 0f:
                {
                    _newPhase = Phase.GameClear;
                }
                    break;
                case < MaxHp * ThresholdFinal:
                {
                    _newPhase = GameManager.CheckpointPassed ? Phase.Transition : Phase.GameOver;
                }
                    break;
                case < MaxHp * ThresholdCheckpoint:
                {
                    _newPhase = Phase.CheckPoint;
                }
                    break;
                case < MaxHp * ThresholdB:
                {
                    _newPhase = Phase.B;
                }
                    break;
                case <= MaxHp:
                {
                    if (BattleStateChecker.CurrentState == BattleStateChecker.BattleState.Default)
                    {
                        return;
                    }
                    _newPhase = Phase.A;
                }
                    break;
                default:
                {
                    return;
                }
            }
            
            // 매 프레임마다 실행된다 (이 사이에 뭔가 값을 주면 평생 고정)
            if (CurrentPhase == _newPhase)
            {
                return;
            }
            
            // 이 밑으로는 딱 한 번 씩만 실행된다.
            //Debug.Log($"Switched {CurrentPhase} to {_newPhase}");
            // Invoke OnSwitchedPhase event (Listener: Boss)
            // TODO: Boss will change Attack Pattern when OnSwitchedPhase event is invoked
            // 1. Event is invoked
            EventManager.Instance.OnPhaseSwitched(); // OK
            Debug.Log($"OnPhaseSwitched: now @ {CurrentPhase} / Boss HP: {debugTest.i} / Battle State: {debugTest.currentGameState} / Boss State: {debugTest.currentBossState} / Nail survived: {debugTest.infernalNail}");
            // 지금 이 상태에서 아무것도 구독된 메소드가 없어서 null이라고 뜰 것
            // 2. PhaseChecker emits
        }

        private void Test()
        {
            CurrentPhase = _newPhase;
        }

        private void SetTimer()
        {
            if (CurrentPhase != Phase.B && InfernalNail.NailSpawned)
            {
                StartCoroutine(nameof(StartTimer));
            }
        }
        
        private IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(timeLimit);
            if (GameManager.CheckpointPassed)
            {
                _newPhase = Phase.Transition;
            }
            else
            {
                _newPhase = Phase.GameOver;
            }
        }
        
        private void OnDisable()
        {
            _boss = FindFirstObjectByType<Boss>();
            EventManager.Instance.PhaseSwitch -= Test;
            EventManager.Instance.PhaseSwitch -= SetTimer;
        }
    }
}