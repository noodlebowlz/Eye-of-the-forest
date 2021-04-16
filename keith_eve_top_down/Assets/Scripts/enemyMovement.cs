using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    // Variables
    public float detectionRadius = 3;
    public float movementSpeed = 5;
    public bool canMove = false;
    public bool movementDirection = false; // false = down | true = up
    public bool isFollowing = false;

    //Delete the comment slashes on the code below once you follow along with Day 12's Recording
    private Animator myAnimator;

    public Transform playerTarget;
    private Rigidbody2D myRB;
    private CircleCollider2D detectionZone;
    private Vector2 up;
    private Vector2 down;
    private Vector2 zero;

    // Start is called before the first frame update
    void Start()
    {
        up = new Vector2(0, movementSpeed);
        down = new Vector2(0, -movementSpeed);
        zero = new Vector2(0, 0);

        playerTarget = GameObject.Find("playerSprite").transform;

        // Assign our Rigidbody Component to our Rigidbody variable in our code.
        myRB = GetComponent<Rigidbody2D>();

        // Delete the comment slashes on the code below once you follow along with Day 12's Recording
        myAnimator = GetComponent<Animator>();

        detectionZone = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        detectionZone.radius = detectionRadius;

        if (isFollowing == false)
        {
            // Delete the comment slashes on the code below once you follow along with Day 12's Recording
            myAnimator.SetBool("isWalking", false);
            myRB.velocity = zero;
        }

        else if (isFollowing == true)
        {
            Vector3 lookPos = playerTarget.position - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            myRB.rotation = angle;
            lookPos.Normalize();

            // Delete the comment slashes on the code below once you follow along with Day 12's Recording
            myAnimator.SetBool("isWalking", true);

            myRB.MovePosition(transform.position + (lookPos * movementSpeed * Time.deltaTime));
        }
    }

    // Runs when our enemy PHYSICALLY collides with something.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("bullet"))
        {
            Destroy(collision.gameObject);

            // Delete the comment slashes on the code below once you follow along with Day 15's Recording
            GameObject.Find("GameManager").GetComponent<GameManager>().playerKillCount++;

            this.gameObject.SetActive(false);
        }
    }

    // Runs when our enemy collider is collided with OR when our enemy collides with another trigger.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
            isFollowing = true;
    }

    // Runs when an object leaves our enemy's trigger volume OR when our enemy leaves another trigger volume.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
            isFollowing = false;
    }
}
