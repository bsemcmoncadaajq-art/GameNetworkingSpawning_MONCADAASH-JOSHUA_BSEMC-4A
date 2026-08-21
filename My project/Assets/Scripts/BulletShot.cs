using Unity.Netcode;
using UnityEngine;

public class BulletShot : NetworkBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;

    void Start()
    {

        if (IsServer)
        {
            Destroy(gameObject, lifetime);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
}
