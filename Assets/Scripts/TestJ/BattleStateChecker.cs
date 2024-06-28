using UnityEngine;

namespace TestJ
{
    // TODO: 조건문 다시 확인 (제대로 전환이 안되고 있음)
    public class BattleStateChecker : MonoBehaviour
    {
        public enum BattleState // TODO: set to private later on
        {
            Default,
            Battle
        }

        public static BattleState _currentState; // TODO: Set to private later on
        private BattleState _newState;
        private float _playerHp; // 플레이어 쪽에서 불러오기

        private void Start()
        {
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
            if (BattlePhaseChecker.CurrentPhase == BattlePhaseChecker.Phase.Default)
            {
                _newState = BattleState.Default;
            }
            else
            {
                _newState = BattleState.Battle;
            }
            
            if (_currentState == _newState) return;
            EventManager.Instance.OnBattleStateChanged();
        }

        private void UpdateState()
        {
            _currentState = _newState;
        }
    }
}
