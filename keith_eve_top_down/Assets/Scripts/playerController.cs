using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject bullet;
    private Rigidbody2D myRB;
    public Sprite invicibility;

    // Delete the comment slashes on the code below once you follow along with Day 13's Recording
    private AudioSource mySpeaker;
    public AudioClip pickupSound;
    public AudioClip punchSound;
    public AudioClip shootSound;

    private bool canShoot = true;
    public float shootCooldownTime;
    private float timeDifference;
    public bool invincible = false;
    public float invinicbleCooldownTime;
    private float timeDifference2;

    public float speed = 10;
    public float bulletLifespan = 1;
    public float bulletSpeed = 15;
    public int playerHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();

        // Delete the comment slashes on the code below once you follow along with Day 13's Recording
        mySpeaker = GetComponent<AudioSource>();

        playerHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            transform.SetPositionAndRotation(new Vector2(), new Quaternion());
            playerHealth = 3;
        }

        Vector2 velocity = myRB.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * speed;
        velocity.y = Input.GetAxisRaw("Vertical") * speed;

        myRB.velocity = velocity;

        if (canShoot)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);

                // Delete the comment slashes on the code below once you follow along with Day 13's Recording
                mySpeaker.clip = shootSound;
                mySpeaker.Play();

                Destroy(b, bulletLifespan);

                canShoot = false;
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                GameObject b = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 1), transform.rotation);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);

                // Delete the comment slashes on the code below once you follow along with Day 13's Recording
                mySpeaker.clip = shootSound;
                mySpeaker.Play();

                Destroy(b, bulletLifespan);

                canShoot = false;
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                GameObject b = Instantiate(bullet, new Vector2(transform.position.x - 1, transform.position.y), transform.rotation);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);

                // Delete the comment slashes on the code below once you follow along with Day 13's Recording
                mySpeaker.clip = shootSound;
                mySpeaker.Play();

                Destroy(b, bulletLifespan);

                canShoot = false;
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                GameObject b = Instantiate(bullet, new Vector2(transform.position.x + 1, transform.position.y), transform.rotation);
                b.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

                // Delete the comment slashes on the code below once you follow along with Day 13's Recording
                mySpeaker.clip = shootSound;
                mySpeaker.Play();

                Destroy(b, bulletLifespan);

                canShoot = false;
            }
        }

        if(canShoot == false)
        {
            timeDifference += Time.deltaTime;

            if (timeDifference >= shootCooldownTime)
            {
                canShoot = true;
                timeDifference = 0;
            }
        }

        if (invincible == true)
        {
            timeDifference2 += Time.deltaTime;

            if (timeDifference2 >= invinicbleCooldownTime)
            {
                invincible = false;
                timeDifference2 = 0;
                GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("enemy") && !invincible)
        {
            // Delete the comment slashes on the code below once you follow along with Day 13's Recording
            mySpeaker.clip = punchSound;
            mySpeaker.Play();

            playerHealth--;

            
        }

        else if (collision.gameObject.name.Contains("pickup"))
        {
            // This also means playerHealth = playerHealth + 1;
            //playerHealth++;

            // Delete the comment slashes on the code below once you follow along with Day 13's Recording
            mySpeaker.clip = pickupSound;
            mySpeaker.Play();

            invincible = true;

            GetComponent<SpriteRenderer>().color = UnityEngine.Color.yellow;


            collision.gameObject.SetActive(false);
        }
    }
}
