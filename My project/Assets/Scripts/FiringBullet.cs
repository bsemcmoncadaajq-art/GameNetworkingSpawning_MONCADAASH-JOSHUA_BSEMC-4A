using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : NetworkBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float spawnOffset = 1.5f;

    void Update()
    {
        if (!IsOwner) return;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RequestFireServerRpc();
        }
    }

    [ServerRpc]
    private void RequestFireServerRpc()
    {

        Vector3 spawnPos = firePoint != null
            ? firePoint.position
            : transform.position + (transform.forward * spawnOffset);

        Quaternion spawnRot = firePoint != null ? firePoint.rotation : transform.rotation;

        GameObject bulletInstance = Instantiate(bulletPrefab, spawnPos, spawnRot);
        bulletInstance.GetComponent<NetworkObject>().Spawn();
    }
}