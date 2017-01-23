using UnityEngine;
using System.Collections;

public class Oscillator : MonoBehaviour
{
    [SerializeField] float zMovementInMeters; // default to 0
    [SerializeField] [Range(0.5f, 60)] float oscillationPeriodInSeconds = 4.0f;

    // Update is called once per frame
    void Update()
    {
        // Optionally osciallates enemy position
        float offsetFraction = Mathf.Cos(Time.time * Mathf.PI / oscillationPeriodInSeconds);
        transform.localPosition += new Vector3(0, 0, zMovementInMeters * offsetFraction * Time.deltaTime);
        //  TODO fix this for runtime changes
    }
}
