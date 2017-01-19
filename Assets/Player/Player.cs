using UnityEngine;
using System.Collections;

// Dependencies for gamepad toggling
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleGamePad()
    {
        print("Toggling gamepad control.");

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
