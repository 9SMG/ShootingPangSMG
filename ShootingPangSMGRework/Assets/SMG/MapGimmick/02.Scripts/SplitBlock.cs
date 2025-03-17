using UnityEngine;

public class SplitBlock : MonoBehaviour
{
    //public float power = 100f;

    public GameObject outA;
    public GameObject outB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //위치 이동
        collision.gameObject.transform.position = transform.position;

        //방향 조정
        float rad = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(-Mathf.Sin(rad), Mathf.Cos(rad));

        float power = collision.GetComponent<Rigidbody2D>().linearVelocity.magnitude;
        collision.GetComponent<Rigidbody2D>().linearVelocity = power * dir;
    }
}
