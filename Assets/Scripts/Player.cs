using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MoveSpeed;
    private float XInput;
  

    void Start()
    {
        
    }

   
    void Update()
    {
       
        XInput = Input.GetAxis("Horizontal");
        //获得水平轴的输入值，并将其赋值给XInput变量。这将用于控制角色的左右移动。
        //水平输入值在-1到1之间，表示向左或向右移动。
        rb.velocity = new Vector2(MoveSpeed * XInput,rb.velocity.y);
       if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x,4);
        }
    }
}
