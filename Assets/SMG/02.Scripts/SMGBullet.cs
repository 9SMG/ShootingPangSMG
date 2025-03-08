using UnityEngine;

/*
 * ProjectSetting > Physics2D > bounce Threshold 값을 1(기본값)에서 0.1로 변경
 */

public class SMGBullet : MonoBehaviour
{
    [Header("State")]
    public bool isStop = true;

    [Header("Test Settings")]
    public Vector2 hitTestPower = new Vector2(1f, 0);
    
    [Header("Audio Clips")]
    public AudioClip bounceSfx;



    Rigidbody2D rb;
    TrailVisible trailVisible;

    float stopSpeedThreshold = 0.0036f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailVisible = GetComponentInChildren<TrailVisible>();
    }

    private void FixedUpdate()
    {
        //int stack
        if (isStop)
            return;
        // 공 정지 판정 검사
        float currentSpeed = rb.linearVelocity.sqrMagnitude;

        //if (Mathf.Sqrt(rb.linearVelocityY * rb.linearVelocityY) < 1)
        //    Debug.Log("rb.linearVelocityY: " + Mathf.Sqrt(rb.linearVelocityY * rb.linearVelocityY));
        
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
    #region ContextMenu HitTest / HitTest10Times
    [ContextMenu("HitTest")]
    void HitTest()
    {
        HitBall(hitTestPower);
    }

    [ContextMenu("HitTest10Times")]
    void HitTest10Times()
    {
        for (int i = 0; i < 10; i++)
        {
            Invoke("HitTest", 6f * i);
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundsPlayer.Instance.PlaySFX(bounceSfx);
    }
}
