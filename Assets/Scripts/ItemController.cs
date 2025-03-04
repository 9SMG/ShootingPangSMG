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

    private string[] itemStr = { "Normal", "Bomb", "Magnet", "AirBomb", "Imotal", "ZeroGravity", "Cleaner" };


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
        for (int i = 0; i < itemButtons.Count; i++)
        {
            TextMeshProUGUI buttonText = itemButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                if (items[i] == 0)
                {
                    buttonText.text = "X";
                }
                else
                {
                    buttonText.text = itemStr[i] + "\n" + items[i].ToString();
                }
            }
        }
    }

    public bool ItemUsed(int item)
    {
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
}

