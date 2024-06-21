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
        [SerializeField] private Object childPrefab;
        [SerializeField] private Vector3 spawnPos = new Vector3(0f, 0f, 138f);
        [SerializeField] private Vector3 childPos = new Vector3(0f, 0f, 0f);

        private void Start()
        {
            Spawn();
        }
        
        /**
         * 사용할 프리팹: Boss 1
         */
        private void Spawn()
        {
            Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        }

        /**
         * 사용할 프리팹: Nail
         */
        private void SpawnChild()
        {
            Instantiate(childPrefab, childPos, Quaternion.identity);
        }
    }
}
