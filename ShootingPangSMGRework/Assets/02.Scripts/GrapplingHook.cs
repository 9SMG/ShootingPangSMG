using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    Transform mouseCorsorTr;

    LineRenderer lineRenderer;
    DistanceJoint2D distJoint;

    Rigidbody2D parentRb;

    RaycastHit2D hit;
    Vector3 mousePos;
    Vector2 localHitPoint;

    int rayLayerMask;
    float orginDamping;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mouseCorsorTr = transform.GetChild(0);
        distJoint = GetComponent<DistanceJoint2D>();
        parentRb = transform.parent.GetComponent<Rigidbody2D>();

        rayLayerMask = 1 << LayerMask.NameToLayer("Wall");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distJoint.enabled = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetDragable())
        {
            if (distJoint.enabled)
                distJoint.enabled = false;
            lineRenderer.SetPosition(1, Vector3.zero);

            return;
        }
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseCorsorTr.position = (Vector2)mousePos;

        Vector3 dir = mousePos - transform.position;
        hit = Physics2D.Raycast(transform.position, dir.normalized, 50f, rayLayerMask);
        localHitPoint = transform.InverseTransformPoint(hit.point);

        lineRenderer.SetPosition(1, localHitPoint);

        if(Input.GetMouseButtonDown(0))
        {
            distJoint.enabled = true;
            distJoint.connectedAnchor = hit.point;
            distJoint.distance = hit.distance;

            lineRenderer.enabled = false;
            orginDamping = parentRb.linearDamping;
            parentRb.linearDamping = 0f;
        }    
        if(Input.GetMouseButtonUp(0))
        {
            distJoint.enabled = false;

            lineRenderer.enabled = true;

            parentRb.linearDamping = orginDamping;
        }
    }

    float dampingDelayDelta;
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            float deg1 = Vector2.Angle(new Vector2(localHitPoint.y, -localHitPoint.x), parentRb.linearVelocity);
            float deg2 = Vector2.Angle(new Vector2(-localHitPoint.y, localHitPoint.x), parentRb.linearVelocity);

            if(deg1 < deg2)
            {
                parentRb.linearVelocity = new Vector2(localHitPoint.y, -localHitPoint.x).normalized * parentRb.linearVelocity.magnitude;
            }
            else
            {
                parentRb.linearVelocity = new Vector2(-localHitPoint.y, localHitPoint.x).normalized * parentRb.linearVelocity.magnitude;
            }
        }
    }
}
