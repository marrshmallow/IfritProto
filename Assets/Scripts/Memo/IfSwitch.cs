using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Memo
{
    public class IfSwitch : MonoBehaviour
    {
        public int score = 100000;
        public char grade;

        private void Start()
        {
            score = 100000;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                score = 99;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                score = 87;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                score = 71;
            }            
            else if (Input.GetKeyDown(KeyCode.F))
            {
                score = 61;
            }            
            else if (Input.GetKeyDown(KeyCode.G))
            {
                score = 59;
            }

            if (score > 100) // 제한자를 먼저 설정해 주면 훨씬 더 좋다
            {
                Debug.LogError("Invalid input");
                return;
            }

            //if (num >= 90 && num <= 100)
            if (score >= 90 && score < 100) // 논리적으로 이 쪽이 더 이해하기 편하다
            {
                grade = 'A';
            }
            //else if (score >= 80 && score < 90) // 앞에서 이미 조건문을 만들어 놨기 때문에 중복해서 물어보지 않아도 된다
            else if (score >= 80)
            {
                grade = 'B';
            }
            else if (score >= 70)
            {
                grade = 'C';
            }
            else if (score >= 60)
            {
                grade = 'D';
            }
            else if (score >= 0)
            {
                grade = 'F';
            }
            
            if (!Input.anyKeyDown)
            {
                return;
            }
            Debug.Log($"당신의 점수는: {score} / 이번 학기 당신의 성적은: {grade}");
        }
    }
}
