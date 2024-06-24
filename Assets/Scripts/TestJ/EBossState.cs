public enum BossState
{
    Idle,
    Evoked,
    FindPlayer, // 플레이어 위치 감지 후 제자리에서 방향 전환
    PursuePlayer,
    NormalAttack, // 일반 공격 (자동공격)
    SkillAttack, // 특수 기술
    FinalAttack, // 전멸기
    Dead
}