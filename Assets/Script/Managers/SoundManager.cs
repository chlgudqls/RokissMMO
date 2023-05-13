using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    // ĳ��@@@@@@@@@@@@@@ 
    // �ѹ�ã���� �ٽ�ã���ʰ� ������ �������°� ã�°͵� ���̶�
    // �������������� �޸𸮰����ϰɸ�
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // MP3 Player   -> AudioSource
    // MP3 ����     -> AudioClip
    // ����(��)     -> AudioListener   -   ī�޶�

    // init �� �޴������� init�Ҷ� ������
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                // ������Ʈ�ϳ��� �Ѱ��� ���̴°ǰ�����
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }
    // ������µ� ��� ��������
    // ���������ʾƼ� ���̹ٲ� Ŭ��� �ʿ��ߴ�
    // ���尡�ƴ� �ٸ��͵� Ŭ�����ؾߵǴ»�Ȳ�־ �Ŵ������� �����ϵ��� �����ʿ�
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);

        Play(audioClip, type , pitch);
    }

    // ��ξ��� �����Ŭ���� ���ؼ���
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        AudioClip audioClip = null;

        if (!path.Contains("Sound/"))
            path = $"Sound/{path}";

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (!_audioClips.TryGetValue(path, out audioClip))
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);

            }
        }

        if (audioClip == null)
            Debug.Log($"Audio Clip Missing ! {path}");

        return audioClip;
    }
}
