using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    BallController playerBall;

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

    // ��� ���� �����°�?
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

    // ��� ���� ����°�?
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
                if (_bullet.GetComponent<BallController>().isUsed == false)
                    return false;
            }

            return true;
        }
    }


    void Update()
    {
        if (false)//GetRemainingCoinCount() == 0)
        {
            //Debug.Log("Game Clear");
            //isDragable = false;
            //ResetStage(++currStagesIdx);
            //ResetNextStage();
#if USE_UI
            UIManager.Instance.ShowResultPanel("Stage Clear", (currStagesIdx + 1 < stages.Length));
#endif
        }
        // ��� ���� ������ ��, �� ī��Ʈ�� �ϳ� ���̰� ������ġ�� �� ��ġ
        if (isShooting && AllBallStop && AllBallUsed)
        {
            isShooting = false;


            // ���� ���� ���� ��
            if (false)//currBallCnt == 0)
            {
#if USE_UI
                UIManager.Instance.ShowResultPanel("Game Over", false);
#else
                Debug.Log("Game Over");
#endif
            }
            else
            {
                // ���� �� ��ġ
                playerBall.ResetToStartPos();
                //��Ż �ʱ�ȭ
                //foreach (MasterPortal _PortalMaster in stages[currStagesIdx].GetComponentsInChildren<MasterPortal>())
                //{
                //    _PortalMaster.ResetIDList();
                //}

                isDragable = true;
            }
        }
    }
    public bool GetDragable()
    {
        return isDragable;
    }
    public void HitBall(Vector2 hit)
    {
        isDragable = false;
        playerBall.HitBall(hit);
        isShooting = true;
        //currBallCnt--;
//        if (!((int)selectedItem < 0 || (int)selectedItem > 2))
//        {
//            itemCnt[(int)selectedItem]--;
//#if USE_UI
//            UIManager.Instance.OnBallUsed((int)selectedItem, itemCnt[(int)selectedItem]);
//#endif
//        }
    }
}
