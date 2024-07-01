using UnityEngine;

namespace TestJ
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance; 
        private bool _checkpointPassed;
        public static bool CheckpointPassed;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            EventManager.Instance.PassCheckpoint += PassCheckpoint;
            EventManager.Instance.PhaseSwitch += EndGame;
        }
        
        private void PassCheckpoint()
        {
            _checkpointPassed = true;
            CheckpointPassed = _checkpointPassed;
        }

        private void OnDisable()
        {
            EventManager.Instance.PassCheckpoint -= PassCheckpoint;
            EventManager.Instance.PhaseSwitch -= EndGame;
        }

        private void EndGame()
        {
            if (BattlePhaseChecker.CurrentPhase == BattlePhaseChecker.Phase.GameOver ||
                BattlePhaseChecker.CurrentPhase == BattlePhaseChecker.Phase.GameClear)
            {
                EventManager.Instance?.OnGameEnded();
                Debug.Log($"Activated @ {BattlePhaseChecker.CurrentPhase}");
            }
        }
    }
}
