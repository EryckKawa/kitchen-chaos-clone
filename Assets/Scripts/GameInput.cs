using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public Vector2 GetMovementVectorNormalized()
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

        return inputSystem;
    }
}
