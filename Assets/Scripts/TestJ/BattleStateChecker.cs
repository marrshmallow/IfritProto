using UnityEngine;

namespace TestJ
{
    public class BattleStateChecker : MonoBehaviour
    {
        public enum BattleState
        {
            Peace,
            Battle
        }

        public static BattleState CurrentState;
        private BattleState _newState;
        private float _playerHp; // 플레이어 쪽에서 불러오기

        private void Start()
        {
            _playerHp = 100f;
            //_player = FindFirstObjectByType<Player>();
            EventManager.Instance.BattleStateChange += ConfirmState;
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
            if (_playerHp <= 0f ||
                PlayerDetector.BossState == EEnemyState.Dead ||
                PlayerDetector.BossState == EEnemyState.Idle)
            {
                _newState = BattleState.Peace;
            }
            else if (PlayerDetector.BossState == EEnemyState.Evoked)
            {
                _newState = BattleState.Battle;
            }
            
            if (CurrentState != _newState)
            {
                EventManager.Instance?.OnBattleStateChanged();
            }
        }

        private void ConfirmState()
        {
            CurrentState = _newState;
        }

        private void OnDisable()
        {
            EventManager.Instance.BattleStateChange -= ConfirmState;
        }
    }
}
