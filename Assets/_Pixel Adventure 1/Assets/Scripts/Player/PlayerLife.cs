using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) // xử lý va chạm
    {
        
        if (other.gameObject.CompareTag("Trap")) // kiểm tra tag của đối tượng có là Trap không
        {
            Die();                            // nếu đúng thì die
            CanvasManager.Instance.Push(eUIName.DeadPopup); // gọi giao diện DeadPopup
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static; // đối tượng sẽ không di chuyển nữa và không ảnh hưởng bởi lực vặt lý
        anim.SetTrigger("death"); // kích hoạt trigger death trong animation
    }
}
