using UnityEngine;
using System.Collections;

// Dependencies for gamepad toggling
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMovement : MonoBehaviour {

    public GameObject cursorMarker;
    public GameObject targetMarker;
    public float surfaceMaxTiltAngle = 45; // TODO use navmesh settings
    UnityEngine.AI.NavMeshAgent agent;
    CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
	}

    void LateUpdate() // Late to prevent presumed race
    {
        RaycastHit hit;
        if (GetValidMoveHit(out hit))
        {
            ShowCursorMarker(hit);
            if (Input.GetButton(Buttons.PrimaryAction))
            {
                agent.SetDestination(hit.point);
                targetMarker.transform.position = cursorMarker.transform.position;
                targetMarker.transform.rotation = cursorMarker.transform.rotation;
            }
        }
        else
        {
            HideCursorMarker();
        }

        Vector3 positionDeltas = targetMarker.transform.position - transform.position;
        bool atTargetLocation = Vector3.SqrMagnitude(positionDeltas) < 1;
        targetMarker.SetActive(!atTargetLocation);
    }

    private bool GetValidMoveHit(out RaycastHit hit)
    {
        if (cameraRaycaster.layerHit == Layer.Walkable)
        {
            hit = cameraRaycaster.hit;
            bool levelSurface = Vector3.Angle(hit.normal, Vector3.up) < surfaceMaxTiltAngle;
            return levelSurface;
        }
        hit = new RaycastHit();
        return false;
    }

    private void ShowCursorMarker(RaycastHit hit)
    {
        cursorMarker.SetActive(true);
        cursorMarker.transform.position = hit.point;
        cursorMarker.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
    }

    private void HideCursorMarker()
    {
        cursorMarker.SetActive(false);
    }

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
	// TODO note this requires re-wiring from UI Toggle in each new scene
	public void ToggleGamePad()
	{
		print("Toggling gamepad control.");
		ToggleComponent<AICharacterControl>();
		ToggleComponent<PlayerMovement>();
		ToggleComponent<ThirdPersonUserControl>();
	}
}
