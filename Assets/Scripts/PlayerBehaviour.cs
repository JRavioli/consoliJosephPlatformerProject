using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Vector2 jumpForce = new Vector2(0, 500);
    private bool onGround = false;
    public float speed = 7;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && onGround == true)
        {
            onGround = false;
            rb2d.AddForce(jumpForce);
        }
        float xMove = Input.GetAxis("Horizontal");

        Vector2 newPos = gameObject.transform.position;

        newPos.x += xMove * speed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, -8.1f, 25.0f);

        gameObject.transform.position = newPos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //make the player respawn on the last platform that was jumped off of: during the jump, save the current location, and if the player hits the respawn box, set them back to the saved location
    }
}
