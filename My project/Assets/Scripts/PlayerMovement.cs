using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();


        if (!IsOwner)
        {
            controller.enabled = false;
        }
    }

    private void Update()
    {

        if (!IsOwner) return;

        float x = 0f;
        float z = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) z += 1f;
            if (Keyboard.current.sKey.isPressed) z -= 1f;
            if (Keyboard.current.dKey.isPressed) x += 1f;
            if (Keyboard.current.aKey.isPressed) x -= 1f;
        }

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        controller.Move(move * Time.deltaTime);
    }
}