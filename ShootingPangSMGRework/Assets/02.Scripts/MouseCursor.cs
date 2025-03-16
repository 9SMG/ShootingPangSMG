using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    Vector3 mousePos;

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (Vector2)mousePos;
    }
}
