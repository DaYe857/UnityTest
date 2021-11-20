using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Charactermanager : MonoBehaviour
{ private Vector2 jumpDir;
    private Rigidbody2D _rigidbody;
    private float horizontalspeed, verizalSpeed;
    private float Timer;
    private bool isJump;
    private bool IsGround;
    private bool IsPlayJump;
    private int count=0;
    [Header("settings")]
    public float speed;
    public float JumpSpeed;
    private Animator animator;
    public float downSpeed;
    public float jumptime;
    private Renderer _renderer;
    private Collculeground collculeground0;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();
        collculeground0 = GetComponentInChildren<Collculeground>();
    }
   
    
    // Update is called once per frame
    void Update()
    {
       
        Move();
        TurnAround();
    }
    void Move()
    {
        Jump();
        IsPlayCrouchAnim(IsCrouch());
        horizontalspeed = Input.GetAxis("Horizontal");
        verizalSpeed = Input.GetAxis("Vertical");

        _rigidbody.velocity = horizontalspeed * Vector2.right*speed+jumpDir+Vector2.down*downSpeed;
        if (horizontalspeed != 0)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        
    }
    void TurnAround()
    {
        if (horizontalspeed != 0)
        {
            if (horizontalspeed <0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

    }
    private void WriteCount()
    {
        while(IsGround && Input.GetKeyDown("s"))
        {
            count++;
            break;
        }
        
    }
  private  bool IsCrouch()
    {
        WriteCount();
        
        if (count==1&&!isJump)
        {
            
            return true;
        }
        if ( count>1||isJump)
        {
            count=0;
            return false;
        }
        if (animator.GetBool("IsJump"))
        {
            return false;
        }
        return false;
    }
   private void IsPlayCrouchAnim(bool IsCrouch)
    {
        animator.SetBool("IsCrouch", IsCrouch);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsPlayJump = false;
       if(_rigidbody.velocity.y<0.5f&&_rigidbody.velocity.y>-0.2f)
        {   
            IsGround = true;
            Timer = 0;
        }
       
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGround = false;
    }
    void Jump()
    {
        if ( Input.GetKeyDown("space")&&collculeground0.isground)
        {
            collculeground0.isground = false;

        }
        if (!collculeground0.isground)
        {
            Timer += Time.deltaTime;
        }
        if (Timer <= jumptime && Input.GetKeyDown("space"))
        {
            isJump = true;


        }
        if (Timer > jumptime)
        {
            isJump = false;
        }
        if (isJump&&!IsCrouch())
        {
            animator.SetBool("IsJump", true);
            IsPlayJump = true;
            jumpDir = Vector2.up * JumpSpeed;
        }
        else
        {

            jumpDir = Vector2.zero;
        }
        if (!IsPlayJump)
        {
            animator.SetBool("IsJump", false);
        }

        if (Timer > jumptime && IsGround)
        {
            Timer = 0;
        }

    }
   
}
