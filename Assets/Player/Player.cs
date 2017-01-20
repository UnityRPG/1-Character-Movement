using UnityEngine;
using System.Collections;

// Dependencies for gamepad toggling
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour {

    // Toggles the necessary components to switch from gamepad to keyboard control
    // This method may be called directly from UI control such as a button or toggle
    public void ToggleGamePad()
    {
        print("Toggling gamepad control.");

        // TODO discuss idea of creating a generic ToggleComponent method here. Use Gameobject or MonoBehavoiur?
        AICharacterControl aiCharControl = GetComponent<AICharacterControl>();
        if (aiCharControl.isActiveAndEnabled)
        {
            aiCharControl.enabled = false;
        }
        else
        {
            aiCharControl.enabled = true;
        }

        CursorMovement cursorMovement = GetComponent<CursorMovement>();
        if (cursorMovement.isActiveAndEnabled)
        {
            cursorMovement.enabled = false;
        }
        else
        {
            cursorMovement.enabled = true;
        }

        ThirdPersonUserControl thirdPersonUserControl = GetComponent<ThirdPersonUserControl>();
        if (thirdPersonUserControl.isActiveAndEnabled)
        {
            thirdPersonUserControl.enabled = false;
        }
        else
        {
            thirdPersonUserControl.enabled = true;
        }
    }
}
