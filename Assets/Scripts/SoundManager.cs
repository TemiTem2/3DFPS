using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;//곡
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    #endregion

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;
    // Update is called once per frame
    private void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }

    public void PlaySE(string name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++) // Fix: Corrected the loop condition
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 AudioSource가 재생중입니다.");
                return;
            }
        }
        Debug.Log("해당 이름의 효과음이 없습니다: " + name);
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == name)
            {
                audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생중인 효과음이 없습니다: " + name);
    }
}
