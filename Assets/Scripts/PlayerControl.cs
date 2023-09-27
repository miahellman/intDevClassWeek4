using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    float horizontalMove;
    public float speed = 5f;

    Rigidbody2D myBody;

    bool grounded = false;

    public float castDist = 1;

    public float jumpPower = 2f;
    public float gravityScale = 5f;
    public float gravityFall = 10f;

    bool jump = false;


    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        Debug.Log(horizontalMove);

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            jump = true;
        }


    }

    private void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;

        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

        if (myBody.velocity.y >= 0)
        {
            myBody.gravityScale = gravityScale;

        } 
        else if (myBody.velocity.y <= 0)
        {
            myBody.gravityScale = gravityFall;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);

        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red); //debug line showing ray in scene view
        
      //  if (hit.collider != null && hit.transform.name == "Ground")
        if (hit.collider != null&& hit.transform.tag == "Ground")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }



        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0f);

    }
}

