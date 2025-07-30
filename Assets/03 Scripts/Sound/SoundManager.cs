using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SoundType { BGM, SFX }
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public SoundSource soundSourcePrefab;

    public AudioSource bgmSource;
    public AudioClip[] bgmAudioClip;
    public AudioClip[] miniGameAudioClip;
    [SerializeField] private GameObject temporarySoundPlayerPrefab;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;

    public AudioClip gameOverBGM;

    public Transform soundSlot;

    private Dictionary<string, AudioClip> clipDictionary;
    private List<TemporarySoundPlayer> activaLoopSounds;

    [SerializeField] private GameObject TotalVolumeBar;
    [SerializeField] private GameObject BGXBar;
    [SerializeField] private GameObject EFXBar;

    public void FindVolumeBar()
    {
        TotalVolumeBar = GameObject.Find("MVolumeBar");
        BGXBar = GameObject.Find("B.G.XBar");
        EFXBar = GameObject.Find("E.F.XBar");

        Scrollbar totalVolumeSlider = TotalVolumeBar.GetComponent<Scrollbar>();
        Scrollbar bgxSlider = BGXBar.GetComponent<Scrollbar>();
        Scrollbar efxSlider = EFXBar.GetComponent<Scrollbar>();

        if (totalVolumeSlider != null)
        {
            totalVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            SetMasterVolume(totalVolumeSlider.value); // 초기값 즉시 반영
        }

        if (bgxSlider != null)
        {
            bgxSlider.onValueChanged.AddListener(SetBGMVolume);
            SetBGMVolume(bgxSlider.value); // 초기값 즉시 반영
        }

        if (efxSlider != null)
        {
            efxSlider.onValueChanged.AddListener(SetSFXVolume);
            SetSFXVolume(efxSlider.value); // 초기값 즉시 반영
        }
    }
    private void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("A", VolumeToDecibel(value));
    }

    private void SetBGMVolume(float value)
    {
        audioMixer.SetFloat("BGM", VolumeToDecibel(value));
    }

    private void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFX", VolumeToDecibel(value));
    }
    private float VolumeToDecibel(float value)
    {
        return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
    }
    private void Update()
    {

    }

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


    private void Start()
    {
        clipDictionary = new Dictionary<string, AudioClip>();

        foreach (AudioClip clip in bgmAudioClip)
        {
            if (clip != null && !clipDictionary.ContainsKey(clip.name))
                clipDictionary.Add(clip.name, clip);
        }

        foreach (AudioClip clip in miniGameAudioClip)
        {
            if (clip != null && !clipDictionary.ContainsKey(clip.name))
                clipDictionary.Add(clip.name, clip);
        }

        // 추가로 다른 효과음 클립들도 여기에 등록해야 할 수도 있습니다.
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
            case "TutorialScene":
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

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }

    public void PlayeGameOver()
    {
        if (gameOverBGM != null)
            PlayBGM(gameOverBGM);
    }
    public void PlaySound2D(string clipName, float delay = 0f, bool isLoop = false, SoundType type = SoundType.SFX)
    {
        GameObject obj = PoolManager.Instance.GetObject(temporarySoundPlayerPrefab, Vector3.zero, Quaternion.identity);

        TemporarySoundPlayer soundPlayer = obj.GetComponent<TemporarySoundPlayer>();

        if (isLoop)
        {
            AddToList(soundPlayer);
        }

        soundPlayer.InitSound2D(GetClip(clipName));
        soundPlayer.Play(audioMixer.FindMatchingGroups(type.ToString())[0], delay, isLoop); // null?

        if (!isLoop) //단발 사운드
        {
            soundPlayer.SetOnFinish(() => PoolManager.Instance.ReturnObject(obj));
        }
    }
    private void AddToList(TemporarySoundPlayer soundPlayer) //사운드 재생 중, 나중에 루프 형태로 재생된 사운드를 제거하기 위해 리스트 저장.
    {
        activaLoopSounds.Add(soundPlayer);
    }
    private AudioClip GetClip(string clipName)
    {
        if (clipDictionary == null) return null;
        AudioClip clip = clipDictionary[clipName]; //예외처리 필요.

        if (clip == null)
        {
            Debug.LogError(clipName + "이 존재하지 않음.");
        }

        return clip;
    }
}
