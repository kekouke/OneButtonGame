using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform Target;

    private Vector3 velocity = Vector3.zero;


    private void FixedUpdate()
    {
        var targetPosition = Target.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.25f);
    }
}
