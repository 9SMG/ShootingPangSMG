using UnityEngine;

/*
 * ��������� ��Ŭ������ ��, �߻� �غ�
 * �߻��غ� ����(��Ŭ��)���� �巹���ϸ� ����� �� ���� ����
 * �߻��غ� ����(��Ŭ��)���� ��Ŭ���� ����, �߻�
 * �߻��غ� ����(��Ŭ��)���� ��Ŭ���� �ϸ�, ���
 */

public class SMGPlayerController : MonoBehaviour
{
    Gage gage;

    bool isDragable = false;
    float hitVectorScale = 11f;

    public SMGBullet bullet;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gage = GetComponentInChildren<Gage>();

        //Vector2 test1 = new Vector2(1, 1);
        //Vector2 test2 = new Vector2(1, 1);
        //Debug.Log("test1: " + test1);
        //test1 *= 2f;
        //Debug.Log("test1: " + test1);

        //Debug.Log("test2: " + test2);
        //test2 = test2.normalized * 2f;
        //Debug.Log("test2: " + test2);


        //Vector2 test3 = new Vector2(1, 0);
        //Vector2 test4 = new Vector2(1, 1);
        //Debug.Log("angle: " + Vector2.Angle(test3, test4));
        //Debug.Log("angle: " + Mathf.Atan2(test4.y, test4.x) * Mathf.Rad2Deg);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isDragable = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isDragable = false;

            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = (Vector2)transform.position - currentMousePosition;
            Vector2 hitVector = dragVector.normalized * gage.GetGage() * hitVectorScale;
            
            HitBullet(hitVector);
        }

        if(Input.GetMouseButtonDown(1))
        {
            isDragable = false;
        }

        if(isDragable)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragVector = (Vector2)transform.position - currentMousePosition;
            
            // gage rotate
            float angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);

            // gage power
            gage.SetGage(dragVector.magnitude);
        }
        else
        {
            gage.SetGage(0f);
        }
    }

    void HitBullet(Vector2 hit)
    {
        bullet.HitBall(hit);
    }
}
