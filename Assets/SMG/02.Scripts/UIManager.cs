using TMPro;
using UnityEngine;
//using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Canvas uiCanvas;

    public TextMeshProUGUI stageNumberText;
    public TextMeshProUGUI targetCoinText;

    //List<ItemPicker> itemPikers = new List<ItemPicker>();
    //[SerializeField]
    ItemPicker[] itemPickers;
    BallBag ballBag;
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

    int[] itemCounts = {2, 3, 4};
    private void Start()
    {
        itemPickers = uiCanvas.GetComponentsInChildren<ItemPicker>();
        ballBag = uiCanvas.GetComponentInChildren<BallBag>();

        for(int i = 0; i < itemPickers.Length; i++)
        {
            //itemPickers[i].pickButton.onClick.AddListener(() => ClickItemPick(i));
            itemPickers[i].SetCount(itemCounts[i]);
            itemPickers[i].SetItem((Item)i);
        }
    }


    public void ClickNextStage()
    {

    }

    public void ClickRetry()
    {

    }

    public void ClickBackToMain()
    {

    }

    void ItemPick(Item item)
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

        ItemPick((Item)item);
        SetCurrentBallState((Item)item);
        //SMGGameManager.Instance.PostItemToBullet((Item)item);
    }


    public void SetStageNumber(int main, int sub)
    {
        stageNumberText.text = main.ToString() + " - " + sub.ToString();
    }

    public void SetTargetCoinCount(int count)
    {
        targetCoinText.text = count.ToString();
    }

    public void SetCurrentBallState(Item item)
    {
        ballBag.SetCurrBallItem(item);
    }

    [ContextMenu("TestShoot")]
    void TestShoot()
    {
        //Game Manger
        //Item selectItem = Item.Bomb;
        if(selectItem >= 0)
            itemPickers[selectItem].SetCount(--itemCounts[selectItem]);
        //itemPickers[(int)item]
        //picker
        //ballbag
        ballBag.SetNextBall();
        selectItem = -1;
    }


}
