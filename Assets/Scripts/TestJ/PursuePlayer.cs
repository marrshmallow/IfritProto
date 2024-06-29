using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 플레이어의 위치를 파악해서 (transform.position)
    /// 방향을 돌린 후
    /// 그 위치를 향해 전진하는 스크립트
    /// </summary>
    public class PursuePlayer : MonoBehaviour
    {
        private PlayerDetector _sensor; // 플레이어 인식하는 코드가 인식한 플레이어의 transform.position 정보를 받아오려고
        [SerializeField] private float moveSpeed = 40f; // 보스의 이동 속도
        [SerializeField] private float maxDistance = 3f; // 보스가 공격할 수 있는 거리
        public float distance;

        [SerializeField] private GameObject bossRotatePivot; // 프리팹 속 BossTransform을 게임오브젝트로 설정하기
        private Vector3 playerPos; // 플레이어 위치

        private void Start()
        {
            _sensor = transform.GetChild(1).GetComponent<PlayerDetector>();
        }

        private void FixedUpdate()
        {
            if (BattleStateChecker.CurrentState != BattleStateChecker.BattleState.Battle) return;
            Move();
        }

        private void CheckDistance()
        {
            if (!_sensor.player) return;
            playerPos = _sensor.player.transform.position;
            float d = Vector3.Distance(playerPos, transform.position);
            distance = Mathf.Sqrt(d);
        }
        
        private void Move()
        {
            CheckDistance();
            // 플레이어가 자동 공격 범위 밖을 벗어났다면 추격 시작
            if (!(distance > maxDistance)) return;
            // 위치 설정
            Vector3 target = playerPos;
            Vector3 currentPos = transform.position;
            Vector3 targetPos = Vector3.MoveTowards(currentPos, target, moveSpeed * Time.fixedDeltaTime);
            transform.position = targetPos;
                
            Vector3 faceDirection = (target - transform.position).normalized;
            if (faceDirection == Vector3.zero) return;
            Quaternion lookRotation = Quaternion.LookRotation(faceDirection);
            if (Quaternion.Angle(transform.rotation, lookRotation) > 1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * moveSpeed * 0.01f);
            }
        }
    }
}