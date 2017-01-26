using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    public int currentHealthPoints; // TODO remove
    [SerializeField] int initialHealthPoints = 100;
    [SerializeField] int maxHealthPoints = 100;
    [SerializeField] Material ghostMaterial;

    [SerializeField] AudioClip[] deathSounds;
    [SerializeField] AudioClip[] startSounds;


    bool isAlive = true;
    SkinnedMeshRenderer playerSkin;
       

    // Use this for initialization
    void Start () {
        currentHealthPoints = initialHealthPoints;
        playerSkin = GetComponentInChildren<SkinnedMeshRenderer>();
        AudioSource.PlayClipAtPoint(PickRandomAudioClip(startSounds), transform.position); // TODO stick to player

    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }

    public void TakeDamage(int damagePoints)
    {
        var newHealthPoints = currentHealthPoints - damagePoints;
        currentHealthPoints = Mathf.Clamp(newHealthPoints, 0, maxHealthPoints);
    }

    void Update()
    {
        if (!isAlive) { return; }

        if (currentHealthPoints == 0)
        {
            isAlive = false;
            AudioSource.PlayClipAtPoint(PickRandomAudioClip(deathSounds), transform.position);
            playerSkin.material = ghostMaterial;
            gameObject.SetActive(false);
        }
    }

    AudioClip PickRandomAudioClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}
