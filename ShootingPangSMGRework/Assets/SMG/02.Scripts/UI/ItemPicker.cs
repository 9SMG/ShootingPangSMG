using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPicker : MonoBehaviour
{
    //Item itemNum;
    int itemCount = 0;

    public Button pickButton;
    ItemImages itemImages;
    TextMeshProUGUI itemCountText;
    //Image block;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pickButton = GetComponentInChildren<Button>();
        itemImages = GetComponentInChildren<ItemImages>();
        itemCountText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void SetItem(int item)//Item item)
    {
        //itemNum = item;
        itemImages.SetItemImage((int)item);
    }

    public void SetCount(int count)
    {
        itemCount = count;
        SetCount(count, false);
    }

    void SetCount(int count, bool preview)
    {
        if (preview)
        {
            itemCountText.color = Color.blue;
        }
        else if (count == 0)
        {
            itemCountText.color = Color.red;
        }
        else
        {
            itemCountText.color = Color.black;
        }

        itemCountText.text = count.ToString();
        pickButton.interactable = (!preview && count == 0) ? false : true;
    }


    public void ItemPick(bool pick)
    {
        SetCount(pick ? itemCount - 1 : itemCount, pick);
    }
}
