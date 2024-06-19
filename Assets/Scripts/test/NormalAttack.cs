using System.Collections;
using UnityEngine;

namespace testJ
{
    public class NormalAttack : MonoBehaviour
    {
        private float _damage;
        private bool _isCriticalHit;
        private float _criticalMultiplier;

        private void Start()
        {
            StartCoroutine(AutoAttack());
        }

        private IEnumerator AutoAttack()
        {
            _damage = Random.Range(50, 60);
        
            if (_isCriticalHit)
                _damage = _damage * _criticalMultiplier;
        
            Debug.Log("보스가 플레이어에게 " + _damage + "만큼 피해!");

            yield return new WaitForSeconds(3);
        }
    }
}
