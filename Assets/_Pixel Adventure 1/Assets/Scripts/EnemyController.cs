using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2.0f;
    public bool vertical;
    Rigidbody2D rigidbody2D;
    public float changeTime = 1.0f;
    float timer;
    int direction = 1;
    bool broken = true;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        if (!broken)
        {
            return;
        }
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction; ;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction; ;
        }

        rigidbody2D.MovePosition(position);
    }
    public void Fix()
    {
        broken = false;
        GetComponent<Rigidbody2D>().simulated = false; // tắt tính năng vật lý của rigidbody
        Destroy(gameObject);
    }
}
