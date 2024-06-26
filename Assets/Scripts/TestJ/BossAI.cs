using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 보스가 주변에 있는 플레이어를 인식하고 그에 맞춰 행동하게 하는 코드
    /// 감지, 확인, 이동의 세 가지 기능
    /// 1. 보스가 현재 위치한 곳을 중심으로 특정 반경 안에 Player 태그를 가진 개체가 있는지 확인
    /// 2. 플레이어를 인식했다면 플레이어의 Transform 값을 호출
    /// 3. 플레이어를 향해 방향 전환 (Rotate)
    /// 4. 플레이어를 향해 직선으로 이동
    ///
    /// 보스에게 필요한 것:
    /// Sphere Collider
    /// 보스 자체에 물리 연산은 필요 없음 (플레이어가 보스를 관통함)
    /// 넉백 공격은 플레이어의 회전과 움직임을 제한한 상태에서 밀어내는 걸로
    ///
    /// Boss 프리팹 안에 Sensor라는 이름g으로 스피어 컬라이더 생성: 현재 지름 3
    /// </summary>
    public class BossAI : MonoBehaviour
    {
        public Transform playerTransform; // 플레이어의 트랜스폼 TODO: 플레이어 스크립트에서 직접 참조할지 나중에 생각하기
    
        // 보스의 일반 공격 범위
        private Vector3 _attackDist;
        // 보스의 스킬 공격 범위 TODO: 나중에
        private Vector3 _skillAttackDist;


        private void Start()
        {
        }

        private void Update()
        {

        }
    

    
        // 플레이어가 어디에 있는지 확인
        private void GetPlayerPosition()
        {
        }
    
        // 플레이어가 있는 Transform.position을 직선을 바라보도록 자신의 Transform.rotation을 수정
        private void LookAtPlayer()
        {
        }

        // 플레이어가 있는 Transform.position으로 직선 이동
        // 멈추는 시점: 일반 공격의 범위 안 (특수 공격부터 나갈 때에는 특수 공격의 사정 거리)
        private void PursuePlayer()
        {
        }
    }
}