using UnityEngine;
using Random = UnityEngine.Random;

namespace TestJ
{
    public class Boss : MonoBehaviour
    {
        private float _hp;

        public float Hp
        {
            get;
            private set;
        }

        private float _damage;
        private bool _isCritHit; // 치명타인지 아닌지. TODO: 값의 범위를 두 가지 정해주고 랜덤하게 주사위 굴려서 나온 숫자가 특정 범위 안이면 치명타 판정을 받도록 하는 방식이 좋을지?
        private float _criticalMultiplier; // 치명타일 경우 피해량 수정을 위해 곱해줄 수치
        private float _autoAttackInterval = 3f;
        private bool _isCriticalHit;

        [SerializeField] private float minDis = 100f; // 보스가 플레이어를 인식하는 최소 거리

        public float MinDis => minDis;
        
        [SerializeField] private float skillDis = 5f; // 보스가 스킬을 사용할 수 있는 거리
        private IAttack _attackImplementation;
        public float SkillDis => skillDis;

        // Phase 변경 이벤트 수신

        private void Start()
        {
            _autoAttackInterval = 3f;
            Hp = 100f;
        }

        private void Update()
        {
            TestUpdate();
        }

        private void TestUpdate()
        {
            Hp -= 10f * Time.deltaTime;
            if (Hp <= 0f)
            {
                Hp = 100f;
            }
        }
    }
}