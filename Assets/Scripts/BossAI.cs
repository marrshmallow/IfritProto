using UnityEngine;
// TODO: 치명타 여부를 판정하는데 홀짝 구분으로 하면 확률이 반반이라 안되겠지...?
// 나머지 값이 어느 범위 안에 들어오냐 그걸로 구분할까?

/// <summary>
/// 보스가 주변에 있는 플레이어를 인식하고 그에 맞춰 행동하게 하는 코드
/// 1. 보스가 현재 위치한 곳을 중심으로 특정 반경 안에 Player 태그를 가진 개체가 있는지 확인
/// 2. 플레이어를 인식했다면 플레이어의 Transform 값을 호출
/// 3. 플레이어를 향해 방향 전환 (Rotate)
/// 4. 
/// </summary>

public class BossAI : MonoBehaviour
{
}
