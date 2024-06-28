using UnityEngine;

namespace TestJ
{
    // TODO: 조건문 다시 확인 (제대로 전환이 안되고 있음)
    public class BattleStateChecker : MonoBehaviour
    {
        public enum BattleState
        {
            Default,
            Battle
        }

        public static BattleState CurrentState;
        private BattleState _newState;
        private float _playerHp; // 플레이어 쪽에서 불러오
        private Boss _boss;

        private void Start()
        {
            _boss = FindFirstObjectByType<Boss>();
            _playerHp = 100f;
            //_player = FindFirstObjectByType<Player>();
            EventManager.Instance.BattleStateChange += UpdateState;
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
            if (_playerHp <= 0f || _boss.Hp <= 0f)
            {
                _newState = BattleState.Default;
            }
            else if (PlayerDetector.BossState == EEnemyState.Idle)
            {
                //if ()
            }
        }

        private void UpdateState()
        {
            CurrentState = _newState;
        }
    }
}
