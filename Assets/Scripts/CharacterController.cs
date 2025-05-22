using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterData characterData;

    [SerializeField]
    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction jumpAction;

    private void Start()
    {
        moveAction     = playerInput.actions.FindAction("Move");
        interactAction = playerInput.actions.FindAction("Interact");
        jumpAction     = playerInput.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        // if (interactAction.WasPressedThisFrame()) Debug.Log($"interact");
        // if (jumpAction.WasPressedThisFrame()) Debug.Log($"Jump");

        var moveVector2 = moveAction.ReadValue<Vector2>();
        var direction   = new Vector3(moveVector2.x , moveVector2.y , 0); // 移動方向
        // Time.deltaTime = 1/fps 抵銷FPS的影響
        transform.position += direction * characterData.moveSpeed * Time.deltaTime;
    }
}