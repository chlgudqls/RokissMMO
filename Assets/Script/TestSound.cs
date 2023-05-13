using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public AudioClip audioClip;
    public AudioClip audioClip2;

    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(audioClip);
        //audio.PlayOneShot(audioClip2);

        //// 이거구나 죽고나면 사라지게 하는거 이것도 써야겠다
        //// 쓰려고했는데 에니메이션도 생각해야되는구나
        //float lifeTime = Mathf.Max(audioClip.length, audioClip2.length);

        //GameObject.Destroy(gameObject, lifeTime);

        i++;

        if (i % 2 == 0)
            Managers.Sound.Play(audioClip, Define.Sound.Bgm);

        else
        Managers.Sound.Play(audioClip2, Define.Sound.Bgm);
    }
}
