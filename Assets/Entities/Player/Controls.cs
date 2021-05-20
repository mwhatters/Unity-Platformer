using UnityEngine.InputSystem;
using UnityEngine;

namespace Player {
    public class Controls : MonoBehaviour
    {
        // Start is called before the first frame update
        internal float moveX = 0f;
        internal float moveY = 0f;
        internal BufferedInput Jump;

        void Start() 
        {
            Jump = new BufferedInput(Timer.New(gameObject, 0.05f));
        }

        public void OnMoveX(InputAction.CallbackContext context)
        {
            moveX = context.ReadValue<float>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            Jump.SetValue(context.ReadValue<float>());
            Jump.UpdateBuffer();
        }
    }
}