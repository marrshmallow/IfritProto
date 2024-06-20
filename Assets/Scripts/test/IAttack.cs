using System.Collections;
using UnityEngine;
/// <summary>
/// 혹시 몰라서 넣어두는 공격 인터페이스
/// 플레이어 쪽에서 사용하지 않더라도 사용하게 되더라도 어느쪽이든 문제 없게 디자인하기
/// </summary>

public interface IAttack
{
    public void ApplyCriticalHit(); // 치명타일 경우 공격량을 수정해주는 기능
    public void NormalAttack(); // 일반 공격 (근접하기만 하면 자동으로 행해지는 공격)
    public void SkillAttack(); // 스킬 공격 (플레이어의 경우 키보드를 눌러서 실행하는 공격, 보스도 같은 맥락)
}
