using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public AudioClip collisionClip;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public bool isplaying = false;

    private AudioSource AS, collisionAS;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        collisionAS = gameObject.AddComponent<AudioSource>() as AudioSource;
        collisionAS.clip = collisionClip;
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count" + count.ToString();
        if(count >= 10)
        {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D))
        //{
        //    AS.Play();
        //}
        //if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        //{
        //    AS.Pause();
        //}

       //  print(rb.velocity.magnitude.ToString());

        float speed = rb.velocity.magnitude;

        if(speed >= 1f )
        {
            if (!isplaying)
            {
                AS.Play();
                isplaying = true;
            }
        }
        else
        {
            AS.Pause();
            isplaying = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Ground")
        {
            collisionAS.Play();

        }
    }
}
