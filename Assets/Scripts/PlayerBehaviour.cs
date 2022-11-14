using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Vector2 jumpForce = new Vector2(0, 400); //base jump force of the player, changes with each level
    private bool onGround = false; //checks if the player is on the ground
    public float speed = 10; //base speed of the player, changes with each level
    private float grapeCount = 0; //number of grapes you eat
    public Vector2 respawnLocation = new Vector2(0, 0); //a saved location for the player to respawn to
    public TMP_Text GrapeText;
    public GameObject RaceText;
    public GameObject NPCText;
    public GameObject FenceText;
    public GameObject Fence;
    private bool canInteract = false; //checks if the player can interact with something

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //gets the rigidbody of the player object and saves it
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && onGround == true) //if player is on ground and space is pressed
        {
            onGround = false; //player is no longer on the ground and unable to jump
            rb2d.AddForce(jumpForce); //player jumps
        }
        float xMove = Input.GetAxis("Horizontal");

        Vector2 newPos = gameObject.transform.position;

        newPos.x += xMove * speed * Time.deltaTime;
        //newPos.x = Mathf.Clamp(newPos.x, -8.1f, 25.0f);
        //change this ^ so that it's different for every level

        gameObject.transform.position = newPos;  //moves the player horizontally

        if (Input.GetKeyDown("v") && canInteract == true) //if v is pressed and is in the trigger of an NPC
        {
            NPCText.SetActive(true); //activates the NPC dialogue
        }
        //debug scene changes
        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("FoxGrapes");
        }
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("TortoiseHare");
        }
        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("MouseLion");
        }
        if (Input.GetKeyDown("4"))
        {
            SceneManager.LoadScene("WolfSheep");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true; //when the player is colliding with solid ground, they are able to jump
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false; //when the player leaves ground, they are unable to jump
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            respawnLocation = gameObject.transform.position; //saves the player's current position based on if they are on the ground
        }
        if (collision.transform.tag == "Projectile")
        {
            //Destroy(collision.gameObject); //No projectile will stay on screen when it hits the player
            float knockBack = Random.Range(-2, 2); //picks out of four numbers, -2, -1, 0, and 1
            if (knockBack == 0)
            {
                knockBack = 1; //since using Random.Range(-1, 2) can still give a value of 0, 0 will instead be replaced by 1
            }
            if (knockBack == -2)
            {
                knockBack = -1; //gives both -1 and 1 an equal chance of being the knockBack value
            }
            rb2d.AddForce(new Vector2(400 * knockBack, 100)); //pushes the player to the left or right when being hit with a projectile
        }
        if (collision.transform.tag == "RespawnBox")
        {
            gameObject.transform.position = respawnLocation; //after falling off the map, the player goes back to the last place they jumped or fell from
        }
        if (collision.transform.tag == "Grape")
        {
            Destroy(collision.gameObject);
            grapeCount += 1;
            GrapeText.text = "Grapes: " + grapeCount;
        }
        if (collision.transform.tag == "FinishLine")
        {
            RaceText.SetActive(true);
        }
        if (collision.transform.tag == "NPC")
        {
            canInteract = true;
        }
        if (collision.transform.tag == "Fence")
        {
            FenceText.SetActive(true);
        }
        if (collision.transform.tag == "Hide")
        {
            Destroy(collision.gameObject);
            Destroy(Fence);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "NPC")
        {
            NPCText.SetActive(false);
            canInteract = false;
        }
        if (collision.transform.tag == "Fence")
        {
            FenceText.SetActive(false);
        }
    }
}

//stuff for dialogue
//TextGrab