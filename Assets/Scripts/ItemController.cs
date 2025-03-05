using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public enum Item
{
    Bomb = 0,           // ��ź: ���� ���� �� ������ ȹ��
    Magnet = 1,         // �ڼ�: ���� ���� �� ������ ����
    AirBomb = 2,        // ��ġ��: ���� ���� �� ������ ��ġ��
    Imotal = 3,         // ����: Ư�� ��� ȸ��
    ZeroGravity = 4,    // ���߷�: Ư�� ��� ȸ��
    Cleaner = 5         // ������: Ư�� ��� ȸ��
}


public class ItemController : MonoBehaviour
{
    public static ItemController Instance { get; private set; }

    [SerializeField] private List<Button> itemButtons;
    [SerializeField] private int[] items;

    [SerializeField] private List<Image> itemImage;

    private string[] itemStr = { "Bomb", "Magnet", "AirBomb", "Imotal", "ZeroGravity", "Cleaner", "Empty" };


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
        InitItem();
        ShowItem();
    }


    private void InitItem()
    {
        //for (int i = 0; i < items.Length; i++)
        //{
        //    items[i] = 2;
        //}
        items[0] = 2;
        items[1] = 2; // Magnet Test
    }

    private void ShowItem()
    {
        // Function on Buttons
        //for (int i = 0; i < itemButtons.Count; i++)
        //{
        //    TextMeshProUGUI buttonText = itemButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        //    if (buttonText != null)
        //    {
        //        if (items[i] == 0)
        //        {
        //            buttonText.text = "X";
        //        }
        //        else
        //        {
        //            buttonText.text = itemStr[i] + "\n" + items[i].ToString();
        //        }
        //    }
        //}

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
}

