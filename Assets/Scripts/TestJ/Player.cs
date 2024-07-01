using System.Collections;
using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 테스트용 플레이어 스크립트   
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private float hp = 500f;
        private float _damageinflicted;
        public float damageInflicted; // 보스가 최종적으로 건네 받을 피해량
        private bool _isCriticalHit;
        private float _criticalMultiplier = 2.14f;

        private Boss _boss;
        private InfernalNail infernalNail;

        private void Start()
        {
            _boss = FindAnyObjectByType<Boss>();
            infernalNail = FindAnyObjectByType<InfernalNail>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(1)) // 마우스 우클릭
            {
                StopCoroutine(nameof(AutoAttackNail));
                StartCoroutine(nameof(AutoAttack));
            }
            else if (Input.GetMouseButtonUp(0)) // 마우스 클릭
            {
                StopCoroutine(nameof(AutoAttack));
                StartCoroutine(nameof(AutoAttackNail));
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator AutoAttackNail()
        {
            infernalNail = FindAnyObjectByType<InfernalNail>();
            if (infernalNail == null) yield break;
            
            _damageinflicted = Random.Range(20f, 30f);
            HitHard();
            if (_isCriticalHit)
            {
                _damageinflicted *= _criticalMultiplier;
            }

            damageInflicted = Mathf.FloorToInt(_damageinflicted);
            infernalNail.OnDamage(damageInflicted);
            yield return new WaitForSeconds(2f);
            StartCoroutine(nameof(AutoAttackNail));
        }

        public IEnumerator AutoAttack()
        {
            _damageinflicted = Random.Range(20f, 30f);
            HitHard();
            if (_isCriticalHit)
            {
                _damageinflicted *= _criticalMultiplier;
            }

            damageInflicted = Mathf.FloorToInt(_damageinflicted);
            _boss.OnDamage(damageInflicted);
            yield return new WaitForSeconds(2f);
            StartCoroutine(nameof(AutoAttack));
        }

        private void HitHard()
        {
            _isCriticalHit = Random.Range(0f, 10f) < 3f;
        }
    }
}