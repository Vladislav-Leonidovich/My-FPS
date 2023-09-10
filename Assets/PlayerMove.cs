using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private float gravity = -9.81f;
    private float jumpHeight = 2f;
    Vector3 velocity;

    public CharacterController controller;

    public Transform groundCheck;
    private float groundDistanse = 0.4f;
    public LayerMask groundMask;
    

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistanse, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Рассчитываем движение вперед и назад (по вертикали)
        Vector3 moveDirection = transform.forward * verticalInput;

        // Рассчитываем движение влево и вправо (по горизонтали)
        Vector3 strafeDirection = transform.right * horizontalInput;

        // Суммируем оба направления
        Vector3 finalMoveDirection = (moveDirection + strafeDirection).normalized;

        // Применяем движение
        controller.Move(finalMoveDirection * moveSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
