using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public enum Item
{
    NoItem = -1,
    KnockBack = 0,        // ��ġ��: ���� ���� �� ������ ��ġ��
    Bomb,               // ��ź: ���� ���� �� ������ ȹ��
    Magnet,             // �ڼ�: ���� ���� �� ������ ����
    
    Imotal,             // ����: Ư�� ��� ȸ��
    Cleaner,             // ������: Ư�� ��� ȸ��
    ZeroGravity      // ���߷�: Ư�� ��� ȸ��
}


public class ItemController : MonoBehaviour
{
    public static ItemController Instance { get; private set; }

    [SerializeField] private List<Image> itemImage;
    [SerializeField] public int[] items;

    private string[] itemStr = { "Bomb", "Magnet", "KnockBack", "Imotal" };
            // , "Cleaner", "ZeroGravity"};


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ShowItem();

        Item test = Item.Bomb;
        switch(test)
        {
            case Item.Bomb:

                break;
        }
    }

    public void ShowItem()
    {
        // Function on Image
        for (int i = 0; i < itemImage.Count; i++)
        {
            TextMeshProUGUI imageText = itemImage[i].GetComponentInChildren<TextMeshProUGUI>();
            if (imageText != null)
            {
                if (items[i] == 0)
                {
                    imageText.text = "X";
                }
                else
                {
                    imageText.text = itemStr[i] + "\n" + items[i].ToString();
                }
            }
        }
    }

    public bool UseItem(int item)
    {
        if (item == -1) return true;

        if (items[item] > 0)
        {
            items[item]--;
            ShowItem();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectItem(int item)
    {
        ClearSelectItem();
        itemImage[item].color = Color.cyan;
    }

    public void UnSelectItem()
    {
        ClearSelectItem();
    }

    public void ClearSelectItem()
    {
        foreach (Image img in itemImage)
        {
            img.color = Color.white;
        }
    }

    public bool IsItemRemain(int item)
    {
        if (items[item] == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void InitItem(int[] itemNums)
    {
        items[(int)Item.Bomb] = itemNums[(int)Item.Bomb];
        items[(int)Item.Magnet] = itemNums[(int)Item.Magnet];
        items[(int)Item.KnockBack] = itemNums[(int)Item.KnockBack];
        items[(int)Item.Imotal] = itemNums[(int)Item.Imotal];
        ShowItem();
    }
}

