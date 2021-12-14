using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public BulletScript bulletprefab;
    public float speed = 5.0f;
    public GameObject bullet;
    private bool _bulletActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.position;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        //Making sure that it clamps the (Position of the position.x within the, leftEdge of the screen + 1.0f to stop clipping and the, rightEdge of the screen - 1.0f to stop clipping
        position.x = Mathf.Clamp(position.x, leftEdge.x + 1.0f, rightEdge.x - 1.0f);
        //Reading position value
        this.transform.position = position;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }
    private void Shoot()
    {
        if (!_bulletActive)
        {
            BulletScript bullet = Instantiate(this.bulletprefab, this.transform.position, Quaternion.identity);
            bullet.destroyed += BulletDestroyed;
            _bulletActive = true;
        }
    }
    private void BulletDestroyed()
    {
        _bulletActive = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Invader") || other.gameObject.layer == LayerMask.NameToLayer("EBullet"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
