using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * 코루틴을 사용하는 세 가지 방법
 * 1. 이름으로 사용: StartCoroutine("SkillB");
 * 단점: 이름이 변경되면 실행시키지 못한다.
 * 2. 참조로 사용 (보통): StartCoroutine(SkillB());
 * 단점: StopCoroutine(SkillB()); 를 사용해 직관적으로 끌 수가 없다.
 * 3. 참조를 해서 스트링으로 변환해 이름으로 끄기: StartCoroutine(nameof(SkillB));
 */
            
/*
 * 코루틴 종료
 *
 * StopCoroutine(SkillB());라고만 적었을 때 무슨 일이 일어나는가
 *
 * StartCoroutine으로 실행한 SkillB()를 꺼주는 게 아니라 SkillB();로 실행된 것을 꺼주려고 한다.
 * SkillB();는 이론상으로는 가능하지만 실제로 실행할 수 없으므로
 * StopCoroutine(SkillB());를 한다고 해서 SkillB();가 멈추는 건 아님
 *
 * 제대로 끄는 3가지 방법
 * 1. 이름으로 끄기: StopCoroutine(nameof(SkillB));
 * 2. 통째로 넣어서 끄기: var c = StartCoroutine(SkillB()); StopCoroutine(c);
 * 3. 다른 모든 코루틴도 중지해야 하는 상황이라면 StopAllCoroutines();
 *
 * 코루틴은 게임오브젝트 단위로 관리되므로
 * 여기서 "전부 껐다" 하더라도 다른 오브젝트에서 실행중인 코루틴이 꺼지지는 않는다.
 */ 

namespace TestJ
{
    /// <summary>
    /// 활성화되면 3초에 한 번씩 일반 공격
    /// 
    /// 보스 스킬은 정해진 간격으로 발동
    /// 발동되면 모든 실행중이던 동작 정지
    /// 스킬 발동 끝에 다시 AA 반복 시작
    ///
    /// 페이즈 기본 템플릿
    /// >> 특징이라고 할 수 있는 공격을 가장 먼저 발동
    /// >> 그 다음에 자동 공격과 스킬 공격을 반복
    /// </summary>
    public class Attack : MonoBehaviour
    {
        private float _damage; // 한 번의 공격량
        private bool _isCriticalHit;
        [SerializeField] private float criticalMultiplier = 1.2f;
        [SerializeField] private bool attack;
        [SerializeField] private bool missionFailed; // 사실 이거는 GameManager에서 관리해야 하는 것
        
        private float _accTime; // 누적 시간
        [SerializeField] private float aaInterval = 3f; // 자동공격 간격
        [SerializeField] private float floorCastTime;
        [SerializeField] private float pillarCastTime;
        [SerializeField] private float burnCastTime;
        [SerializeField] private float criticalRate = 2f;

        private void Start()
        {
            EventManager.Instance.InitiateBattle += StartAttack;
            EventManager.Instance.GameEnd += StopAllAttacks;
        }
        
        private void Update()
        {
            _accTime += Time.deltaTime;
        }

        /**
         * 확률적으로 치명타를 발생시킴
         */
        private void HitHard()
        {
            _isCriticalHit = Random.Range(0f, 10f) < criticalRate;
        }

        private void StartAttack()
        {
            StartCoroutine(nameof(AutoAttack));
        }
        
        private IEnumerator AutoAttack()
        {
            while (true)
            {
                float d = Random.Range(50f, 60f); // 공격량 결정
                HitHard(); // 치명타 룰렛 실행
                if (_isCriticalHit)
                {
                    d *= criticalMultiplier;
                }
            
                _damage = Mathf.FloorToInt(d); // 최종 공격량 확정
                Debug.Log($"보스가 플레이어에게 {_damage}만큼 피해!");
                yield return new WaitForSeconds(3f);
            }
        }
        
        private IEnumerator Incinerate()
        {
            StopCoroutine(nameof(AutoAttack));
            float d = Random.Range(100f, 140f);
            HitHard(); // 치명타 룰렛 실행
            if (_isCriticalHit)
            {
                d *= criticalMultiplier;
            }
            
            _damage = Mathf.FloorToInt(d); // 최종 공격량 확정
            Debug.Log($"보스가 45도 부채꼴 화염 공격. 플레이어에게 {_damage} 만큼 피해!");
            
            yield return new WaitForSeconds(aaInterval);
            StartCoroutine(nameof(AutoAttack)); // Start, but after the animation is over and boss is close enough
        }

