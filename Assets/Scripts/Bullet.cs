using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField] GameObject BulletContainer;

    private float initialScale = 2f;
    private float maxScale = 15f - 2f;

    public bool IsActiveObject = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetScale(float scale)
    {
        transform.localScale = (initialScale + scale * maxScale) * Vector2.one;
    }

    public void Push(Vector3 movement)
    {
        rigidbody.AddForce(movement, ForceMode2D.Impulse);
    }
}
