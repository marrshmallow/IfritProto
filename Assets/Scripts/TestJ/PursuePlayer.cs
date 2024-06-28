using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 플레이어의 위치를 파악해서 (transform.position)
    /// 방향을 돌린 후
    /// 그 위치를 향해 전진하는 스크립트
    ///
    /// 고정된 좌표를 향해 이동하게 하려면
    /// 1. 목표 좌표를 정한다
    /// 2. 현재 좌표를 뽑아놓는다
    /// 3. 현재 좌표에서 목표 좌표가 어느 방향에 위치해 있는지 뽑는다
    /// 4. _t.position += moveSpeed * Time.deltaTime * _dir; ?????
    /// </summary>
    public class PursuePlayer : MonoBehaviour
    {
        private PlayerDetector _sensor; // 플레이어 인식하는 코드가 인식한 플레이어의 transform.position 정보를 받아오려고
        [SerializeField] private float moveSpeed = 1f; // 보스의 이동 속도
        [SerializeField] private float maxDistance = 3f; // 보스가 공격할 수 있는 거리
        public float result;

        [SerializeField] private GameObject bossRotatePivot; // 프리팹 속 BossTransform을 게임오브젝트로 설정하기
        private Vector3 playerPos; // 플레이어 위치

        private void Start()
        {
            _sensor = transform.GetChild(1).GetComponent<PlayerDetector>();
        }

        private void FixedUpdate()
        {
            if (BattleStateChecker.CurrentState != BattleStateChecker.BattleState.Battle) return;
            CheckDistance();
            Move();
        }

        private void CheckDistance()
        {
            if (!_sensor.player) return;
            playerPos = _sensor.player.transform.position;
            float d = Vector3.Distance(playerPos, transform.position);
            result = Mathf.Sqrt(d);
        }
        
        private void Move()
        {
            // 플레이어가 자동 공격 범위 밖을 벗어났다면 추격 시작
            //if (result > maxDistance)
            //{
                // 위치 설정
                Vector3 target = playerPos;
                bossRotatePivot.transform.LookAt(target, Vector3.up); // 뭔가 모양이 이상하다면 기준이 되는 축 확인해보기

                // TODO: 왜 처음 한 번만 따라오고 계속 안 따라오는 것인가
                Vector3 targetPos = new Vector3(transform.position.x - playerPos.x, 0f, transform.position.z - playerPos.z);
                targetPos.Normalize();
                // 여기서 그냥 _t.position += moveSpeed * Time.deltaTime * dir를 해버리면
                // 보스가 마지막에 비정상적으로 왔다갔다 하는 문제가 발생한다
                // 수치가 딱 떨어지지 않는 경우: 4.5 만큼의 거리가 남았는데 다음 프레임에 움직일 거리는 5일 경우 (v.v)
                // 목표로 하는 위치에 마지막의 마지막에 절대로 도달하지 못하기 때문에 발생
                // 해결하려면 남은 거리를 계산한 다음에 내가 움직일 거리보다 작은 경우에는 그만큼 줄여주고
                // 멀 경우에는 그만큼 늘려준다 TODO: Vector3.MoveTowards 공부하기
                // maxDistanceDelta: 다음 프레임에 움직일 거리. 즉 moveSpeed * Time.deltaTime
                Vector3 targetDir = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime );
                // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.MoveTowards.html
                //Debug.Log($"Target at: {playerPos} / Set target destination to: {targetPos} / TargetDir: {targetDir}");
                transform.position -= moveSpeed * Time.deltaTime * targetDir; // MoveTowards()를 사용해서 나온 값의 용도를 착각했기 때문에 문제 발생
            //}
        }
    }
}