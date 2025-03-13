using Unity.VisualScripting;
using UnityEngine;

public class WireTest : MonoBehaviour
{
    LineRenderer lineRenderer;
    Transform mouseCursorTr;
    Rigidbody2D rb;
    DistanceJoint2D distJoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mouseCursorTr = transform.GetChild(0);

        rb = GetComponent<Rigidbody2D>();
        distJoint = GetComponent<DistanceJoint2D>();
    }

    
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseCursorTr.position = new Vector3(mousePos.x, mousePos.y, 0f);

        Vector3 dir = mousePos - transform.position;
        dir.z = 0f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 100f, LayerMask.GetMask("Wall"));
        lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));
        //raycastHitPointTr.transform.position = transform.InverseTransformPoint(hit.point);
        //Debug.Log(hit.point);
        //distJoint.connectedAnchor = hit.point;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
        }
        float speed = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * speed * 9f * Time.deltaTime);
    }


}
