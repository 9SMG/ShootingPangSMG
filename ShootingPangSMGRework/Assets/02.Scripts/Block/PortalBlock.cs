using UnityEngine;

/*
 * [포탈]
 * ! MasterPortal이 부모 객체에 있어야 합니다
 * 기능
 *  포탈은 입/출구 구분이 없음
 *  단, 하나의 객체는 해당 포탈을 한번만 사용 가능
 *  포탈은 방향이 있음
 */
public class PortalBlock : MonoBehaviour
{
    private string tagBullet = "Bullet";

    [SerializeField]
    private MasterPortal masterPortal;


    public GameObject OutPortal;

    public AudioClip sfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        masterPortal = GetComponentInParent<MasterPortal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tagBullet))
        {
            GameObject _gameObject = collision.gameObject;

            bool isUsed = masterPortal.FindID(_gameObject.GetInstanceID());

            if (isUsed == false)
            {
                masterPortal.AddID(_gameObject.GetInstanceID());

                //Trail 숨김
                Debug.Log("Portal OnTriggerEnter2D()");
                TrailVisible[] trailVisibles = _gameObject.GetComponentsInChildren<TrailVisible>();
                foreach(TrailVisible trailVisible in trailVisibles)
                {
                    trailVisible.SetVisible(false);
                    trailVisible.SetVisibleTimer(true, 0.1f);
                }


                //위치 이동
                _gameObject.transform.position = OutPortal.transform.position;

                //힘 방향 조정 (벡터 회전)
                float portalDeg = OutPortal.transform.eulerAngles.z - transform.eulerAngles.z;
                float rad = portalDeg * Mathf.Deg2Rad;
                float cos = Mathf.Cos(rad);
                float sin = Mathf.Sin(rad);

                Rigidbody2D _rig = collision.GetComponent<Rigidbody2D>();
                Vector2 velocity = _rig.linearVelocity;

                float x = velocity.x * cos - velocity.y * sin;
                float y = velocity.x * sin + velocity.y * cos;

                _rig.linearVelocity = new Vector2(x, y);

                SoundsPlayer.Instance.PlaySFX(sfx);
                #region 에러 흔적
#if false
                ////Debug.Log("sin(0) : " + Mathf.Sin(0) + " sin(90): " + Mathf.Sin(90 * Mathf.Deg2Rad));
                ////Debug.Log("cos(0): " + Mathf.Cos(0) + " cos(90): " + Mathf.Cos(90 * Mathf.Deg2Rad));
                ////Debug.Log("in z: " + transform.eulerAngles.z + " out z: " + OutPortal.transform.eulerAngles.z);
                //float portalDeg = transform.eulerAngles.z - OutPortal.transform.eulerAngles.z;
                ////float deg = OutPortal.transform.eulerAngles.z - transform.eulerAngles.z;
                ////Debug.Log("deg: " + deg);
                //Vector2 dir = new Vector2(Mathf.Sin(portalDeg * Mathf.Deg2Rad), Mathf.Cos(portalDeg * Mathf.Deg2Rad));
                //Debug.Log("dir : " + dir);

                //Debug.Log("before velocity: " + collision.GetComponent<Rigidbody2D>().linearVelocity);
                //float power = collision.GetComponent<Rigidbody2D>().linearVelocity.magnitude;
                ////Debug.Log("power: " + power);

                ////collision.GetComponent<Rigidbody2D>().linearVelocity = power * dir;
                //Vector2 velDir = collision.GetComponent<Rigidbody2D>().linearVelocity.normalized;
                ////Debug.Log("velDir: " + velDir);
                ////collision.GetComponent<Rigidbody2D>().linearVelocity = power * (dir + velDir).normalized;
                //collision.GetComponent<Rigidbody2D>().linearVelocity = power * dir;
                ////Debug.Log("power * deg: " + (power * dir));
                //Debug.Log("after velocity: " + collision.GetComponent<Rigidbody2D>().linearVelocity);


                //////Vector2 direction = new Vector2(Mathf.Cos(dir.z), Mathf.Sin(dir.z)).normalized;

                //////collision.gameObject.transform.rotation *= Quaternion.Euler(0, 0, 90f);

                ////Quaternion normal = OutPortal.transform.rotation.normalized;
                //////float normalZ = (360 - normal.z) % 360;
                //////Vector2 tmp = new Vector2(0 + normalZ, 1 + normalZ);
                ////Debug.Log(normal.eulerAngles);
                //////tmp.Normalize();
                //////Debug.Log("tmp : " + tmp);
                //////collision.GetComponent<Rigidbody2D>().linearVelocity *= ;
                //////collision.GetComponent<Rigidbody2D>().SetRotation(90);
                //////collision.gameObject.transform.rotation *= Out

                ////float power = collision.GetComponent<Rigidbody2D>().linearVelocity.magnitude;
                ////Quaternion dir = OutPortal.transform.rotation.normalized;
                ////Vector2 direction = new Vector2(Mathf.Cos(dir.z), Mathf.Sin(dir.z)).normalized;

                ////Debug.Log("Why not move");
                ////Debug.Log($"Velocity: {collision.GetComponent<Rigidbody2D>().linearVelocity}");

                ////collision.GetComponent<Rigidbody2D>().linearVelocity = direction * power;
                //////collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, OutPortal.transform.position.z).normalized * power, ForceMode2D.Impulse);
#endif
                #endregion
            }
        }
    }


}
