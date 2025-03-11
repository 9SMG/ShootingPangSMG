using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] LayerMask layerMask;

    [SerializeField] float pushPower;

    [ContextMenu("Push")]
    //타겟 당기기
    public void Push()
    {
        Debug.Log("Call KnockBack.Push()");
        //스캔된 오브젝트들
        RaycastHit2D[] targets2D = Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 0, layerMask);

        foreach (RaycastHit2D target2D in targets2D)
        {
            if (target2D.transform.CompareTag("Block"))
            {
                Vector2 dir = target2D.transform.position - transform.position;
                dir = dir.normalized;

                Rigidbody2D _rigid2D = target2D.transform.GetComponent<Rigidbody2D>();
                _rigid2D.AddForce(dir* pushPower, ForceMode2D.Impulse);
            }
        }
    }
}
