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
        public InfernalNail infernalNail;
        public BattlePhaseChecker phaseChecker;
        public float time;
        public BattlePhaseChecker.Phase currentPhase;
        public BattleStateChecker.BattleState currentGameState;
        public EEnemyState currentBossState;
        private PursuePlayer _pursuePlayer;

        public int i;

        private void Start()
        {
            EventManager.Instance.PhaseSwitch += FindNail;
            
            _boss = FindFirstObjectByType<Boss>();
            _pursuePlayer = FindFirstObjectByType<PursuePlayer>();
            phaseChecker = FindFirstObjectByType<BattlePhaseChecker>();
        }

        private void Update()
        {
            if (infernalNail != null)
            {
                int j = (int)infernalNail.hp;
                nailHp.text = $"Nail HP: {j}";
                time = BattlePhaseChecker.timeLimit;
                timeLimit.text = $"Time Limit: {time}";
            }
            
            i = (int)_boss.Hp;
            bossHp.text = $"Boss HP: {i}";
            currentPhase = BattlePhaseChecker.CurrentPhase;
            phase.text = $"Phase: {currentPhase}";
            currentGameState = BattleStateChecker.CurrentState;
            gameState.text = $"State: {currentGameState}";
            currentBossState = PlayerDetector.BossState;
            bossState.text = $"Boss State: {currentBossState}";
        }

        private void FindNail()
        {
            if (infernalNail == null) return;
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