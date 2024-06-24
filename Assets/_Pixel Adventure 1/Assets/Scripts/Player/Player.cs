using System.Collections;
using System.Collections.Generic;
using GameTool;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;
    
    [SerializeField] private float jumpVelo = 15f;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float dirX;
    [SerializeField] private bool isGrounded;
    public GameObject projectilePrefab;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 lookDirection1 = new Vector2(-1, 0);

    private enum MovementState
    {
        Idle,
        Run,
        Jump,
        Fall
    }
    
    
    private void Start()
    {
        AudioManager.Instance.PlayMusic(eMusicName.Game);
        jumpVelo = 15f;
        moveSpeed = 6f;
    }
    private void Update()
    {
        isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, 
            Vector2.down, .1f, jumpableGround); // kiểm tra va chạm, nếu có va chạm với các đối tượng thuộc lớp đất IsGround = true
        dirX = Input.GetAxisRaw("Horizontal"); // gán giá trị đầu vào từ trục ngang cho dirX, dùng để điều khiển di chuyển ngang
        
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); // gán vận tốc di chuyển theo chiều ngang và chiều dọc cho velocity 
        
        if (Input.GetButtonDown("Jump") && isGrounded) // && isGrounded , nếu nhấn jump thì sẽ nhảy
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelo);
            AudioManager.Instance.Shot(eSoundName.Jump);
        }
        AnimationUpdate();
        if (Input.GetKeyDown(KeyCode.C)) // nhấn C để bắn đạn
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayer character = hit.collider.GetComponent<NonPlayer>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }
    private void AnimationUpdate()
    {
        MovementState state; // kiểu dữ liệu liệt kê, chứa các giá trị đại diện cho trang thái di chuyển như nhảy, đứng yên, rơi
        if (dirX > 0f)
        {
            state = MovementState.Run;
            sprite.flipX = false; // dùng để lật ngang hình ảnh của đối tượng, xác định hướng của nhân vật trên trụ X
        }
        else if (dirX < 0f)
        {
            state = MovementState.Run;
            sprite.flipX = true; // là true thì hình ảnh sẽ được lật ngang
        }
        else
        {
            state = MovementState.Idle; // đứng yên
        }

        if (rb.velocity.y > .1f) 
        {
            state = MovementState.Jump; // nhảy
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.Fall; // die
        }
        
        anim.SetInteger("state", (int)state);
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        if (dirX > 0f)
        {
            projectile.Launch(lookDirection, 300); // nếu vận tốc trên trục x>0 thì bắn về bên phải
        }
        else if(dirX < 0f)
        {
            projectile.Launch(lookDirection1, 300);
        }
        else
        {
            if (sprite.flipX)
            {
                projectile.Launch(lookDirection1, 300); // nếu nhân vật đang quay về bên trái thì bắn về bên trái
            }
            else
            {
                projectile.Launch(lookDirection, 300);
            }
        }

    }

}
