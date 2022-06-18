using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    private Rigidbody2D rigidbodyButton;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidbodyButton = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        if (InputController.WasKeyPressed(KeyCode.Space))
        {
            transform.localScale -= 0.01f * Vector3.one;
            spriteRenderer.color = Color.magenta;
        }
        if (InputController.WasKeyReleased(KeyCode.Space))
        {
            transform.localScale += 0.01f * Vector3.one;
            spriteRenderer.color = Color.white;
        }
    }

    public void ImpulseMove(Vector2 direction, float impulsePower = 1f)
    {
        rigidbodyButton.AddForce(direction * impulsePower, ForceMode2D.Impulse);
    }
}
