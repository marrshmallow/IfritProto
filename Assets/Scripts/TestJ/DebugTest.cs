using System;
using TMPro;
using UnityEngine;

namespace TestJ
{
    public class DebugTest : MonoBehaviour
    {
        public TextMeshProUGUI bossHp;
        public TextMeshProUGUI bossState;
        public TextMeshProUGUI phase;
        public TextMeshProUGUI gameState;
        public TextMeshProUGUI distance;

        private Boss _boss;
        private PlayerDetector _bossSensor;
        private BattlePhaseChecker.Phase currentPhase;
        private BattleStateChecker.BattleState currentGameState;
        private EEnemyState currentBossState;
        private PursuePlayer _pursuePlayer;

        private void Start()
        {
            _boss = FindFirstObjectByType<Boss>();
            _bossSensor = FindFirstObjectByType<PlayerDetector>();
            _pursuePlayer = FindFirstObjectByType<PursuePlayer>();
        }

        private void Update()
        {
            int i = (int)_boss.Hp;
            bossHp.text = $"Boss HP: {i}";
            currentPhase = BattlePhaseChecker.CurrentPhase;
            phase.text = $"Phase: {currentPhase}";
            currentGameState = BattleStateChecker._currentState;
            gameState.text = $"State: {currentGameState}";
            currentBossState = _bossSensor.myState;
            bossState.text = $"Boss State: {currentBossState}";
        }

        private void FixedUpdate()
        {
            if (distance)
            {
                distance.text = $"Distance: {_pursuePlayer.result}";
            }
        }
    }
}
