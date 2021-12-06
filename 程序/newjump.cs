using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newjump : MonoBehaviour
{ private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigbody2D;
    public LayerMask ground;
    public Transform checkground;
    public float Force,speed;
    public float DownForce;
    bool IsGround = false;
    bool IsCanStop;
    bool IsCanStop0;
    private int extraTime = 1;
    public Animator animator;
    private float jumpforce;
    bool jumpPress;
    private int count = 0;
    private int SpingCount=1;
    float SpingwaitTime=0;
    bool IsSping;
    float temp;
    public float SpingSpeed,MaxSpingTime,SpingTime;
    private void FixedUpdate()
    {


       


    }
    void Start()
    {
        jumpforce = Force;
        _rigbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {   
        moveBetter();
        _jump();
        if (Input.GetKeyDown("s"))
        {
            jumpPress = true;
        }
        if (Input.GetKeyUp("s"))
        {
            jumpPress = false;
        }
        Teleport();
       
    }
    void moveBetter()
    {
        IsCanStop0= Physics2D.Raycast(transform.position +new Vector3(0, 0, 0), transform.right, _spriteRenderer.bounds.size.x / 2.5f, ground);
        IsCanStop = Physics2D.Raycast(transform.position-new Vector3(0, _spriteRenderer.bounds.size.y/3, 0), transform.right, _spriteRenderer.bounds.size.x/2.5f,ground );
        //Debug.DrawRay(transform.position - new Vector3(0, _spriteRenderer.bounds.size.y / 3, 0), transform.right, Color.red, _spriteRenderer.bounds.size.x);
        //Debug.DrawRay(transform.position + new Vector3(0,0, 0), transform.right, Color.red, _spriteRenderer.bounds.size.x);
        IsPlayCrouchAnim(IsCrouch());
        float horizontalspeed = Input.GetAxis("Horizontal");
        if(horizontalspeed!=0)
        {
            temp = horizontalspeed;
        }
        if(IsCanStop||IsCanStop0)
        {   if(temp>0)
            {
                if(Input.GetKey("a"))
                {
                    horizontalspeed = -1;
                }
                if (Input.GetKey("d"))
                {
                    horizontalspeed = 0;
                }
            }
            if (temp <0)
            {
                if (Input.GetKey("d"))
                {
                    horizontalspeed = 1;
                }
                if (Input.GetKey("a"))
                {
                    horizontalspeed = 0;
                }

            }

            
        }
        _rigbody2D.velocity = new Vector2(horizontalspeed*speed,_rigbody2D.velocity.y);
        if (horizontalspeed != 0)
        {
            if (horizontalspeed < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        if (Input.GetKey("a")||Input.GetKey("d"))
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }
    }
    void _jump()
    {   
        IsGround = Physics2D.OverlapCircle(checkground.position, 0.4f, ground);
        if (IsGround)
        {   
            extraTime = 1;
            animator.SetBool("IsJump", false);
           
        }
        if(Input.GetKeyDown("space") && extraTime>0)
        {
            _rigbody2D.velocity =  new Vector2(_rigbody2D.velocity.x, Force); 
            extraTime--;
            animator.SetBool("IsJump", true);
            
        }
        if(Input.GetKeyDown("space") && extraTime==0&&IsGround)
        {
            _rigbody2D.velocity  =new Vector2(_rigbody2D.velocity.x,  Force) ;
            animator.SetBool("IsJump", true);
            
        }
        
    }
    private void WriteCount()
    {
        while (IsGround &&Input.GetKeyDown("s"))
        {
            count++;
            break;
        }

    }
    private bool IsCrouch()
    {
        WriteCount();

        if (count == 1 && IsGround)
        {

            return true;
        }
        if (count > 1 || !IsGround)
        {
            count = 0;
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
    void Teleport()
    {if(Input.GetKey("a") || Input.GetKey("d"))
        {
            if (Input.GetKey("left shift") && SpingCount > 0)
            {
                IsSping = true;

            }

            if (IsSping && SpingTime <= MaxSpingTime && SpingCount > 0)
            {

                SpingTime += Time.deltaTime;
                _rigbody2D.velocity = (Vector2)transform.right * SpingSpeed + new Vector2(0, _rigbody2D.velocity.y);
                //transform.Translate(Vector2.right * SpingSpeed * Time.deltaTime);
                // _rigbody2D.MovePosition(transform.right*SpingSpeed * Time.deltaTime);
                //if (Input.GetAxis("Horizontal")<0)
                //{
                //SpingSpeed = -Mathf.Abs(SpingSpeed);

                //}

                //if (Input.GetAxis("Horizontal") >0)
                // {
                //SpingSpeed = Mathf.Abs(SpingSpeed);
                //}



            }
            if (SpingTime > MaxSpingTime)
            {
                SpingCount -= 1;
                IsSping = false;
            }
            if (!IsSping && SpingTime > MaxSpingTime)
            {

                SpingwaitTime += Time.deltaTime;
                if (SpingwaitTime > 0.5f)
                {
                    SpingCount = 1;
                    SpingTime = 0;
                    SpingwaitTime = 0;

                }
            }
        }
        
    }
}
