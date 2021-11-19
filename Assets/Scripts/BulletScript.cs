using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Transform bullet;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        bullet.position += Vector3.up * bulletSpeed;
        if (bullet.position.y >= 10)
            Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D bulletCollide)
    {
        if (bulletCollide.tag == "Enemy")
        {
            Destroy(bulletCollide.gameObject);
            Destroy(gameObject);
        }else if (bulletCollide.tag == "Base") {
            Destroy(gameObject);
        }
    }
}
