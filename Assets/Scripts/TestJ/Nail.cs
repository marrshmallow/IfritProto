using UnityEngine;
using UnityEngine.Serialization;

namespace TestJ
{
    /// <summary>
    /// 이 적 물체는 플레이어를 공격하지 않음
    /// 관통 가능
    /// TODO: 플레이어가 스스로의 자동 공격 범위 안에 들어와야  
    /// </summary>
    public class Nail : MonoBehaviour
    {
        public float hp; // 못 체력

        private void Update()
        {
            if (hp <= 0f) Destroy(gameObject);
        }
        
        private void OnDisable()
        {
            if (!GameManager.CheckpointPassed) return;
            EventManager.Instance.OnCheckpointPassed();
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