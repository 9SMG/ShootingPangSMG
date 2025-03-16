using System.Collections.Generic;
using UnityEngine;

/*
 * [마스터 포탈]
 * 포탈기능 관리
 * 포탈은 2개이상의 포탈이 한 묶음이므로, 이를 관리하는 기능을 수행함
 */

public class MasterPortal : MonoBehaviour
{
    List<int> inObjectIDList;// = new List<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inObjectIDList = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetIDList()
    {
        inObjectIDList.Clear();
    }

    public void AddID(int id)
    {
        inObjectIDList.Add(id);
    }



    public bool FindID(int findID)
    {
        bool isFind = false;

        foreach (int id in inObjectIDList)
        {
            if (id == findID)
            {
                isFind = true;
                break;
            }
        }

        return isFind;
    }
}
