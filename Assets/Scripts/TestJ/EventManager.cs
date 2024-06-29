using System;
using UnityEngine;

namespace TestJ
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public event Action PhaseSwitch;

        public void OnPhaseSwitched()
        {
            PhaseSwitch?.Invoke();
        }

        public event Action BattleStateChange;

        public void OnBattleStateChanged()
        {
            BattleStateChange?.Invoke();
        }

        public event Action InitiateBattle;

        public void OnBattleInitiated()
        {
            InitiateBattle?.Invoke();
        }

        public event Action PassCheckpoint;

        public void OnCheckpointPassed()
        {
            PassCheckpoint?.Invoke();
        }
    }
}
