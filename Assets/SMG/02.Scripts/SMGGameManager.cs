//using Unity.VisualScripting;
using UnityEngine;

public class SMGGameManager : MonoBehaviour
{
    public static SMGGameManager Instance;
    public SMGBullet MyBullet;

    StageSettings stage;

    int currBallCount;

    bool isShooting;
    bool isDragable;

    //public bool isDragable
    //{
    //    get
    //    {
    //        return isDragable;
    //    }

    //    private set
    //    {
    //        isDragable = value;
    //    }
    //}

    //public SMGGameManager()
    //{
    //    this.isDragable = true;
    //}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    public bool AllBallStop
    {
        // ��� ���� ����°�?
        get
        {
            foreach(GameObject _bullet in GameObject.FindGameObjectsWithTag(TagManager.tagBullet))
            {
                if (_bullet.GetComponent<SMGBullet>().isStop == false)
                    return false;
            }

            return true;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stage = GameObject.Find("Stage0").GetComponent<StageSettings>();

        currBallCount = 5;
    }

    float TestDeltaTime;
    // Update is called once per frame
    void Update()
    {
        if(GetRemainingCoinCount() == 0)
        {
            //Debug.Log("Game Clear");
            isDragable = false;
        }

        //TestDeltaTime += Time.deltaTime;
        //if(TestDeltaTime >5f)
        //{
        //    TestDeltaTime = 0f;
        //    isDragable = !isDragable;
        //}

        // ��� ���� ������ ��, �� ī��Ʈ�� �ϳ� ���̰� ������ġ�� �� ��ġ
        if (isShooting && AllBallStop)
        {
            isShooting = false;
            currBallCount--;

            // ���� ���� ���� ��
            if(currBallCount == 0)
            {
                Debug.Log("Game Over");
            }


            // ���� �� ��ġ
            MyBullet.ResetToStartPos();

            //��Ż �ʱ�ȭ
            foreach(MasterPortal _PortalMaster in stage.GetComponentsInChildren<MasterPortal>())
            {
                _PortalMaster.ResetIDList();
            }
        }
    }

    #region ContextMenu Function

    [ContextMenu("GetRemainingCoinCount()")]
    void LogRemainingCoinCount()
    {
        Debug.Log("GetRemainingCoinCount(): " + GetRemainingCoinCount());
    }
    #endregion

    // �������� ��, ���� ���� ����
    int GetRemainingCoinCount()
    {
        Coin[] remaingCoins = stage.GetComponentsInChildren<Coin>();
        return remaingCoins.Length;
    }

    // ���� �� ����
    int GetRemainingBallCount()
    {
        return currBallCount;
    }

    public void HitBullet(Vector2 hit)
    {
        MyBullet.HitBall(hit);
        isShooting = true;
    }

    public bool GetDragable()
    {
        return isDragable;
    }

    public void PostItemToBullet(Item item)
    {
        MyBullet.SelectItem(item);
    }
}
