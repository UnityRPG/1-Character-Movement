using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace RPG
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class PlayerMovement : MonoBehaviour
    {
        public static bool isInDirectMode = false;  // TODO consider setter / enum later

        [SerializeField] float attackMoveStopRadius = 4f;
        [SerializeField] float walkMoveStopRadius = 0.2f;

        ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
        Camera mainCamera;                  // A reference to the main camera in the scenes transform
        CameraRaycaster cameraRaycaster;
        Vector3 currentClickTarget;
        float currentClickRange = 1f;
        
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
            if (Input.GetKeyDown(KeyCode.G))
            {
                isInDirectMode = !isInDirectMode;
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
                print("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString());
                switch (cameraRaycaster.layerHit)
                {
                    case Layer.Walkable:
                        currentClickRange = walkMoveStopRadius;
                        break;
                    case Layer.Enemy:
                        currentClickRange = attackMoveStopRadius;
                        break;
                    default:
                        return; // Do nothing clicking outside world
                }
                currentClickTarget = cameraRaycaster.hit.point;  // So not set in default case
            }

            var playerToClickPoint = transform.position - currentClickTarget;
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
