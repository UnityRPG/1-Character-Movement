using System;
using UnityEngine;

namespace RPG
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class PlayerMovement : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Camera mainCamera;                  // A reference to the main camera in the scenes transform
        private Vector3 m_Move;
        
        private void Start()
        {
            mainCamera = Camera.main;
            m_Character = GetComponent<ThirdPersonCharacter>();
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            ReadGamepadAndKB();
            ReadMouseClicks();
            m_Character.Move(m_Move, false, false); // pass all parameters to the character control script#
        }

        private void ReadGamepadAndKB()
        {
            // read inputs
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // calculate camera relative direction to move:
            Vector3 camForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * camForward + h * mainCamera.transform.right;
        }

        private void ReadMouseClicks()
        {
            if (Input.GetMouseButton(0))
            {
                //var worldSpaceClick = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                //Debug.LogFormat ("From " + transform.position.ToString() + " to " + worldSpaceClick.ToString());
                //m_Move = worldSpaceClick - transform.position;
            }
        }
    }
}
