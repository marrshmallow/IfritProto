using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestJ
{
    /// <summary>
    /// 테스트용 플레이어 스크립트   
    /// </summary>
    public class Player : MonoBehaviour
    {
        private enum PlayerState
        {
            Idle,
            Battle,
            Dead
        }
        
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
            EventManager.Instance.GameEnd += StopAllAttacks;
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

            if (hp <= 0f)
            {
                // 보스한테 한 것과 같이 해야
            }
        }

        private IEnumerator AutoAttackNail()
        {
            while (true)
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
                Debug.Log($"Player attacks the nail");
                yield return new WaitForSeconds(2f);
            }
        }

        public IEnumerator AutoAttack()
        {
            while (true)
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
            }
        }

        private void HitHard()
        {
            _isCriticalHit = Random.Range(0f, 10f) < 3f;
        }

        private void StopAllAttacks()
        {
            StopAllCoroutines();
        }

        private void OnDisable()
        {
            EventManager.Instance.GameEnd -= StopAllAttacks;
        }
    }
}