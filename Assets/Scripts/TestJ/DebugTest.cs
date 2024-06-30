using TMPro;
using UnityEngine;

namespace TestJ
{
    public class DebugTest : MonoBehaviour
    {
        public TextMeshProUGUI bossHp;
        public TextMeshProUGUI nailHp;
        public TextMeshProUGUI timeLimit;
        public TextMeshProUGUI bossState;
        public TextMeshProUGUI phase;
        public TextMeshProUGUI gameState;
        public TextMeshProUGUI distance;

        private Boss _boss;
        private InfernalNail infernalNail;
        private PlayerDetector _bossSensor;
        private BattlePhaseChecker phaseChecker;
        private float time;
        private BattlePhaseChecker.Phase currentPhase;
        private BattleStateChecker.BattleState currentGameState;
        private EEnemyState currentBossState;
        private PursuePlayer _pursuePlayer;

        private void Start()
        {
            EventManager.Instance.PhaseSwitch += FindNail;
            
            _boss = FindFirstObjectByType<Boss>();
            _bossSensor = FindFirstObjectByType<PlayerDetector>();
            _pursuePlayer = FindFirstObjectByType<PursuePlayer>();
            phaseChecker = FindFirstObjectByType<BattlePhaseChecker>();
        }

        private void Update()
        {
            if (infernalNail != null)
            {
                int j = (int)infernalNail.hp;
                nailHp.text = $"Nail HP: {j}";
            }
            
            int i = (int)_boss.Hp;
            bossHp.text = $"Boss HP: {i}";
            currentPhase = BattlePhaseChecker.CurrentPhase;
            phase.text = $"Phase: {currentPhase}";
            currentGameState = BattleStateChecker.CurrentState;
            gameState.text = $"State: {currentGameState}";
            currentBossState = PlayerDetector.BossState;
            bossState.text = $"Boss State: {currentBossState}";
            timeLimit.text = $"Time Limit: {time}";
        }

        private void FindNail()
        {
            if (currentPhase != BattlePhaseChecker.Phase.B) return;
            infernalNail = FindAnyObjectByType<InfernalNail>();
        }
    
        private void FixedUpdate()
        {
            if (distance)
            {
                distance.text = $"Distance: {_pursuePlayer.distance}";
            }
        }

        private void OnDisable()
        {
            EventManager.Instance.PhaseSwitch -= FindNail;
        }
    }
}