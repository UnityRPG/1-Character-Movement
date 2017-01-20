using UnityEngine;
using System.Collections;

// Dependencies for gamepad toggling
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour {
    
    private void ToggleComponent<T>() where T : MonoBehaviour
    {
        T componentToToggle = GetComponent<T>();
        if (componentToToggle.isActiveAndEnabled)
        {
            componentToToggle.enabled = false;
        }
        else
        {
            componentToToggle.enabled = true;
        }
    }

    // Toggles the necessary components to switch from gamepad to keyboard control
    // This method may be called directly from UI control such as a button or toggle
    public void ToggleGamePad()
    {
        print("Toggling gamepad control.");
        ToggleComponent<AICharacterControl>();
        ToggleComponent<CursorMovement>();
        ToggleComponent<ThirdPersonUserControl>();
    }
}
