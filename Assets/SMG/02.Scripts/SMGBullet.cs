using UnityEngine;

/*
 * ProjectSetting > Physics2D > bounce Threshold ���� 1(�⺻��)���� 0.1�� ����
 */

public class SMGBullet : MonoBehaviour
{
    [Header("State")]
    public bool isStop = true;

    [Header("Test Settings")]
    public Vector2 hitTestPower = new Vector2(1f, 0);
    
    [Header("Audio Clips")]
    public AudioClip bounceSfx;

    Vector3 startPos;
    Rigidbody2D rb;
    TrailVisible trailVisible;

    float stopSpeedThreshold = 0.004f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailVisible = GetComponentInChildren<TrailVisible>();
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (isStop)
            return;
        
        // �� ���� ���� �˻�
        float currentSpeed = rb.linearVelocity.sqrMagnitude;

        if (currentSpeed < stopSpeedThreshold)
        {
            //Debug.Log("currentSpeed: " + currentSpeed);
            rb.linearVelocity = Vector2.zero;
            isStop = true;
        }
    }

    //���ϴ� ���ͷ� ���� ħ
    public void HitBall(Vector2 hit)
    {
        isStop = false;
        rb.AddForce(hit, ForceMode2D.Impulse);
        //rb.AddTorque(hit.x, ForceMode2D.Impulse);
    }
    #region ContextMenu Function
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
    [ContextMenu("Reset Position")]
    public void ResetToStartPos()
    {
        rb.linearVelocity = Vector2.zero;
        trailVisible.SetVisible(false);
        trailVisible.SetVisibleTimer(true, 0.1f);
        transform.position = startPos;
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundsPlayer.Instance.PlaySFX(bounceSfx);
    }

    

}
