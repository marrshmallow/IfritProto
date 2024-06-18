using UnityEngine;

public class Boss : MonoBehaviour
{
    private float _damage; //TODO: 랜덤 어떻게 하는지 잊어버렸다
    private bool _isCritHit; // 치명타인지 아닌지. TODO: 값의 범위를 두 가지 정해주고 랜덤하게 주사위 굴려서 나온 숫자가 특정 범위 안이면 치명타 판정을 받도록 하는 방식이 좋을지?
    private float _criticalMultiplier; // 치명타일 경우 피해량 수정을 위해 곱해줄 수치
}
