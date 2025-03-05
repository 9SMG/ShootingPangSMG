using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    public static SoundsPlayer Instance { get; private set; }
    private AudioSource audio;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ����Ǿ ������
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� ����
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip sfx)
    {
        audio.PlayOneShot(sfx);
    }

    public void PlaySFX(AudioClip sfx, float time)
    {
        audio.PlayOneShot(sfx);
        Invoke("StopPlay", time);
    }

    public void StopPlay()
    {
        audio.Stop();
    }
}
