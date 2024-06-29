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
        }
        
        private void PassCheckpoint()
        {
            _checkpointPassed = true;
            CheckpointPassed = _checkpointPassed;
        }

        private void OnDisable()
        {
            EventManager.Instance.PassCheckpoint -= PassCheckpoint;
        }
    }
}
