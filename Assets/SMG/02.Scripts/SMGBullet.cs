using UnityEngine;

public class SMGBullet : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("State")]
    public bool isStop = true;

    [Header("Settings")]
    public Vector2 hitTestPower = new Vector2(1f, 0);
    public float stopSpeedThreshold;

    [Header("Audio Clips")]
    public AudioClip bounceSfx;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        for(int i = 0; i < 10; i++)
        {
            Invoke("HitTest", 7f * i);
        }
    }

    private void FixedUpdate()
    {

        // 공 정지 판정 검사
        float currentSpeed = rb.linearVelocity.sqrMagnitude;
        //Debug.Log("currentSpeed: " + currentSpeed);
        if (currentSpeed < stopSpeedThreshold)
        {
            rb.linearVelocity = Vector2.zero;
            isStop = true;
        }
    }

    //원하는 벡터로 공을 침
    public void HitBall(Vector2 hit)
    {
        isStop = false;
        //Vector2 power;
        //rb.AddForce(hit);
        rb.AddForce(hit, ForceMode2D.Impulse);
    }

    [ContextMenu("HitTest")]
    void HitTest()
    {
        HitBall(hitTestPower);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //PlayOnShot(bounceSfx);
    }
}
