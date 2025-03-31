using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SMGGameManager : MonoBehaviour
{
    public static SMGGameManager Instance;
    public SMGBullet MyBullet;

    public StageSettings[] stages;
    int currStagesIdx;

    

    bool isShooting;
    bool isDragable = true;

    Item selectedItem;
    
    int[] itemCnt = new int[4];
    int targetCoinCnt;
    int currBallCnt;
    Vector3 startPos;
    int cameraSize;

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
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    public bool AllBallUsed
    {
        // ��� ���� �����°�?
        get
        {
            foreach(GameObject _bullet in GameObject.FindGameObjectsWithTag(TagManager.tagBullet))
            {
                if (_bullet.GetComponent<SMGBullet>().isUsed == false)
                    return false;
            }

            return true;
        }
    }

    public bool AllBallStop
    {
        // ��� ���� ����°�?
        get
        {
            foreach (GameObject _bullet in GameObject.FindGameObjectsWithTag(TagManager.tagBullet))
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
        selectedItem = Item.NoItem;



        Invoke("InitStart", 0.2f);
    }

    void InitStart()
    {
        ResetStage(0);
    }

    void ResetStage(int idx)
    {
        if (!(idx >= 0 && idx < stages.Length))
            return;
        currStagesIdx = idx;

        // Active Stage Object
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].gameObject.SetActive((i == currStagesIdx));
        }

        // Get Stage Setting
        for(int i = 0; i < 4; i++)
        {
            itemCnt[i] = stages[currStagesIdx].GetItemCount(i);
        }

        currBallCnt = stages[currStagesIdx].GetBulletNum();
        startPos = stages[currStagesIdx].GetStartPos();
        Camera.main.orthographicSize = stages[currStagesIdx].GetCameraSize();

        MyBullet.SetStartPos(startPos);
        MyBullet.ResetToStartPos();


        UIManager.Instance.SetEnableBall(currBallCnt);
        UIManager.Instance.SetTargetCoinCount(GetRemainingCoinCount());
        for (int i = 0; i < 3; i++)
        {
            UIManager.Instance.SetItemCnt(i, itemCnt[i]);
        }
    }

    public void ResetCurrStage()
    {
        ResetStage(currStagesIdx);
    }
    public void ResetNextStage()
    {
        ResetStage(++currStagesIdx);
    }


    void Update()
    {
        if(GetRemainingCoinCount() == 0)
        {
            //Debug.Log("Game Clear");
            //isDragable = false;
            //ResetStage(++currStagesIdx);
            //ResetNextStage();
            UIManager.Instance.ShowResultPanel("Stage Clear", (currStagesIdx + 1 < stages.Length));
        }

        // ��� ���� ������ ��, �� ī��Ʈ�� �ϳ� ���̰� ������ġ�� �� ��ġ
        if (isShooting && AllBallStop && AllBallUsed)
        {
            isShooting = false;


            // ���� ���� ���� ��
            if (currBallCnt == 0)
            {
                //Debug.Log("Game Over");
                UIManager.Instance.ShowResultPanel("Game Over", false);
            }
            else
            {
                // ���� �� ��ġ
                MyBullet.ResetToStartPos();
                //��Ż �ʱ�ȭ
                foreach (MasterPortal _PortalMaster in stages[currStagesIdx].GetComponentsInChildren<MasterPortal>())
                {
                    _PortalMaster.ResetIDList();
                }

                isDragable = true;
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
        Coin[] remaingCoins = stages[currStagesIdx].GetComponentsInChildren<Coin>();
        return remaingCoins.Length;
    }

    // ���� �� ����
    int GetRemainingBallCount()
    {
        return currBallCnt;
    }

    public void HitBullet(Vector2 hit)
    {
        isDragable = false;
        MyBullet.HitBall(hit);
        isShooting = true;
        currBallCnt--;
        if (!((int)selectedItem < 0 || (int)selectedItem > 2))
        {
            itemCnt[(int)selectedItem]--;
            UIManager.Instance.OnBallUsed((int)selectedItem, itemCnt[(int)selectedItem]);
        }
    }

    public bool GetDragable()
    {
        return isDragable;
    }

    public void PostItemToBullet(Item item)
    {
        selectedItem = item;
        MyBullet.SelectItem(selectedItem);
    }
}
