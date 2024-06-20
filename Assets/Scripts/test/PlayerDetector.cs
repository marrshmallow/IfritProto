using UnityEngine;

namespace test
{
    public class PlayerDetector : MonoBehaviour
    {
        private bool _provoked; // 보스가 플레이어를 인식했는가
                                // 한 번 참이 되면 플레이어가 죽거나 보스가 죽을 때까지 변하지 않는다

        public Player player;

        /*
         * 보스가 플레이어를 인식하게 하는 것
         * 플레이어 태그를 가진 오브젝트가 들어오면
         * 전투 시작 = 참
         *
         * OnTriggerEnter를 사용하기 위해서는 충돌할 두 개체 중 어느 한쪽에 Rigidbody가 있어야 한다.
         * 여기서는 플레이어에게 Rigidbody를 심어놓기로 한다. (보스의 밀어내기 공격에도 필요...?)
         */
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _provoked = true;
                Debug.Log("플레이어 감지. 상태: 전투");
                player = other.GetComponent<Player>(); // TODO: 주의: 컬라이더와 같은 오브젝트에 해당 스크립트가 연결되어 있지 않으면 불러올 수 없다
                Debug.Log("해당 플레이어의 Player.cs를 연결");
                // 역할이 끝났으면 바로 스피어 컬라이더를 꺼준다
                SphereCollider s = GetComponent<SphereCollider>();
                s.enabled = false;
            }
        }
    }
}