using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 50f;

    public bool canShoot;
    public bool canRotate;
    private bool canMove = true;

    public float boundX = -11f;

    public Transform attack_Point;
    public GameObject bullet_Prefab;

    private Animator anim;
    private AudioSource explosionSound;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Randomizing the rotation speed and direction of the obstacles
        if (canRotate)
        {
            if(Random.Range(0,2) > 0)
            {
                rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
                rotateSpeed *= -1f;
            }
            else
            {
                rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
            }
        }

        if (canShoot)
        {
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateEnemy();
    }
    // Function to move the enemy from right to left of the screen
    void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x -= Time.deltaTime * speed;
            transform.position = temp;

            if(temp.x < boundX)
            {
                gameObject.SetActive(false); // Turning off the game object after it has passed the left edge of the screen
            }
        }
    }
    
    void RotateEnemy()
    {
        if (canRotate)
        {
            transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * rotateSpeed), Space.World);
        }
    }
    
    void StartShooting()
    {
        // Spawning bullet from the point of enemy spaceship
        GameObject bullet = Instantiate(bullet_Prefab, attack_Point.position, Quaternion.identity); 
        bullet.GetComponent<BulletScript>().isEnemyBullet = true;

        if (canShoot)
        {
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
    }

    void TurnGameObjectOff()
    {
        gameObject.SetActive(false);
    }
    
    // Checking for collision between enemy and bullet/player
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet" || target.tag == "Player")
        {
            canMove = false;
            if (canShoot)
            {
                canShoot = false;
                CancelInvoke("StartShooting");
            }

            Invoke("TurnGameObjectOff", 3f);

            explosionSound.Play(); // Playing explosion sound when collision happens
            anim.Play("Destroy"); // Playing explosion animation when collision happens
        }
    }
}
