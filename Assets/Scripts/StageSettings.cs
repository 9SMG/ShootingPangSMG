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


    private void Awake()
    {
        SetStage();
    }

    private void SetStage()
    {
        // Item�� ItemContoller
        // Coin�� GameManager
        // Bullet�� PlayerController
        // �� ���� �߱����� �����ΰ�
        //ItemController.Instance.items[(int)Item.Bomb] = itemBomb;
        //ItemController.Instance.items[(int)Item.Magnet] = itemMagnet;
        //ItemController.Instance.items[(int)Item.KnockBack] = itemKnockBack;
        //ItemController.Instance.items[(int)Item.Imotal] = itemImotal;
        //ItemController.Instance.items[(int)Item.Cleaner] = itemCleaner;
        //ItemController.Instance.items[(int)Item.ZeroGravity] = itemZeroGravity;
        ItemController.Instance.InitItem(items);

        //GameManager.Instance.remainCoin = numCoin;
        GameManager.Instance.InitCoin(numCoin);

        //PlayerController.Instance.bulletPossess = numBullet;
        PlayerController.Instance.InitBullet(numBullet);

        //PlayerController.Instance.gameObject.transform.position = PlayerPosition;
        PlayerController.Instance.InitPosition(playerPosition);
    }

    public int GetCoinNum()
    {
        return numCoin;
    }
    
    public int GetBulletNum()
    {
        return numBullet;
    }
}
