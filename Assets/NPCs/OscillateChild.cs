using UnityEngine;
using System.Collections;

public class OscillateChild : MonoBehaviour
{

    [SerializeField] private float zMovementInMeters; // default to 0
    [SerializeField] [Range(0.5f, 60)]  // Note cannot be 0
    private float oscillationPeriodInSeconds = 4.0f;

    // Update is called once per frame
    void Update()
    {
        // TODO fix this for runtime changes
        float offsetFraction = Mathf.Cos(Time.time * Mathf.PI / oscillationPeriodInSeconds);
        transform.localPosition += new Vector3(0, 0, zMovementInMeters * offsetFraction * Time.deltaTime);
    }
}
