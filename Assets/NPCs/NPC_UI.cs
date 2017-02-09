using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;

public class NPC_UI : MonoBehaviour {

    [SerializeField]
    [Tooltip("Select the NPC AI prefab")]
    GameObject npcUIPrefab = null;

    Camera cameraToLookAt;

    [SerializeField]
    RawImage healthBarRawImage;
    Enemy enemy;

    // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
        enemy = GetComponentInParent<Enemy>();

        print(enemy);
        healthBarRawImage = GetComponentInChildren<RawImage>();
        print(healthBarRawImage);

        Instantiate(npcUIPrefab, transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame 
    void Update()
    {
        //var directionToLook = Quaternion.LookRotation(cameraToLookAt.transform.position - transform.position); 
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);

        float xValue = -(enemy.healthAsPercentage / 2f) - 0.5f;  // TODO note hard code
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}