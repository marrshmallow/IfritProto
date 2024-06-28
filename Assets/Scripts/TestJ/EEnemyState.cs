namespace TestJ
{
    public enum EEnemyState
    {
        Idle = 0,
        Evoked = 100,
        //FindTarget = 200, // 대상 위치 감지 후 제자리에서 rotate
        //PursueTarget = 300,
        
        // FindTarget과 PursueTarget은 Attack일 때 실행된다
        //Attack = 200,
        Dead = 300 // Emits event
    }
}
