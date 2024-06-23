using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 플레이어 생성기 (테스트용)
    /// </summary>
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Vector3 spawnPos = new Vector3(0f, 0f, -132f);

        private void Start()
        {
            Spawn();
        }
        
        /**
         * 사용할 프리팹: Player(Dummy)
         */
        private void Spawn()
        {
            GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        }
    }
}
