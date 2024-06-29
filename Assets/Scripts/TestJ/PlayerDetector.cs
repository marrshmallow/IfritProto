using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 플레이어 탐지기
    /// 플레이어를 인식하면 BattleState를 Battle로 변경해 준다
    /// 플레이어가 죽거나 보스가 죽을 때까지 변하지 않는다
    /// </summary>
    public class PlayerDetector : MonoBehaviour
    {
        private Boss _boss;
        public GameObject player;
        public static EEnemyState BossState;
        private EEnemyState _newState;

        /*
         * 보스가 플레이어를 인식하게 하는 것
         * 플레이어 태그를 가진 오브젝트가 들어오면
         * 전투 시작 = 참
         *
         * OnTriggerEnter를 사용하기 위해서는 충돌할 두 개체 중 어느 한쪽에 Rigidbody가 있어야 한다.
         * 여기서는 플레이어에게 Rigidbody를 심어놓기로 한다. (보스의 밀어내기 공격에도 필요...?)
         */

        private void Start()
        {
            _boss = transform.parent.GetComponent<Boss>();
            EventManager.Instance.InitiateBattle += InitiateBattle;
            EventManager.Instance.BattleStateChange += UpdateState;
        }

        private void Update()
        {
            ChangeState();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != 3) return;
            player = other.gameObject;
            EventManager.Instance.OnBattleInitiated();
        }

        private void ChangeState()
        {
            switch (BossState)
            {
                case EEnemyState.Evoked when _boss.Hp > 0f:
                    return;
                case EEnemyState.Evoked when _boss.Hp <= 0f:
                {
                    _newState = EEnemyState.Dead;
                    Debug.Log("?????");
                }
                    break;
            }

            /*switch (BossState)
            {
                case EEnemyState.Idle:
                {
                    _newState = EEnemyState.Evoked;
                }
                    break;
                case EEnemyState.Evoked:
                {
                    if (_boss.Hp <= 0f)
                    {
                        _newState = EEnemyState.Dead;
                        Debug.Log("???");
                    }

                    // 보스 아니고 일반 적 관련 처리인데 왜 여기다 적니이이이이
                    /*if (Hp > 0f) // && player OnTriggerExit
                    {
                        newState = EEnemyState.Idle;
                    }
                    else if (Hp <= 0f) // && player OnTriggerExit didn't happen
                    {
                        newState = EEnemyState.Dead;
                    }#1#
                }
                    break;
            }*/

            if (BossState == _newState) return;
            EventManager.Instance.OnBattleStateChanged();
        }

        private void UpdateState()
        {
            BossState = _newState;
        }

        private void InitiateBattle()
        {
            BossState = EEnemyState.Evoked;
            BattleStateChecker.CurrentState = BattleStateChecker.BattleState.Battle;
        }
        
        private void OnDisable()
        {
            EventManager.Instance.InitiateBattle -= InitiateBattle;
            EventManager.Instance.BattleStateChange -= UpdateState;
        }
    }
}