using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 보스 생성기. 씬이 실행되면 바로 보스가 생성된다.
    /// 중간에 보스가 호출하면 Nail도 생성해야 한다
    /// </summary>
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject bossPrefab;
        [SerializeField] private GameObject childPrefab;
        private GameObject spawnedChild;
        [SerializeField] private Vector3 spawnPos = new Vector3(0f, 0f, 138f);
        [SerializeField] private Vector3 childPos = new Vector3(0f, 0f, 0f);
        
        private void Awake()
        {
            Spawn();
        }

        private void Start()
        {
            EventManager.Instance.PhaseSwitch += SpawnChild;
            EventManager.Instance.PhaseSwitch += DestroyChild;
        }

        private void OnDisable()
        {
            EventManager.Instance.PhaseSwitch -= SpawnChild;
            EventManager.Instance.PhaseSwitch -= DestroyChild;
        }

        /**
         * 사용할 프리팹: Boss 2
         * Boss 1은 Capsule Collider가 있고 2에는 없다.
         * 1에 Capsule Collider가 있는 이유는 나중에 플레이어가 공격할 때 타격 판정을 내리기 위해서... 인데
         * Circle Collider가 같은 곳에서 센서 역할을 할 때, Capsule Collider에 대한 충돌 정보도 같은 Rigidbody로 전송되기 때문에 2중으로 인식되는 문제 발생
         *
         * 지금 떠오르는 해결 방안으로서는
         * Rigidbody를 새로 만들어서 그것과 캡슐 컬라이더를 다른 곳으로 옮기는 것
         * >> TODO: How to have multiple colliders check for different OnCollider/Trigger events properly
         */
        private void Spawn()
        {
            // 보스 프리팹을 씬의 현재 위치에 복제
            GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
            // 6시 방향을 바라보게 회전
            boss.transform.Rotate(0f, -180f, 0f); // TODO: 이 상태에서 W 눌러서 화살표 방향 보면 진짜진짜진짜 신경쓰이는데 괜찮은걸까
        }

        /**
         * 사용할 프리팹: Nail
         */
        private void SpawnChild()
        {
            if (BattlePhaseChecker.CurrentPhase != BattlePhaseChecker.Phase.B) return;
            spawnedChild = Instantiate(childPrefab, childPos, Quaternion.identity);
        }

        private void DestroyChild()
        {
            if (BattlePhaseChecker.CurrentPhase != BattlePhaseChecker.Phase.CheckPoint) return;
            if (GameManager.CheckpointPassed)
            {
                Destroy(spawnedChild);
            }
            else
            {
                spawnedChild.SetActive(false);
            }
        }
    }
}