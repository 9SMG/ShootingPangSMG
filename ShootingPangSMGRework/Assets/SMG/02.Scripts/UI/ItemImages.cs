using UnityEngine;
using System.Collections.Generic;

public class ItemImages : MonoBehaviour
{
    List<GameObject> items = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(transform.childCount);
        for(int i = 0; i < transform.childCount; i++)
        {
            items.Add(transform.GetChild(i).gameObject);
        }
    }

    #region ContextMenu Function
    [ContextMenu("SetItemImage(-1)")]
    void TestSetItemImageReset()
    {
        SetItemImage(-1);
    }

    [ContextMenu("SetItemImage(0)")]
    void TestSetItemImage0()
    {
        SetItemImage(0);
    }

    [ContextMenu("SetItemImage(1)")]
    void TestSetItemImage1()
    {
        SetItemImage(1);
    }

    [ContextMenu("SetItemImage(2)")]
    void TestSetItemImage2()
    {
        SetItemImage(2);
    }
    #endregion

    public void SetItemImage(int item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(i == item ? true : false);
        }
    }

    
}
