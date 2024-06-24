using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestJ
{
    public class Boss : MonoBehaviour, IAttack
    {
        public float Hp { get; set; }

        private float _damage;
        private bool _isCritHit; // 치명타인지 아닌지. TODO: 값의 범위를 두 가지 정해주고 랜덤하게 주사위 굴려서 나온 숫자가 특정 범위 안이면 치명타 판정을 받도록 하는 방식이 좋을지?
        private float _criticalMultiplier; // 치명타일 경우 피해량 수정을 위해 곱해줄 수치
        private float _autoAttackInterval = 3f;
        private bool _isCriticalHit;

        [SerializeField] private float minDis = 100f; // 보스가 플레이어를 인식하는 최소 거리

        public float MinDis { get; set; }
        
        [SerializeField] private float skillDis = 5f; // 보스가 스킬을 사용할 수 있는 거리
        public float SkillDis { get; set; }
        
        // Phase 변경 이벤트 수신

        private void Start()
        {
            _autoAttackInterval = 3f;
            Hp = 1000f;
        }
    
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                Hp = 100f;
                //OnSwitch2PhaseA?.Invoke();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                Hp = 75f;
                //OnSwitch2PhaseB?.Invoke();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                Hp = 45f;
                //OnSwitch2PhaseCheckPoint?.Invoke();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                Hp = 22f;
                //OnSwitch2PhaseFinal?.Invoke();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                Hp = 0f;
                //OnSwitch2PhaseDefault?.Invoke();
            }
        }
    
        /**
 * 메서드에 대한 설명을 작성할 수 있다
 * (메서드 이름 위에 마우스를 올려 놓으면 확인 가능)
 */
        void IAttack.ApplyCriticalHit()
        {
        
            if (_isCriticalHit)
                _damage = _damage * _criticalMultiplier;
        }
        /// <summary>
        /// 메서드 위에 작성하면 변수 등에 대한 안내서 기록 가능
        /// </summary>
        public void NormalAttack()
        {
            _damage = Random.Range(50, 60);
        
            if (_isCriticalHit)
                _damage = _damage * _criticalMultiplier;
        
            Debug.Log("보스가 플레이어에게 " + _damage + "만큼 피해!");
        }

        void IAttack.SkillAttack()
        {
            throw new System.NotImplementedException();
            //SetState(BossState.NormalAttack);
        }
        
        public BossState bossState;
    }
}