using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    BallController playerBall;

    // Stage
    public StageSettings[] stages;
    Vector3 startPos;
    int currStagesIdx = 0;
    int currBallCnt;


    bool isShooting;
    bool isDragable = true;

    

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
#if USE_UI
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
#endif

        playerBall = FindAnyObjectByType<BallController>();
    }

    private void Start()
    {
        Invoke("InitStart", 0.5f);
    }

    void InitStart()
    {
        ResetStage(0);
    }

    // 모든 공이 끝났는가?
    public bool AllBallUsed
    {
        get
        {
            foreach (GameObject _bullet in GameObject.FindGameObjectsWithTag(TagManager.tagBall))
            {
                if (_bullet.GetComponent<BallController>().isUsed == false)
                    return false;
            }

            return true;
        }
    }

    // 모든 공이 멈췄는가?
    public bool AllBallStop
    {
        get
        {
            foreach (GameObject _bullet in GameObject.FindGameObjectsWithTag(TagManager.tagBall))
            {
                if (_bullet.GetComponent<BallController>().isStop == false)
                    return false;
            }

            return true;
        }
    }

    public  bool AllBallEnd
    {
        get
        {
            foreach (GameObject _bullet in GameObject.FindGameObjectsWithTag(TagManager.tagBall))
            {
                if (_bullet.GetComponent<BallController>().isEnd == false)
                    return false;
            }

            return true;
        }
    }

    void Update()
    {
        if (GetRemainingCoinCount() == 0)
        {
            Debug.Log("Game Clear");
            //isDragable = false;
            //ResetStage(++currStagesIdx);
            ResetNextStage();
#if USE_UI
            UIManager.Instance.ShowResultPanel("Stage Clear", (currStagesIdx + 1 < stages.Length));
#endif
        }
        // 모든 공이 멈췄을 때, 공 카운트를 하나 줄이고 시작위치에 공 배치
        if (isShooting && AllBallEnd)//AllBallStop && AllBallUsed)
        {
            isShooting = false;


            // 남은 공이 없을 때
            if (currBallCnt == 0)
            {
#if USE_UI
                UIManager.Instance.ShowResultPanel("Game Over", false);
#else
                Debug.Log("Game Over");
#endif
            }
            else
            {
                Debug.Log("Ball End, Ball Reset");
                // 시작 공 배치
                playerBall.ResetToStartPos();

                foreach (GrapableBlock grapableBlock in FindObjectsByType<GrapableBlock>(FindObjectsSortMode.None))
                {
                    grapableBlock.RespawnBlock();
                }
                //포탈 초기화
                //foreach (MasterPortal _PortalMaster in stages[currStagesIdx].GetComponentsInChildren<MasterPortal>())
                //{
                //    _PortalMaster.ResetIDList();
                //}

                isDragable = true;
            }
        }
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
        for (int i = 0; i < 4; i++)
        {
            //itemCnt[i] = stages[currStagesIdx].GetItemCount(i);
        }

        currBallCnt = stages[currStagesIdx].GetBulletNum();
        startPos = stages[currStagesIdx].GetStartPos();
        Camera.main.orthographicSize = stages[currStagesIdx].GetCameraSize();

        playerBall.SetStartPos(startPos);
        playerBall.ResetToStartPos();

#if USE_UI
        UIManager.Instance.SetEnableBall(currBallCnt);
        UIManager.Instance.SetTargetCoinCount(GetRemainingCoinCount());
        for (int i = 0; i < 3; i++)
        {
            UIManager.Instance.SetItemCnt(i, itemCnt[i]);
        }
#endif
    }

    public void ResetNextStage()
    {
        //if(currStagesIdx + 1 >)
        ResetStage(++currStagesIdx);
    }

    public bool GetDragable()
    {
        return isDragable;
    }
    int GetRemainingCoinCount()
    {
        Coin[] remaingCoins = stages[currStagesIdx].GetComponentsInChildren<Coin>();
        return remaingCoins.Length;
    }

    public void HitBall(Vector2 hit)
    {
        isDragable = false;
        playerBall.HitBall(hit);
        isShooting = true;
        currBallCnt--;
//        if (!((int)selectedItem < 0 || (int)selectedItem > 2))
//        {
//            itemCnt[(int)selectedItem]--;
//#if USE_UI
//            UIManager.Instance.OnBallUsed((int)selectedItem, itemCnt[(int)selectedItem]);
//#endif
//        }
    }
}
