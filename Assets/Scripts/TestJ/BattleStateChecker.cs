using UnityEngine;

namespace TestJ
{
    // TODO: 조건문 다시 확인 (제대로 전환이 안되고 있음)
    public class BattleStateChecker : MonoBehaviour
    {
        public enum BattleState
        {
            Default,
            Battle,
            Victory,
            Defeat
        }

        private Boss _boss;
        public static BattleState CurrentState;
        private BattleState _newState;
        private float _playerHp; // 플레이어 쪽에서 불러오기

        private void Start()
        {
            _boss = FindFirstObjectByType<Boss>();
            _playerHp = 100f;
            //_player = FindFirstObjectByType<Player>();
            EventManager.EventManagerInstance.BattleStateChange += Test;
        }

        private void Update()
        {
            ChangeState();

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _playerHp = 0f;
            }
        }

        private void ChangeState()
        {
            if (_boss.Hp <= 0f && _playerHp > 0f)
            {
                _newState = BattleState.Victory;
            }
            else if (_boss.Hp < 0f && _playerHp <= 0f)
            {
                _newState = BattleState.Defeat;
            }
            else if (BattlePhaseChecker.CurrentPhase == BattlePhaseChecker.Phase.Default)
            {
                _newState = BattleState.Default;
            }
            
            if (CurrentState == _newState) return;
            CurrentState = _newState;
            EventManager.EventManagerInstance.OnBattleStateChanged();
        }

        private void Test()
        {
            Debug.Log("Battle state change event invoked!");
        }

        public void StartBattle()
        {
            CurrentState = BattleState.Battle;
            Debug.Log("!");
        }
    }
}
