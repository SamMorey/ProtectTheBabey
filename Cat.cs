using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public PolygonCollider2D collider;
    public GameObject particle;
    public GameObject splatHolder;
    public List<GameObject> catBits;
    public LevelManager levelManager;
    public AudioManager audioManager;


    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        splatHolder = GameObject.FindGameObjectWithTag("BloodHolder");
        StartCoroutine("SetColider");
        StartCoroutine("Kill");
    }

    // Update is called once per frame
    

    IEnumerator SetColider()
    {
        yield return new WaitForSeconds(.2f);
        collider.enabled = true;
    }
    IEnumerator Kill()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            audioManager.PlayCatch();
            Destroy(gameObject);
        }
        else
        {
            audioManager.PlayBlowup();
            GameObject splat = Instantiate(particle, gameObject.transform);
            splat.transform.SetParent(splatHolder.transform, true);
            for(int i = 0; i < catBits.Count; i++)
            {
                GameObject bits = Instantiate(catBits[i], gameObject.transform);
                bits.transform.SetParent(splatHolder.transform, true);
                bits.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.position * -45);
            }
            levelManager.RestartScene();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Goal")
        {
            Destroy(gameObject);
        }
    }
}
