using Unity.VisualScripting;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    private string tagBullet = "Bullet";
    //private string layerInvincible = "Invincible";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(tagBullet))
        {
            // ���̾� ����ó��
            //if (collision.gameObject.layer == LayerMask.NameToLayer(layerInvincible))
            //    Break;

            Destroy(collision.gameObject);
            Invoke("MakeBullet", 2f);
        }
    }

    private void MakeBullet()
    {
        PlayerController.Instance.MakeBullet();
        PlayerController.Instance.isBulletSelected = false;
        PlayerController.Instance.selectAvailable = true;
        PlayerController.Instance.bulletDestroyed = true;
    }

}
