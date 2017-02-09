using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_UI : MonoBehaviour {

    [SerializeField]
    [Tooltip("Select the NPC AI prefab")]
    GameObject npcUIPrefab = null;

    Camera cameraToLookAt;
    
    // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
        Instantiate(npcUIPrefab, transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame 
    void Update()
    {
        //var directionToLook = Quaternion.LookRotation(cameraToLookAt.transform.position - transform.position); 
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(-cameraToLookAt.transform.forward);
    }
}
