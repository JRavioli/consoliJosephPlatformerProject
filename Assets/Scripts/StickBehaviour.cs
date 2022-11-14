using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBehaviour : MonoBehaviour
{
    const int fallSpeed = 7;
    public float topY;
    public float botY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, fallSpeed * Time.deltaTime);
        if (transform.position.y <= botY)
        {
            transform.position = new Vector3(transform.position.x, topY);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "RespawnBox")
        {
            Destroy(collision.gameObject);
        }
    }
}
