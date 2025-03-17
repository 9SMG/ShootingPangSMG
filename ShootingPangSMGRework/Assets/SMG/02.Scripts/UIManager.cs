using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Canvas uiCanvas;

    public TextMeshProUGUI stageNumberText;
    public TextMeshProUGUI targetCoinText;

    ItemPicker[] itemPickers;
    BallBag ballBag;


    [Header("Stage Canvas")]
    public GameObject ResultPanel;
    public TextMeshProUGUI textHeader;
    public Button buttonNextStage;
    public Button buttonRetry;

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

        itemPickers = uiCanvas.GetComponentsInChildren<ItemPicker>();
        ballBag = uiCanvas.GetComponentInChildren<BallBag>();
    }

    private void Start()
    {
        for(int i = 0; i < itemPickers.Length; i++)
        {
        //    //itemPickers[i].pickButton.onClick.AddListener(() => ClickItemPick(i));
            itemPickers[i].SetItem(i);
        }
    }

    public void HideResultPanel()
    {
        ResultPanel.SetActive(false);
    }

    public void ShowResultPanel(string header, bool hasNextStage)
    {
        ResultPanel.SetActive(true);
        textHeader.text = header;
        buttonNextStage.gameObject.SetActive(hasNextStage);
    }

    public void ClickNextStage()
    {
        HideResultPanel();
        GameManager.Instance.ResetNextStage();
    }

    public void ClickRetry()
    {
        HideResultPanel();
        GameManager.Instance.ResetCurrStage();
    }

    public void ClickBackToMain()
    {

    }

    void ItemPick(int item)//Item item)
    {
        for (int i = 0; i < itemPickers.Length; i++)
        {
            itemPickers[i].ItemPick((i == (int)item) ? true : false);
        }
    }

    int selectItem;
    public void ClickItemPick(int item)
    {
        selectItem = item;

        ItemPick(item);
        SetCurrentBallState(item);
        SMGGameManager.Instance.PostItemToBullet(item);
    }

    public void SetStageNumber(int main, int sub)
    {
        stageNumberText.text = main.ToString() + " - " + sub.ToString();
    }

    public void SetTargetCoinCount(int count)
    {
        targetCoinText.text = count.ToString();
    }

    public void SetCurrentBallState(int item)//Item item)
    {
        ballBag.SetCurrBallItem(item);
    }

    public void OnBallUsed(int usedItem, int nowCount)
    {
        if (usedItem >= 0)
            itemPickers[usedItem].SetCount(nowCount);
        ballBag.SetNextBall();
    }

    [ContextMenu("TestShoot")]
    void TestShoot()
    {
        //if(selectItem >= 0)
        //    itemPickers[selectItem].SetCount(--itemCounts[selectItem]);
        //ballBag.SetNextBall();
        //selectItem = -1;
    }

    public void SetEnableBall(int cnt)
    {
        ballBag.ResetEnableBall(cnt);
    }

    public void SetItemCnt(int item, int cnt)
    {
        itemPickers[item].SetCount(cnt);
    }
}
