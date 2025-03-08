using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] CircleCollider2D _circleColl;

    public void UseBomb()
    {
        _circleColl.enabled = true;
    }
}
