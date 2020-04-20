using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatParts : MonoBehaviour
{
    public GameObject splatPrefab;
    public GameObject bloodHolder;
    public AudioManager audioManager;

    private void Awake()
    {
        bloodHolder = GameObject.FindGameObjectWithTag("BloodHolder");
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            audioManager.PlayChunks();
            GameObject splat = Instantiate(splatPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
            splat.transform.SetParent(bloodHolder.transform, true);
            BloodSplat splatSript = splat.GetComponent<BloodSplat>();
            splatSript.Intialize();
        }
    }
    
}
