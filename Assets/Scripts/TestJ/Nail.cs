using System;
using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 이 적 물체는 플레이어를 공격하지 않음
    /// 관통 가능
    /// TODO: 플레이어가 스스로의 자동 공격 범위 안에 들어와야  
    /// </summary>
    public class Nail : MonoBehaviour
    {
        [SerializeField] private float hp; // 못 체력
    }
}
