using UnityEngine;

namespace TestJ
{
    public class Boss : MonoBehaviour, IDamageable
    {
        private float _hp;

        public float Hp
        {
            get;
            private set;
        }

        private float _damage;
        private bool _isCritHit; // 치명타인지 아닌지
        private float _criticalMultiplier; // 치명타일 경우 피해량 수정을 위해 곱해줄 수치
        private float _autoAttackInterval = 3f;
        private bool _isCriticalHit;

        [SerializeField] private float minDis = 100f; // 보스가 플레이어를 인식하는 최소 거리

        public float MinDis => minDis;
        
        [SerializeField] private float skillDis = 5f; // 보스가 스킬을 사용할 수 있는 거리
        private IAttack _attackImplementation;
        public float SkillDis => skillDis;
        
        private void Start()
        {
            _autoAttackInterval = 3f;
            Hp = 123f;
        }

        private void Update()
        {
            //TestUpdate();
        }

        private void TestUpdate()
        {
            Hp -= 10f * Time.deltaTime;
            if (Hp <= 0f)
            {
                Hp = 123f;
            }
        }

        public void OnDamage(float damage)
        {
            // 플레이어한테서 inflictedDamage 받아서 Hp 수정
            Hp -= damage;
            if (Hp <= 0f)
            {
                Hp = 0f;
            }
        }
    }
}
