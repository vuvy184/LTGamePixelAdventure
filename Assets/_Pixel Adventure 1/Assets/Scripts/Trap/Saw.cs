using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Trap
{
    [SerializeField] private float speed = 2f;
    private void Update()
    {
        transform.Rotate(0f, 0f, 360 * speed * Time.deltaTime);
    }
}
