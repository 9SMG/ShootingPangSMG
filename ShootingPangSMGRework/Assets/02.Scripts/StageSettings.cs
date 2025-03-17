using UnityEngine;

public class StageSettings : MonoBehaviour
{
    //[SerializeField] private int itemBomb;      // ��ź: ���� ���� �� �� �ı�
    //[SerializeField] private int itemMagnet;    // �ڼ�: ���� ���� �� ������ ����
    //[SerializeField] private int itemKnockBack; // ��ġ��: ���� ���� �� �� ��ġ��
    //[SerializeField] private int itemImotal;    // ����: Ư�� ��� ȸ��
    //[SerializeField] private int itemCleaner;   // ������: Ư�� ��� ȸ��
    //[SerializeField] private int itemZeroGravity;   // ���߷�: Ư�� ��� ȸ��

    [SerializeField] private int[] items = new int[4];

    [SerializeField] private int numCoin;
    [SerializeField] private int numBullet;

    [SerializeField] private Vector3 playerPosition;

    [SerializeField] private int cameraSize;

    private void SetStage()
    {
        Camera.main.orthographicSize = cameraSize;
    }

    public int GetItemCount(int item)
    {
        return items[item];
    }
    public int GetCoinNum()
    {
        return numCoin;
    }
    
    public int GetBulletNum()
    {
        return numBullet;
    }

    public Vector3 GetStartPos()
    {
        return playerPosition;
    }

    public int GetCameraSize()
    {
        return cameraSize;
    }
}
