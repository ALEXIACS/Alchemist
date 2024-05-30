using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMotor : MonoBehaviour
{
   private CharacterController controller;
   private Vector3 playerVelocity;
   private bool IsGrounded;
   public float speed = 5f;
   public float gravity = -9.8f;
   public float jumpHeight = 1.5f;

   void Start()
   {
    controller = GetComponent<CharacterController>();
   }
void Update()
{
   IsGrounded = controller.isGrounded;
}

   public void ProcessMove(Vector2 input)
   {
    Vector3 moveDirection = Vector3.zero;
    moveDirection.x = input.x;
    moveDirection.z = input.y;
    controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
    playerVelocity.y += gravity * Time.deltaTime;
    controller.Move(playerVelocity *Time.deltaTime);
    if(IsGrounded && playerVelocity.y < 0)
    playerVelocity.y = -2f;
    Debug.Log(playerVelocity.y);
   }
   public void Jump()
   {
      if(IsGrounded)
      {
         playerVelocity.y = Mathf.Sqrt(jumpHeight * -3F * gravity);
      }
   }
}
