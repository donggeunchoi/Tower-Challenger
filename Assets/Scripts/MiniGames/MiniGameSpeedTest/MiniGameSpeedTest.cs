using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MiniGameSpeedTest : MonoBehaviour
{
    StageManager stageManager;

    public TextMeshProUGUI trueOrFalse; //텍스트 UI

    public Button speedUIBtn;           //순발력 게임버튼
    private bool isGreen = false;       //색이 현재 그린인지검사
    private bool isClick = false;       //클릭했는지 검사
    private bool isFalseEffect = false; //실패임팩트 중복실행 방지
    private Coroutine miniGameCoroutine;

    private void Awake()
    {
        stageManager = StageManager.instance;
    }

    private void Start()
    {
        trueOrFalse.text = "";
        StartGame();
    }

    private void StartGame()
    {
        if (miniGameCoroutine != null)
        {
            StopCoroutine(miniGameCoroutine);
            miniGameCoroutine = null;
        }

        isGreen = false;
        isClick = false;
        speedUIBtn.interactable = true;
        miniGameCoroutine = StartCoroutine(StartMiniGame());
        speedUIBtn.onClick.RemoveAllListeners();
        speedUIBtn.onClick.AddListener(OnClickImg);
    }


    public void OnClickImg() //이미지를 누를시
    {
        if (isClick)
            return;

        isClick = true;

        speedUIBtn.interactable = false;

        if (isGreen)
        {
            trueOrFalse.text = "True!";
            isClick = true;
            isGreen = false;
            speedUIBtn.image.color = Color.white;
            if (miniGameCoroutine != null)
                StopCoroutine(miniGameCoroutine);

            GameResult(true);
        }
        else
        {
            trueOrFalse.text = "False!";
            StartCoroutine(FailEffect());
            GameResult(false);
        }
    }

    private void GameResult(bool gameResult)
    {
        if (gameResult)
        {
            stageManager.MiniGameResult(true);
            return;
        }
        else
        {
            stageManager.MiniGameResult(false);
            ResetGameState();
            StartGame();
        }
    }

    private void ResetGameState()
    {
        isGreen = false;
        isClick = false;
        speedUIBtn.interactable = true;
        speedUIBtn.image.color = Color.white;
    }

    private IEnumerator StartMiniGame() //미니게임
    {
        
        speedUIBtn.image.color = Color.white;            //시작색 초기화
        trueOrFalse.text = "is green Click To display!"; //게임설명
        isGreen = false;
        isClick = false;
        yield return new WaitForSeconds(1f);

        trueOrFalse.text = "Ready?";

        float randomGameTime = Random.Range(0f, 6f);                 //랜덤값설정
        randomGameTime = Mathf.Round(randomGameTime * 100f) / 100f;   //반올림
        Debug.Log(randomGameTime);   //랜덤시간 몇초인지 로그
        yield return new WaitForSeconds(randomGameTime); //랜덤시간동안 기다렸다가

        speedUIBtn.image.color = Color.green;
        isGreen = true;
        trueOrFalse.text = "Click!!";
        yield return new WaitForSeconds(3f); //1초동안 눌러도되는시간

        if (isGreen && !isClick)  //클릭했는지 안했는지 검사 안했으면 실패
        {
            isGreen = false;
            trueOrFalse.text = "False!";
            OnClickImg();
            yield break;
        }
        else if (isGreen && isClick)  //했으면 성공
        {
            speedUIBtn.image.color = Color.white;
        }
    }

    private IEnumerator FailEffect()  //실패 임팩트
    {
        if (isFalseEffect) yield break;
        isFalseEffect = true;

        speedUIBtn.image.color = Color.red;
        yield return new WaitForSeconds(1f);
        speedUIBtn.image.color = Color.white;

        isFalseEffect = false;
        isClick = false;
        speedUIBtn.interactable = true;

        GameResult(false);
    }
}
