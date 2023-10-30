using UnityEngine;


public class CameraFollower : MonoBehaviour
{
    [SerializeField] Vector3 StartOffset;

    public void InitPosition(Vector3 position)
    {
        transform.position = position + StartOffset;
    }

    public void SetPosition(Vector3 position)
    {
        var newPosition = transform.position;
        newPosition.x = position.x;
        transform.position = newPosition;
    }
}
