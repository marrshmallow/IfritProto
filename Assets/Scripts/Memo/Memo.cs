using UnityEngine;

namespace Memo
{
    public class Memo : MonoBehaviour
    {
        /*
         * Delegate: container for functions
         * Delegate script:
         * 1. Create a template
         * 2. Create delegate with a delegate keyword
         * 3. DelegateSignature (Return type, name, param)
         * 4. Declare member var (of the delegate we just created)
         */

        delegate void ExampleDelegate(int num); // returns void, int parameter
        ExampleDelegate _newDelegate; // Type is ExampleDelegate
        
        delegate void MultiDelegate(); // returns void, no parameter required
        MultiDelegate _newMultiDelegate;

        // Calling two different methods using the same delegate
        private void Start()
        {	
            _newMultiDelegate += PowerUp; // Subscribe PowerUp method to newMultiDelegate
            // PowerUp will be listening for newMultiDelegate event
            // PowerUp will be executed when the event is raised
            _newMultiDelegate += TurnRed; // Subscribe TurnRed method to newMultiDelegate ...
	
            // Both PowerUp and Turn Red is subscribed to newMultiDelegate
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                _newDelegate = PrintNum; // Insert PrintNum method to newDelegate
                
                // Always make sure the delegate is not called when it's null
                _newDelegate?.Invoke(50); // Pass the parameter
                _newDelegate -= PrintNum; // Unsubscribe
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _newDelegate = DoubleNum; // Subscribe
                _newDelegate?.Invoke(30); // Invoke
                _newDelegate -= PrintNum;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                _newMultiDelegate?.Invoke(); // Raise a new MultiDelegate event, both methods are executed
            }
        }

        private void OnDisable()
        {
            // Unsubscribe
            _newMultiDelegate -= PowerUp;
            _newMultiDelegate -= TurnRed; 
        }

        // Both of these functions follow the same template as our delegate
        private void PrintNum(int num)
        {
            Debug.Log($"Print Num: {num}");
        }

        private void DoubleNum (int num)
        {
            Debug.Log($"Double Num: {num*2}");
        }
        
        // Both methods have the return type of void
        void PowerUp()
        {
            Debug.Log("Orb is powering up!");
        }

        void TurnRed()
        {
            GetComponent<Renderer>().material.color = Color.red; //
        }
    }
}