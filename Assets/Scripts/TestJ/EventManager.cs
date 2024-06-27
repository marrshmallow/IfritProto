using System;
using UnityEngine;

namespace TestJ
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager EventManagerInstance;

        private void Awake()
        {
            EventManagerInstance = this;
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
    }
}
