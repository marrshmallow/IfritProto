using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 이 적 물체는 플레이어를 공격하지 않음
    /// 관통 가능
    /// TODO: 플레이어가 스스로의 자동 공격 범위 안에 들어와야  
    /// </summary>
    public class InfernalNail : MonoBehaviour
    {
        public float hp = 1750f; // 못 체력
        public static bool NailSpawned;

        private void Start()
        {
            NailSpawned = true;
            Debug.Log("Inferno Nail spawned!");
        }
        
        private void Update()
        {
            if (hp <= 0f) Destroy(gameObject);
        }
        
        private void OnDisable()
        {
            NailSpawned = false;
            Debug.Log("Inferno Nail destroyed!");
            if (!GameManager.CheckpointPassed) return;
            EventManager.Instance.OnCheckpointPassed();
            EventManager.Instance.OnPhaseSwitched();
        }

        public void OnDamage(float damage)
        {
            // 코드: Boss.cs 참조
            hp -= damage;
            if (hp <= 0f)
            {
                hp = 0f;
            }
        }
    }
}