using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Viewer fields
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Vector3 mousePosition;
    [SerializeField]
    private GameObject cat;
    [SerializeField]
    private Transform throwPoint;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    //Calculations for throwing
    [SerializeField]
    private float startingThrowForce;
    private Vector3 throwDirection;
    private float throwForce;

    //Checks for input polling in update to pass to fixed update
    private bool isTeleport = false;
    private bool isThrow = false;
    private bool canTeleport = false;
    private bool canThrow = true;


    private RaycastHit2D hit;
    public ContactFilter2D chooseMask;

    [SerializeField]
    private GameObject holdCat;
    [SerializeField]
    private GameObject flipPoint;
    [SerializeField]
    private GameObject originalPoint;
    [SerializeField]
    private LevelManager levelManager;

    public AudioManager audioManager;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }

    void Update()
    {
        PollMouseInput();
        FlipUpdate();
    }

    private void FixedUpdate()
    {
        TeleportCalc();
        ThrowCalc();
    }

    private void PollMouseInput()
    {      
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        throwDirection = mousePosition - gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.E) && canTeleport)
        {
            isTeleport = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            throwForce = startingThrowForce;
            StartCoroutine("ChargeThrow");
        }
        if (Input.GetMouseButtonUp(0))
        {
            isThrow = true;
            canTeleport = true;
        }
    }

    private void FlipUpdate()
    {
        if (mousePosition.x < gameObject.transform.position.x)
        {
            spriteRenderer.flipX = true;
            holdCat.transform.position = flipPoint.transform.position;
        }
        else if (mousePosition.x > gameObject.transform.position.x)
        {
            spriteRenderer.flipX = false;
            holdCat.transform.position = originalPoint.transform.position;
        }
    }
    
    private void TeleportCalc()
    {
        if (isTeleport && canTeleport)
        {
            hit = Physics2D.Linecast(gameObject.transform.position, mousePosition);
            if (hit)
            {
                gameObject.transform.position = hit.point;
            }
            else
            {
                gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            }
            audioManager.PlayTeleport();
            isTeleport = false;
        }
    }

    private void ThrowCalc()
    {
        if (isThrow && canThrow)
        {
            audioManager.PlayThrow();
            canThrow = false;
            holdCat.SetActive(false);
            GameObject clone = Instantiate(cat, throwPoint);
            clone.transform.parent = null;
            clone.GetComponent<Rigidbody2D>().AddForce(throwDirection * throwForce);
            isThrow = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Cat")
        {
            canTeleport = false;
            canThrow = true;
            holdCat.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Goal")
        {
            audioManager.PlayWin();
            levelManager.NextScene();
        }
    }

   
    IEnumerator ChargeThrow()
    {
        yield return new WaitForSeconds(1);
        throwForce = startingThrowForce * 2;
    }
}
