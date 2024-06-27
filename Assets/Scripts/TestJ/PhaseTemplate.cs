using System.Collections;
using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 페이즈 기본 템플리시
    /// 1. 해당 페이즈에 새로 추가되는 스킬을 가장 먼저 실행
    /// 2. 자동 공격 실행
    /// 3. 주기적으로 스킬이 실행됨
    /// - 스킬은 실행될 때 가장 먼저 자동 공격을 멈춘다
    /// - 자동 공격은 업데이트에서 돌아가므로 lock용 bool을 만들면...?
    /// </summary>
    public class PhaseTemplate : MonoBehaviour
    {
        private bool _useSkill;
    }
}