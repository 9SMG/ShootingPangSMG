using UnityEngine;

public class Magnetic : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float pullPower;

    //Ÿ�� ����
    public void Pull()
    {
        //��ĵ�� ������Ʈ��
        RaycastHit2D[] targets2D = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 0, layerMask);

        foreach (RaycastHit2D target2D in targets2D)
        {
            Vector2 dir = transform.position - target2D.transform.position;
            float distance = dir.magnitude;
            dir = dir.normalized * distance * pullPower;

            Rigidbody2D _rigid2D = target2D.transform.GetComponent<Rigidbody2D>();
            _rigid2D.AddForce(dir, ForceMode2D.Impulse);
        }
    }
}
