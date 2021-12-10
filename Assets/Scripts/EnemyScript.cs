using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int value;
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    private GameManager gameManager;
    public System.Action killed;
    

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();
        //repeats something over and over again Xamount of time
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }
    //Updates the frame
    private void AnimateSprite()
    {
        _animationFrame++;
        //Makes it so that the sprites doesn't exceed the frame
        if (_animationFrame >= this.animationSprites.Length)
        {
            _animationFrame = 0;
        }
        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }
    private void OnTriggerEnter2D(Collider2D other)
    {//Checks if an enemy is hit by a player bullet
        if(other.gameObject.layer == LayerMask.NameToLayer("PBullet"))
        {
            gameManager.IncreaseScore(value);
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }


    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //if (collision.gameObject.tag == "SideBoundary")
    //        {
    //            formation.movingSide = false;
    //            formation.setDestinationAndMoveDown();
    //        }
    //if(collision.gameObject.tag == "Boundary")
    //        {

    //        }
    //      }
}
