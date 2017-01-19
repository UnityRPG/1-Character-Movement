using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {

    public GameObject cursorMarker;
    public GameObject targetMarker;
    public float surfaceMaxTiltAngle = 45;
    NavMeshAgent agent;
    Cursor cursor;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        cursor = FindObjectOfType<Cursor>();
	}

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (GetValidMoveHit(out hit))
        {
            ShowCursorMarker(hit);
            if (Input.GetButton(Button.PrimaryAction))
            {
                agent.SetDestination(hit.point);
                targetMarker.transform.position = cursorMarker.transform.position;
                targetMarker.transform.rotation = cursorMarker.transform.rotation;
            }
        } else
        {
            HideCursorMarker();
        }

        Vector3 positionDeltas = targetMarker.transform.position - transform.position;
        bool atTargetLocation = Vector3.SqrMagnitude(positionDeltas) < 1;
        targetMarker.SetActive(!atTargetLocation);
    }

    private bool GetValidMoveHit(out RaycastHit hit)
    {
        if (cursor.GetHighlighted(out hit, Action.Walkable))
        {
            bool levelSurface = Vector3.Angle(hit.normal, Vector3.up) < surfaceMaxTiltAngle;
            return levelSurface;
        }
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
}
