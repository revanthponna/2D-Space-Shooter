using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float maxY, minY;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform attackPoint;

    public float attackTimer = 0.35f;
    private float current_attackTimer;
    private bool canAttack;

    public AudioSource laserAudio;
    // Start is called before the first frame update
    void Start()
    {
        current_attackTimer = attackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        // Input controls for the spaceship movement
        if(Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += Time.deltaTime * playerSpeed;
            // Making sure spaceship does not go above the screen
            if (temp.y > maxY) temp.y = maxY;
            transform.position = temp;
        }

        if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= Time.deltaTime * playerSpeed;
            // Making sure spaceship does not go below the screen
            if (temp.y < minY) temp.y = minY;
            transform.position = temp;
        }
        // Allowing the player to shoot only after a time gap
        attackTimer += Time.deltaTime;
        if (attackTimer > current_attackTimer) canAttack = true;
        // Input control for shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canAttack)
            {
                canAttack = false;
                attackTimer = 0f;

                Instantiate(bullet, attackPoint.position, Quaternion.identity);
                laserAudio.Play(); // Playing audio while shooting
            }
        }
    }
    // Checking collisions with bullets or enemies
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet" || target.tag == "Enemy")
        {
            canAttack = false;
            gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver"); // Game Over if spaceship collides with bullet or enemies
        }
    }
}
