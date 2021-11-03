using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 5f;
    public float deactivateTimer = 4f;

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
    {
        Vector3 temp = transform.position;
        temp.x += bulletSpeed * Time.deltaTime;
        transform.position = temp;
    }

    void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet" || target.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
