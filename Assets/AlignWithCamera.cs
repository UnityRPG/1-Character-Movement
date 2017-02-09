using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithCamera : MonoBehaviour {

    Camera cameraToLookAt;

    // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
    }

    // Update is called once per frame 
    void Update()
    {
        //var directionToLook = Quaternion.LookRotation(cameraToLookAt.transform.position - transform.position); 
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(-cameraToLookAt.transform.forward);
    }
}
