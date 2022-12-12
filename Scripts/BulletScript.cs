using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 5f;
    public float deactivateTimer = 4f; // Bullet will deactivate after 4secs if there is no collision

    [HideInInspector]
    public bool isEnemyBullet = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isEnemyBullet)
        {
            bulletSpeed *= -1f;
        }
        Invoke("DeactivateBullet", deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {   // Making bullet move with specified speed
        Vector3 temp = transform.position;
        temp.x += bulletSpeed * Time.deltaTime;
        transform.position = temp;
    }

    void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }
    // Checking for collision between bullet with another bullet or enemy
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet" || target.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
