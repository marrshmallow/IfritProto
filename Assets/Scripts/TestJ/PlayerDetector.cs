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
        public GameObject player;
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
            EventManager.EventManagerInstance.InitiateBattle += DetectPlayer;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3)
            {
                player = other.gameObject;
                DetectPlayer();
            }
        }

        private void DetectPlayer()
        {
            // 플레이어를 인식...
            //_provoked = true;
            Debug.Log("플레이어 감지. 상태: 전투");
            //player = other.GetComponent<Player>(); // 주의: 컬라이더와 같은 오브젝트에 해당 스크립트가 연결되어 있지 않으면 불러올 수 없다
            /*
             * 충돌체크는 물리엔진이 하고, 그 정보를 받아서 처리하는 것은 Rigidbody
             * 자식 오브젝트에 컬라이더가 있는데 같은 오브젝트에 Rigidbody가 없다면, Rigidbody를 찾아서 점점 위로 올라가게 된다.
             * 올라가는 도중에 찾는 가장 첫 번째 Rigidbody에 모든 충돌 정보를 전달
             */
        }
    }
}