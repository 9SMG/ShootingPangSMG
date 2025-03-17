using UnityEngine;

/*
 * ProjectSetting > Physics2D > bounce Threshold ���� 1(�⺻��)���� 0.1�� ����
 */

public class SMGBullet : MonoBehaviour
{
    [Header("State")]
    public bool isStop = true;
    public bool isUsed = true;

    [Header("Test Settings")]
    public Vector2 hitTestPower = new Vector2(1f, 0);
    
    [Header("Audio Clips")]
    public AudioClip bounceSfx;

    Vector3 startPos;
    Rigidbody2D rb;
    TrailVisible trailVisible;
    ItemImages itemImages;

    //Item
    Magnetic magnetic;
    KnockBack knockBack;
    SMGExplosion smgExplosion;

    int selectItemNum;

    float stopSpeedThreshold = 0.004f;

    private void Awake()
    {
        magnetic = GetComponent<Magnetic>();
        knockBack = GetComponent<KnockBack>();
        smgExplosion = GetComponent<SMGExplosion>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailVisible = GetComponentInChildren<TrailVisible>();
        startPos = transform.position;
        itemImages = GetComponentInChildren<ItemImages>();
        
    }

    private void FixedUpdate()
    {
        if(!isUsed)
        {
            activeTime -= Time.fixedDeltaTime;
            if (activeTime <= 0)
                isUsed = true;
        }
        if (isStop)
            return;
        
        // �� ���� ���� �˻�
        float currentSpeed = rb.linearVelocity.sqrMagnitude;

        if (currentSpeed < stopSpeedThreshold)
        {
            Debug.Log("currentSpeed: " + currentSpeed);
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            isStop = true;
            UseItem();
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
        trailVisible.SetVisible(false);
        trailVisible.SetVisibleTimer(true, 0.1f);
        //trailVisible.SetVisible(false);
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = startPos;
        //trailVisible.SetVisible(true);
        SelectItem(-1);// Item.NoItem);
    }
    #endregion

    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
        //SMGPlayerController.Instance.transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundsPlayer.Instance.PlaySFX(bounceSfx);
    }

    public void SelectItem(int item)
    {
        selectItemNum = item;
        itemImages.SetItemImage((int)selectItemNum);
    }

    float activeTime;
    public void UseItem()
    {
        switch(selectItemNum)
        {
            case 0:
                activeTime = 0.5f;
                smgExplosion.Explosion();
                break;
            case 1:
                activeTime = 0.5f;
                knockBack.Push();
                break;
            case 2:
                activeTime = 2.5f;
                magnetic.Pull();
                break;
            default:
                activeTime = 0f;
                break;
        }
        isUsed = false;
    }
}
