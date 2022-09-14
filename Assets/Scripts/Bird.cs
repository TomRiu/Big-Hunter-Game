using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float moveSpeed;
    public float flySpeed;
    private float downBound = -2;
    public int point;

    public GameObject deathVFX;

    private Rigidbody2D rb;
    [HideInInspector] public bool moveLeft;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        MovingDirection();
        flySpeed = Random.Range(-flySpeed, flySpeed) + GameManger.instance.UpgradeFlySpeed;
        moveSpeed = moveSpeed + GameManger.instance.UpgradeMoveSpeed;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(moveLeft == true)
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, flySpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, flySpeed * Time.deltaTime, 0);
        }
        if(transform.position.y <= downBound)
        {
            flySpeed *= -1;
        }
        Flip();
    }

    public virtual void MovingDirection()
    {
        moveLeft = transform.position.x > 0 ? true : false;
    }

    public virtual void Flip()
    {
        if(moveLeft == true && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if(moveLeft == false && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        if(deathVFX)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "ClearClone")
        {
            Destroy(gameObject);
        }
    }
}
