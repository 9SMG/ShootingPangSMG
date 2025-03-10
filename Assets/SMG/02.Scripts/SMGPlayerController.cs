using UnityEngine;

/*
 * ȭ���� ��Ŭ������ ��, �߻� �غ�
 * �߻��غ� ����(��Ŭ��)���� �巹���ϸ� ����� �� ���� ����
 * �߻��غ� ����(��Ŭ��)���� ��Ŭ���� ����, �߻�
 * �߻��غ� ����(��Ŭ��)���� ��Ŭ���� �ϸ�, ���
 */



public class SMGPlayerController : MonoBehaviour
{
    Gage gage;    
    Vector2 mouseButtonDownPos;
    
    float hitVectorScale = 11f;
    bool isDraging = false;

    void Start()
    {
        gage = GetComponentInChildren<Gage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SMGGameManager.Instance.GetDragable())
        {
            if (isDraging)
            {
                isDraging = false;
                DragGage(isDraging);
            }
            return;
        }
            
        if(Input.GetMouseButtonDown(0) && !isDraging)
        {
            isDraging = true;
            mouseButtonDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0) && isDraging)
        {
            isDraging = false;
            HitBullet();
        }

        if(Input.GetMouseButtonDown(1))
        {
            isDraging = false;
        }

        DragGage(isDraging);
    }

    void DragGage(bool isDraging)
    {
        if (isDraging)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 dragVector = (Vector2)transform.position - currentMousePosition;
            Vector2 dragVector = mouseButtonDownPos - currentMousePosition;

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

    void HitBullet()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 dragVector = (Vector2)transform.position - currentMousePosition;
        Vector2 dragVector = mouseButtonDownPos - currentMousePosition;
        // 0 Vector Cancel
        if (dragVector == Vector2.zero)
            return;
        Vector2 hitVector = dragVector.normalized * gage.GetGage() * hitVectorScale;

        //bullet.HitBall(hit);
        SMGGameManager.Instance.HitBullet(hitVector);
    }
}