        private IEnumerator VulcanBurst()
        {
            StopCoroutine(nameof(AutoAttack));
            float d = Random.Range(30f, 40f);
            HitHard(); // 치명타 룰렛 실행
            if (_isCriticalHit)
            {
                d *= criticalMultiplier;
            }
            
            _damage = Mathf.FloorToInt(d); // 최종 공격량 확정
            Debug.Log($"보스가 밀어내기 공격. 플레이어에게 {_damage} 만큼 피해!");
            
            yield return new WaitForSeconds(aaInterval);
            StartCoroutine(nameof(AutoAttack)); // Start, but after the animation is over and boss is close enough
        }

        private IEnumerator Eruption()
        {
            StopCoroutine(nameof(AutoAttack));
            
            float d = Random.Range(50f, 60f);
            HitHard(); // 치명타 룰렛 실행
            if (_isCriticalHit)
            {
                d *= criticalMultiplier;
            }
            
            _damage = Mathf.FloorToInt(d); // 최종 공격량 확정
            yield return new WaitForSeconds(floorCastTime);
            Debug.Log($"보스의 장판 폭발 공격. 플레이어에게 {_damage} 만큼 피해!");
            
            StartCoroutine(nameof(AutoAttack));
        }

        private IEnumerator RadiantPlume()
        {
            StopCoroutine(nameof(AutoAttack));

            float d = Random.Range(80f, 90f);
            HitHard(); // 치명타 룰렛 실행
            if (_isCriticalHit)
            {
                d *= criticalMultiplier;
            }
            
            _damage = Mathf.FloorToInt(d); // 최종 공격량 확정
            Debug.Log($"보스가 불기둥 장판 폭발 공격. 플레이어에게 {_damage} 만큼 피해!");
            StartCoroutine(nameof(AutoAttack));
            yield break;
        }

        private void Hellfire()
        {
            StopAllCoroutines();

            // Nail, upon destruction, sends CheckPointPassed event
            // GameManager is subscribed to the event and will trigger its method to set static bool "CheckPointPassed"
            // If CheckPointPassed is true, player will take some damage and Final phase will start
            // If CheckPointPassed is false, player will die no matter what and stage will be reset
            if (missionFailed)
            {
                // 플레이어의 HP 값만큼 공격이 들어감
                Debug.Log("미션 실패. 플레이어 사망. 게임을 처음부터 시작합니다.");
            }
            else
            {
                float d = Random.Range(180f, 200f);
                //if (_isCriticalHit) _damage = _damage * _criticalMultiplier;
                Debug.Log($"미션 성공. 플레이어에게 {_damage} 만큼 피해! 다음 페이즈로 넘어갑니다.");
            }
        }
        
        private void RunSkillRoulette()
        {
            // A: Flame
            // B: Knock-back
            // C: Floor
            // D: Pillar
            if (_accTime < 10f)
            {
                return; // 안좋음. 엔터 (놓치고 지나갈 수 있어서)
            }
            _accTime = 0f;
            
            switch (BattlePhaseChecker.CurrentPhase)
            {
                case BattlePhaseChecker.Phase.A:
                {
                    // A, B
                }
                    break;
                case BattlePhaseChecker.Phase.B:
                {
                    // A, B, C
                }
                    break;
                case BattlePhaseChecker.Phase.CheckPoint:
                {
                    // A, B, C
                }
                    break;
                case BattlePhaseChecker.Phase.Transition:
                {
                    // Nothing
                    // 전멸기
                }
                    break;
                case BattlePhaseChecker.Phase.Final:
                {
                    // A, B, C, D
                }
                    break;
            }
        }

        private void StopAllAttacks()
        {
            StopAllCoroutines();
        }
        
        private void OnDisable()
        {
            EventManager.Instance.InitiateBattle -= StartAttack;
            EventManager.Instance.GameEnd -= StopAllAttacks;
        }
    }
}