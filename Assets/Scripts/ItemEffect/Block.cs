using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    Rigidbody2D _rigid2D;

    private void Start()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rigid2D.linearVelocity != Vector2.zero)
        {
            _rigid2D.linearVelocity -= (_rigid2D.linearVelocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Explosion") && _rigid2D.bodyType == RigidbodyType2D.Dynamic)
        {
            Destroy(gameObject);
        }
    }
}
