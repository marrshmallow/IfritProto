using System;
using UnityEngine;

namespace TestJ
{
    /// <summary>
    /// 테스트용
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 50f;
        private Transform _t;

        private void Start()
        {
            EventManager.Instance.GameEnd += StopMovement;
            _t = GetComponent<Transform>();
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D))
            {
                Move();
            }
        }

        private void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 dir = new Vector3(h, 0f, v);
            dir.Normalize();
            _t.transform.position += moveSpeed * Time.deltaTime * dir;
        }

        private void StopMovement()
        {
            enabled = false;
        }

        private void OnDisable()
        {
            EventManager.Instance.GameEnd -= StopMovement;
        }
    }
}