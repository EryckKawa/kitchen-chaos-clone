using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movSpeed;

    private bool isWalking;

    private void Update()
    {
        Vector2 inputSystem = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputSystem.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputSystem.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputSystem.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputSystem.x += 1;
        }

        inputSystem = inputSystem.normalized;

        Vector3 movDir = new Vector3(inputSystem.x, 0f, inputSystem.y);
        transform.position += movSpeed * Time.deltaTime * movDir;

        isWalking = movDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
