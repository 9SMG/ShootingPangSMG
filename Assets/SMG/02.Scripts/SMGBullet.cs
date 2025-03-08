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
    }

    private void FixedUpdate()
    {
        if (isStop)
            return;
        // 공 정지 판정 검사
        float currentSpeed = rb.linearVelocity.sqrMagnitude;
        
        if (currentSpeed < stopSpeedThreshold)
        {
            Debug.Log("currentSpeed: " + currentSpeed);
            rb.linearVelocity = Vector2.zero;
            isStop = true;
        }
    }

    //원하는 벡터로 공을 침
    public void HitBall(Vector2 hit)
    {
        isStop = false;
        rb.AddForce(hit, ForceMode2D.Impulse);
        //rb.AddTorque(hit.x, ForceMode2D.Impulse);
    }

    [ContextMenu("HitTest")]
    void HitTest()
    {
        HitBall(hitTestPower);
    }

    [ContextMenu("HitTest10times")]
    void HitTest10times()
    {
        for (int i = 0; i < 10; i++)
        {
            Invoke("HitTest", 6f * i);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundsPlayer.Instance.PlaySFX(bounceSfx);
    }
}
