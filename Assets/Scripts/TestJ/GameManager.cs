using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 게임의 흐름을 관리하는 스크립트.
    /// 전투 시작 여부 확인
    /// - 몬스터 생존 여부 확인
    /// - 몬스터 HP 추적
    /// - 플레이어 생존 여부 확인
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public bool startBattle;
        public bool bossDead;
        public bool playerDead;

        [SerializeField] private Boss boss;
        [SerializeField] private Player player;

        private void Update()
        {
        }
    }
}
