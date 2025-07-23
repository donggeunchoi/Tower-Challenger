using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public SoundSource soundSourcePrefab;

    public AudioSource bgmSource;
    public AudioClip[] bgmAudioClip;
    public AudioClip[] miniGameAudioClip;

    public AudioClip gameOverBGM;

    public Transform soundSlot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        bgmSource = GetComponent<AudioSource>();
    }

    #region SceneChange
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        ChangeBGM(scene.name);
    }
    #endregion
    public void ChangeBGM(string BGM)
    {
        if (bgmAudioClip == null || bgmAudioClip.Length == 0)
        {
            return;
        }

        switch (BGM)
        {
            case "StartScene":
                PlayBGM(FindMusic("GameStart"));
                break;
            case "VillageScene":
                PlayBGM(FindMusic("Village"));
                break;
            case "TowerEntrance":
            case "GameScene":
                PlayBGM(FindMusic("TowerEntrance_Lobby"));
                break;
            case "TopScene-1":
            case "TopScene-2":
            case "TopScene-3":
            case "TopScene-4":
                PlayBGM(FindMusic("Top"));
                break;
            case "BossRoom":
                PlayBGM(FindMusic("TowerBoss"));
                break;
            default:
                AudioClip miniGameClip = Array.Find(miniGameAudioClip, clip => clip != null && clip.name.Contains(BGM));

                if (miniGameClip != null)
                {
                    PlayBGM(miniGameClip);
                    break;
                }
                else
                {
                    StopBGM();
                    break;
                }
        }
    }

    public AudioClip FindMusic(string musicName)
    {
        return Array.Find(bgmAudioClip, clip => clip != null && clip.name == musicName);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
            return;

        if (bgmSource == null)
            bgmSource = GetComponent<AudioSource>();

        if (bgmSource.clip == clip)
            return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = null;
    }

    public void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(soundSourcePrefab, soundSlot);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, 1);
    }

    public void PlayeGameOver()
    {
        if (gameOverBGM != null)
            PlayBGM(gameOverBGM);
    }
}
