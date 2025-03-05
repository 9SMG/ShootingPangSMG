using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] CircleCollider2D _circleColl;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Block"))
        {
            print("����!");
            _circleColl.enabled = true;
        }
    }
}
