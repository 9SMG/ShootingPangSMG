using UnityEngine;

public class BallController : MonoBehaviour
{
    Gage gage;

    [Header("State")]
    public bool isStop = true;
    public bool isUsed = true;
    public bool isEnd
    {
        get
        {
            return isStop && isUsed && !ability.isActive;
        }
    }

    float stopSpeedThreshold = 0.004f;

    [Header("Audio Clips")]
    public AudioClip bounceSfx;

    Vector3 startPos;
    Rigidbody2D rb;
    TrailVisible trailVisible;
    ItemImages itemImages;

    //Item
    //Magnetic magnetic;
    //KnockBack knockBack;
    //SMGExplosion smgExplosion;

    //int selectItemNum;

    BallAbility ability;

    float activeTime;


    [Header("Test Settings")]
    public Vector2 hitTestPower = new Vector2(1f, 0);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trailVisible = GetComponentInChildren<TrailVisible>();
        itemImages = GetComponentInChildren<ItemImages>();
        gage = GetComponentInChildren<Gage>();
    }

    float stopDeltaTime;
    private void Start()
    {
        startPos = transform.position;

        if (IsNearStopSpeed())
        {
            StopCompletely();
        }
        isUsed = true;
    }


    private void FixedUpdate()
    {
        if (!isUsed)
        {
            activeTime -= Time.fixedDeltaTime;
            if (activeTime <= 0)
                isUsed = true;
        }
        if (isStop)
            return;

        // 공 정지 판정 검사
        if (IsNearStopSpeed())
        {
            stopDeltaTime += Time.deltaTime;
            if(stopDeltaTime > 10f)
            {
                Debug.Log("Stop ball [currentSpeed: " + rb.linearVelocity.sqrMagnitude + "]");
                StopCompletely();
                UseItem();
            }
        }
    }

    bool IsNearStopSpeed()
    {
        return rb.linearVelocity.sqrMagnitude < stopSpeedThreshold;
    }

    void StopCompletely()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        isStop = true;
    }

    public void HitBall(Vector2 hit)
    {
        stopDeltaTime = 0f;
        isStop = false;
        rb.AddForce(hit, ForceMode2D.Impulse);
        //rb.AddTorque(hit.x, ForceMode2D.Impulse);
    }

    public void UseItem()
    {
        //switch (selectItemNum)
        //{
        //    case 0:
        //        activeTime = 0.5f;
        //        smgExplosion.Explosion();
        //        break;
        //    case 1:
        //        activeTime = 0.5f;
        //        knockBack.Push();
        //        break;
        //    case 2:
        //        activeTime = 2.5f;
        //        magnetic.Pull();
        //        break;
        //    default:
        //        activeTime = 0f;
        //        break;
        //}
        isUsed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.Instance.PlaySFX(bounceSfx);
    }

    [ContextMenu("Reset Position")]
    public void ResetToStartPos()
    {
        trailVisible.SetVisible(false);
        trailVisible.SetVisibleTimer(true, 0.1f);
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPos;
        //SelectItem(-1);// Item.NoItem);
    }

    #region Only ContextMenu Function
    [ContextMenu("HitTest")]
    void HitTest()
    {
        HitBall(hitTestPower);
    }

    [ContextMenu("HitTest 10 Times")]
    void HitTest10Times()
    {
        for (int i = 0; i < 10; i++)
        {
            Invoke("HitTest", 6f * i);
        }
    }

    
    #endregion
}
