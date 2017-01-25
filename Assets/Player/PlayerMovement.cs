using System;
using UnityEngine;

namespace RPG
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        float currentWeaponRange = 4f;

        ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        Camera mainCamera;                  // A reference to the main camera in the scenes transform
        CameraRaycaster cameraRaycaster;
        Vector3 currentClickTarget;
        float currentClickRange = 1f;
        Vector3 playerToClickPoint = Vector3.zero;
        public static bool isInDirectMode = false;

        private void Start()
        {
            mainCamera = Camera.main;
            cameraRaycaster = mainCamera.GetComponent<CameraRaycaster>();
            m_Character = GetComponent<ThirdPersonCharacter>();
            currentClickTarget = transform.position;
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                isInDirectMode = !isInDirectMode;
                print("Is in direct control mode: " + isInDirectMode);
            }

            if (isInDirectMode) {
                ProcessDirectMovement();
            }
            else
            {
                ProcessMouseMovement();
            }
        }

        private void ProcessMouseMovement()
        {
            if (Input.GetMouseButton(0))
            {
                currentClickTarget = cameraRaycaster.hit.point;
                switch (cameraRaycaster.layerHit)
                {
                    case Layer.Walkable:
                        currentClickRange = 0f;
                        break;
                    case Layer.Enemy:
                        currentClickRange = currentWeaponRange;
                        break;
                    default:
                        currentClickRange = 0f;
                        break;
                }
            }

            playerToClickPoint = transform.position - currentClickTarget;
            if (playerToClickPoint.magnitude >= currentClickRange)
            {
                m_Character.Move(currentClickTarget - transform.position, false, false);
            }
            else
            {
                m_Character.Move(Vector3.zero, false, false);
            }
        }

        private void ProcessDirectMovement()
        {
            // read inputs
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // calculate camera relative direction to move:
            Vector3 camForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 m_Move = v * camForward + h * mainCamera.transform.right;

            // pass all parameters to the character control script#
            m_Character.Move(m_Move, false, false); 
        }
    }
}
