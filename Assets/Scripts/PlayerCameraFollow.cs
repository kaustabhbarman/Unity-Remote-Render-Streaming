using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 positionOffset = new Vector3(0, 5, -8);

    [SerializeField]
    private Vector3 rotationOffset = new Vector3(20, 0, 0);

    void LateUpdate()
    {
        transform.position = player.position + positionOffset;
    }
}
