using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using NUnit.Framework;

public class MiniGameSpeedTest : MonoBehaviour
{
    StageManager stageManager;

    //public TextMeshProUGUI trueOrFalse; //텍스트 UI

    [SerializeField] private Image backGround;

    public Button speedUIBtn;           //순발력 게임버튼
    private bool isGreen = false;       //색이 현재 그린인지검사
    private bool isClick = false;       //클릭했는지 검사
    private bool isFalseEffect = false; //실패임팩트 중복실행 방지
    private Coroutine miniGameCoroutine;

    [SerializeField] private Sprite[] sprites;

    [SerializeField] private float delayTime;
    [SerializeField] private float successTime;
    
    [Header("미니게임 클리어 UI")]
    public GameObject miniGameClearUI;
    public Canvas mainCanvas;
    private bool _clear = false;

    private void Awake()
    {
        stageManager = StageManager.instance;
    }

    private void Start()
    {
        if (stageManager != null && GameManager.Instance != null)
        {
            int difficulty = stageManager.difficulty;

            MiniGameData data = GameManager.Instance.miniGameDataList.Find(x => x.name == "SpeedTest" && x.DifficultyLevel == difficulty);

            if (data != null)
            {
                successTime = data.successTime;
                delayTime = data.delayTime;
            }
        }

        StartGame();
    }

    private void StartGame()
    {
        speedUIBtn.onClick.RemoveAllListeners();

        backGround.sprite = sprites[0];

        if (miniGameCoroutine != null)
        {
            StopCoroutine(miniGameCoroutine);
            miniGameCoroutine = null;
        }

        isGreen = false;
        isClick = false;
        speedUIBtn.interactable = false;
        speedUIBtn.onClick.AddListener(OnClickImg);
        miniGameCoroutine = StartCoroutine(StartMiniGame());
    }


    public void OnClickImg() //이미지를 누를시
    {

        speedUIBtn.interactable = false;

        if (isClick)
            return;

        isClick = true;

        if (isGreen)
        {
            //trueOrFalse.text = "True!";
            isClick = true;
            isGreen = false;
            backGround.sprite = sprites[0];
            if (miniGameCoroutine != null)
                StopCoroutine(miniGameCoroutine);
            
            if (!_clear)
            {
                _clear = true;
                ShowClearUI();
                StartCoroutine(WaitinTime());
            }
        }
        else
        {
            //trueOrFalse.text = "False!";
            StartCoroutine(FailEffect());
            
        }
    }

    IEnumerator WaitinTime()
    {
        if(_clear == false) yield break;
        
        yield return new WaitForSeconds(1f);
        
        if (stageManager != null)
        {
            stageManager.MiniGameResult(true);
            
        }
    }

    private void ResetGameState()
    {
        isGreen = false;
        isClick = false;
        speedUIBtn.interactable = false;
        backGround.sprite = sprites[0];
    }

    private IEnumerator StartMiniGame() //미니게임
    {

        backGround.sprite = sprites[0];            //시작색 초기화
        isGreen = false;
        isClick = false;
        yield return new WaitForSeconds(1f);

        speedUIBtn.interactable = true;

        float randomGameTime = Random.Range(0f, delayTime);                 //랜덤값설정
        randomGameTime = Mathf.Round(randomGameTime * 100f) / 100f;   //반올림
        Debug.Log(randomGameTime);   //랜덤시간 몇초인지 로그
        yield return new WaitForSeconds(randomGameTime); //랜덤시간동안 기다렸다가

        backGround.sprite = sprites[1];
        isGreen = true;
        SoundManager.instance.PlaySound2D("BoorLight");
        yield return new WaitForSeconds(successTime); //1초동안 눌러도되는시간

        if (isGreen && !isClick)  //클릭했는지 안했는지 검사 안했으면 실패
        {
            speedUIBtn.interactable = false;
            isGreen = false;
            OnClickImg();
            yield break;
        }
        else if (isGreen && isClick)  //했으면 성공
        {
            backGround.sprite = sprites[1];
        }
    }

    private IEnumerator FailEffect()  //실패 임팩트
    {
        if (isFalseEffect) yield break;
        isFalseEffect = true;

        backGround.sprite = sprites[2];
        yield return new WaitForSeconds(1f);

        isFalseEffect = false;
        isClick = false;
        speedUIBtn.interactable = true;

        if (stageManager != null)
        stageManager.MiniGameResult(false);

        ResetGameState();
        StartGame();
    }
    
    IEnumerator ScaleUp(RectTransform rect, float duration)
    {
        float time = 0f;
        Vector3 from = Vector3.zero;
        Vector3 to = Vector3.one;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, time / duration);
            rect.localScale = Vector3.Lerp(from, to, t);
            yield return null;
        }
        
        rect.localScale = to;
    }

    void ShowClearUI()
    {
        GameObject miniGameClear = Instantiate(miniGameClearUI,mainCanvas.transform);
        miniGameClear.transform.SetAsLastSibling();
        
        var rt = miniGameClear.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 0.5f);         // 하단 중앙
        rt.anchoredPosition = Vector2.zero;       // 캔버스 하단 중앙
        rt.localScale = Vector3.zero;             // 초기 스케일 0
        
        // 3) Scale 애니메이션
        StartCoroutine(ScaleUp(rt, 0.5f));        // 0.5초 동안
    }
}
