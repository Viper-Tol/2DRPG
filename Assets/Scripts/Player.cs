using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MoveSpeed;
    public float JumpForce;
    private float XInput;

    private Animator animator;

    private int facingDir=1;
    private bool facingRight = true;
    
    [SerializeField]private bool IsMoving;

    [Header("Dash info")]
    [SerializeField]private float dashSpeed; //速度

    [SerializeField]private float dashDuration; //持续时间
    private float dashTime; //冷却时间
    [SerializeField]private float dashCooldown; //冷却时间
    private float dashCooldownTimer; //冷却时间计时器

    [Header("Attack info")]
    private bool isAttacking;
    private int comboCounter;







    [Header("Ground Check")]
    [SerializeField]private float groundCheckDistance; //地面检测距离
    [SerializeField]private LayerMask whatIsGround; //地面层掩码
    private bool isGrounded;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
    {
        Debug.LogError("Animator component not found on this GameObject or its children.");
    }

    if (rb == null)
    {
        Debug.LogError("Rigidbody2D component not assigned.");
    }
    }

   
    void Update()
    {
        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime; //冷却时间计时器递减
        Movment();
        CheckInput();
        CollisionChecks();
        FlipController();
        AnimatorController();//调用AnimatorController方法来控制动画。
    }
    private void AnimatorController()
    {
        IsMoving = rb.velocity.x != 0;
        animator.SetFloat("yVelocity",rb.velocity.y);
        animator.SetBool("IsMoving", IsMoving);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsDashing", dashTime > 0); //设置是否处于Dash状态。
        animator.SetBool("IsAttacking",isAttacking);
        animator.SetInteger("comboCounter", comboCounter); //设置comboCounter的值。

        
    }

    private void Jump()
    {
        if(isGrounded){
        rb.velocity = new Vector2(rb.velocity.x,JumpForce);}
    }

    private void Movment()
    {
        if(dashTime > 0)
        {
            rb.velocity = new Vector2(dashSpeed * XInput, 0); //在Dash状态下，角色的移动速度不受输入影响。

        }else{
        rb.velocity = new Vector2(MoveSpeed * XInput,rb.velocity.y);
    }}

    private void CheckInput()
    {
        XInput = Input.GetAxis("Horizontal");
        //获得水平轴的输入值，并将其赋值给XInput变量。这将用于控制角色的左右移动。
        //水平输入值在-1到1之间，表示向左或向右移动。
      if(Input.GetKeyDown(KeyCode.Mouse0)){
        isAttacking = true;
      }

       if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer < 0)
        {
            DashAbilty();
        }

    }
    private void Flip()
    {
        facingDir = facingDir * -1; //翻转方向，例如从1变为-1或从-1变为1。这将用于改变角色的朝向。
        facingRight = !facingRight; //切换面向方向，例如从true变为false或从false变为true。这将用于确定角色是向左还是向右移动。

        transform.Rotate(0,180,0);//将角色的旋转角度绕Y轴旋转180度，从而实现角色的翻转。
    }

    private void FlipController()
    {
        if(rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if(rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
     
    }
    private void OnDrawGizmos()
    {
        //绘制地面检测范围
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x ,transform.position.y - groundCheckDistance));
    }
    //碰撞检测,用于检测人物是否在地面上，通过一条射线
    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);//Raycast(起点，方向，距离，检测的物体)

    }
    private void DashAbilty()
    {
        dashCooldownTimer = dashCooldown; //重置冷却时间计时器

        dashTime = dashDuration; //重置冷却时间
    }
    public void AttackOver()
    {
        isAttacking = false; //重置攻击状态
    }


    }
