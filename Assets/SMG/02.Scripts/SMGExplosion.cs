using UnityEngine;

public class SMGExplosion : MonoBehaviour
{
    float range = 4f;
    public LayerMask targetLayer = -1;  // default Everything
    public string[] targetTags = {"Block"};

    int gameObjectID;
    float minDepthZ = -10;

    private void Start()
    {
        gameObjectID = gameObject.GetInstanceID();
    }

    [ContextMenu("Explosion")]
    public void Explosion()
    {
        Debug.Log("call SMGExplosion.Explosion()");
        // 대상 레이어 충돌 체크
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, range, targetLayer, minDepthZ);

        bool hasTagElements = targetTags.Length > 0 ? true : false;

        foreach (Collider2D _coll in colls)
        {
            if(_coll.gameObject.GetInstanceID() == gameObjectID)
                continue;

            //태그 배열이 있을 시, 태그 검사
            if(hasTagElements)
            {
                bool isTargetTag = false;
                foreach(string _tag in targetTags)
                {
                    if(_coll.CompareTag(_tag) == true)
                    {
                        isTargetTag = true;
                        break;
                    }
                }
                if (!isTargetTag)
                    continue;
            }
            if(_coll.GetComponent<Rigidbody2D>() != null)
                // 파괴 실행
                Destroy(_coll.gameObject);
        }
    }
}
