using Unity.VisualScripting;
using UnityEngine;

public class WireTest : MonoBehaviour
{
    LineRenderer lineRenderer;
    Transform mouseCursorTr;
    Rigidbody2D rb;
    DistanceJoint2D distJoint;
    //SpringJoint2D springJoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mouseCursorTr = transform.GetChild(0);

        rb = GetComponent<Rigidbody2D>();
        distJoint = GetComponent<DistanceJoint2D>();
        //springJoint = GetComponent<SpringJoint2D>();
    }


    bool isClicked = false;
    Vector3 mousePos;
    void Update()
    {
        if (GameManager.Instance.GetDragable())
            return;
        
        if (true)//!isClicked)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseCursorTr.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }
        
        Vector3 dir = mousePos - transform.position;
        dir.z = 0f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, LayerMask.GetMask("Wall"));
        lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
        }
        float speed = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * speed * 9f * Time.deltaTime);

        if(Input.GetMouseButtonDown(0))
        {
            distJoint.enabled = true;
            distJoint.connectedAnchor = hit.point;
            distJoint.distance = Vector2.Distance(transform.position, hit.point);

            //springJoint.enabled = true;
            //springJoint.connectedAnchor = hit.point;
            //springJoint.distance = Vector2.Distance(transform.position, hit.point);

            lineRenderer.enabled = false;

            //rb.linearVelocity *= dir.normalized * hookPower;
            //Debug.Log((dir.normalized * hookPower));
            //transform.parent.GetComponent<Rigidbody2D>().linearVelocity = transform.parent.GetComponent<Rigidbody2D>().linearVelocity + (Vector2)(dir.normalized * hookPower);
            //rb.mass = 1f;
            //rb.linearDamping = 0.1f;
            //lineRenderer.startColor = new Color(0.5f, 0.5f, 0.5f);
            //lineRenderer.endColor = new Color(0.5f, 0.5f, 0.5f);
            isClicked = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            distJoint.enabled = false;
            //springJoint.enabled = false;

            lineRenderer.enabled = true;
            //rb.mass = 2f;
            //rb.linearDamping = 0.5f;
            //lineRenderer.startColor = new Color(1f, 1f, 1f);
            //lineRenderer.endColor= new Color(0f, 0.92f, 1f);
            isClicked = false;
        }

    }

    public float hookPower = 2f;


}
