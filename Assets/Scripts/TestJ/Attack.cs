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
 * TODO: 어쩌면 이 오브젝트에 연결된 다른 스크립트에서 실행중인 코루틴은 꺼질 수도?
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
    /// 보스 스킬은 페이즈별로 패턴이 다르다
    /// >> 스킬 메소드 안에서 페이즈를 체크하고
    /// >> 페이즈별로 실행 구현... NO
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
        private float _transitionPadding;

        private BattleStateChecker.BattleState _battleState;
        
        private void Start()
        {
            EventManager.Instance.InitiateBattle += StartAttack;
        }
        
        private void Update()
        {
            // 타이머: 시간 누적
            _accTime += Time.deltaTime;
            //AutoAttack();
        }

        private void OnDisable()
        {
            EventManager.Instance.InitiateBattle -= StartAttack;
        }

        /**
         * 확률적으로 치명타를 발생시킴
         */
        private void HitHard()
        {
            _isCriticalHit = Random.Range(0f, 10f) < 3f;
            /*if (Random.Range(0f, 10f) < 3f) // 동일한 코드
            {
                _isCriticalHit = true;
            }
            else
            {
                _isCriticalHit = false;
            }*/
        }

        /*// 페이즈 1일 때 보스의 스킬 룰렛 안
        private void SelectSkill1()
        {
            if (Random.Range(0f, 10f) <= nFlame)
            {
                _useFlame = true;
                Debug.Log("보스가 화염을 뿜었다");
            }
        }

        // 페이즈 2일 때
        private void SelectSkill2()
        {
            if (Random.Range(0f, 10f) <= nFloor)
            {
                _useFloor = true;
                Debug.Log("보스가 장판을 폭발시켰다");
            }
            else
            {
                _useFlame = true;
                Debug.Log("보스가 화염을 뿜었다");
            }
        }

        private void SelectSkill3()
        {
            if (Random.Range(0f, 10f) <= nFloor)
            {
                _useFloor = true;
                Debug.Log("보스가 장판을 폭발시켰다");
            }
            else if (Random.Range(0f, 10f) > nFloor && Random.Range(0f, 10f) <= nPillar)
            {
                _usePillar = true;
                Debug.Log("보스가 불기둥을 폭발시켰다");
            }
            else
            {
                _useFlame = true;
                Debug.Log("보스가 화염을 뿜었다");
            }
        }*/
        
        /*private IEnumerator AutoAttack()
        {
            // TODO: 여기서 문제는 Start에서 코루틴을 실행시켜 줄 때 초기값이 !attack인 경우에도 최초 한 번은 실행된다는 점
            _damage = Random.Range(50, 60);
            // if (_isCriticalHit)
            //     _damage = _damage * _criticalMultiplier;
            Debug.Log("보스가 플레이어에게 " + _damage + "만큼 피해!");
            yield return new WaitForSeconds(3f);
            StartCoroutine(AutoAttack());
        }*/

        /*/*
         * 기본 목표: 보스가 스킬을 쓴다
         * 다음 목표: 여러가지 스킬 중에서 랜덤으로 선택해 쓴다
         * 궁극적인 목표: 순차적으로 스킬 풀에 스킬이 하나씩 추가됨
         #1#
        private void UseAttackSkill()
        {
        }*/

        private void StartAttack()
        {
            StartCoroutine(nameof(AAtest));
        }
        
        private IEnumerator AAtest()
        {
            float d = Random.Range(50f, 60f); // 공격량 결정
            HitHard(); // 치명타 룰렛 실행
            if (_isCriticalHit)
            {
                d *= criticalMultiplier;
                Debug.Log($"치명타!");
            }
            
            _damage = Mathf.FloorToInt(d); // 최종 공격량 확정
            Debug.Log($"보스가 플레이어에게 {_damage}만큼 피해!");
            yield return new WaitForSeconds(3f);
            StartCoroutine(nameof(AAtest));
        }
        
        private void AutoAttack()
        {
            if (_accTime >= aaInterval)
            {
                _accTime = 0f;
                float d = Random.Range(50f, 60f); // 공격량 결정
                HitHard(); // 치명타 룰렛 실행
                if (_isCriticalHit)
                {
                    d *= criticalMultiplier;
                    Debug.Log($"치명타!");
                }

                _damage = Mathf.FloorToInt(d); // 최종 공격량 확정
                Debug.Log($"보스가 플레이어에게 {_damage}만큼 피해!");
            }
        }
        
        private void SkillA()
        {
            _damage = Random.Range(100f, 140f);
            // if (_isCriticalHit)
            //     _damage = _damage * _criticalMultiplier;
            Debug.Log($"보스가 45도 부채꼴 화염 공격. 플레이어에게 {_damage} 만큼 피해!");
        }
        
        private void SkillB()
        {
            _damage = Random.Range(30f, 40f);
            // if (_isCriticalHit)
            //     _damage = _damage * _criticalMultiplier;
            Debug.Log($"보스가 밀어내기 공격. 플레이어에게 {_damage} 만큼 피해!");
        }
        
        private void SkillC()
        {
            _damage = Random.Range(50f, 60f);
            // if (_isCriticalHit)
            //     _damage = _damage * _criticalMultiplier;
            Debug.Log($"보스의 장판 폭발 공격. 플레이어에게 {_damage} 만큼 피해!");
        }
        
        private void SkillD()
        {
            _damage = Random.Range(80f, 90f);
            // if (_isCriticalHit)
            //     _damage = _damage * _criticalMultiplier;
            Debug.Log($"보스가 불기둥 장판 폭발 공격. 플레이어에게 {_damage} 만큼 피해!");
        }
        
        private void SkillE()
        {
            if (missionFailed)
            {
                // 플레이어의 HP 값만큼 공격이 들어감
                Debug.Log("미션 실패. 플레이어 사망. 게임을 처음부터 시작합니다.");
            }
            else
            {
                _damage = Random.Range(180f, 200f);
                //if (_isCriticalHit) _damage = _damage * _criticalMultiplier;
                Debug.Log($"미션 성공. 플레이어에게 {_damage} 만큼 피해! 다음 페이즈로 넘어갑니다.");
            }
        }

        private IEnumerator SwitchPattern()
        {
            // Check current phase from PhaseChecker
            switch (BattlePhaseChecker.CurrentPhase)
            {
                case BattlePhaseChecker.Phase.Default:
                    break;
                case BattlePhaseChecker.Phase.A:
                {
                    //StartCoroutine(nameof(PhaseA));
                }
                    break;
                case BattlePhaseChecker.Phase.B:
                {
                    //StartCoroutine(nameof(PhaseB));
                }
                    break;
                case BattlePhaseChecker.Phase.CheckPoint:
                {
                    //StartCoroutine(nameof(CheckPoint));
                }
                    break;
                case BattlePhaseChecker.Phase.Final:
                {
                    //StartCoroutine(nameof(FinalPhase));
                }
                    break;
            }
                
            //
            yield return null;
        }

        private IEnumerator PhaseA()
        {
            float d = Random.Range(50f, 60f);
            HitHard();
            if (_isCriticalHit)
            {
                d *= criticalMultiplier;
                Debug.Log("치명타!");
            }

            _damage = Mathf.FloorToInt(d); // 최종 공격량 확정
            Debug.Log($"보스가 플레이어에게 {_damage}만큼 피해!");
            yield return new WaitForSeconds(aaInterval);
        }
        
        private IEnumerator FinalPhase()
        {
            // 전멸기 연출하는 시퀀스
            // 조건 체크 후 분기
            yield return new WaitForSeconds(_transitionPadding);
        }
        
        /**
        * 다 치우고 자동 공격 1, 2, 3번 중 랜덤 발동하고 스킬 쓰기
        */
        private IEnumerator UseSkill(int phaseIndex)
        {
            switch (phaseIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            yield break;
        }
    }
}