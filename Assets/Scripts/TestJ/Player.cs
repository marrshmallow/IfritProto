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

        private void Start()
        {
            _boss = FindAnyObjectByType<Boss>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(1))
            {
                StartCoroutine(nameof(AutoAttack));
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                StopAllCoroutines();
            }
        }
        
        public IEnumerator AutoAttack()
        {
            _damageinflicted = Random.Range(20f, 30f);
            HitHard();
            if (_isCriticalHit)
            {
                _damageinflicted *= _criticalMultiplier;
                Debug.Log("플레이어의 치명타");
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