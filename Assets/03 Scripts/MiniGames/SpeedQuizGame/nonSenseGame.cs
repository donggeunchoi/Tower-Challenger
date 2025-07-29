using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class nonSenseGame : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text quizText;
    public Button[] answerButtons;
    
    public List<SpeedQuizData> quizes = new List<SpeedQuizData>();
    private string currentCorrectAnswer;
    
    [Header("미니게임 클리어 UI")]
    public GameObject miniGameClearUI;
    public Canvas mainCanvas;
    private bool _clear = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SettingQuiz();
        //시작에는 보기를 랜덤으로 배정하는 부분을 구현해야하고
        //문제가 나타나게 만들어야하고
        //정답에 대한 부분을 인지를 해야하고
    }

    public void SettingQuiz()
    {
        Debug.Log(quizes.Count);
        
        quizes = QuizBase.QuizList;
        //선택 퀴즈를 랜덤으로 뽑기
        SpeedQuizData selectedQuiz = quizes[Random.Range(0, quizes.Count)];
        
        Debug.Log("저기부터니");
        //퀴즈텍스트는 선택한 퀴즈로 & 현재 정답은 선택한 답으로
        quizText.text = selectedQuiz.question;
        currentCorrectAnswer = selectedQuiz.answer;
        
        List<string> wrongAnswers = new List<string>{selectedQuiz.op2,selectedQuiz.op3,selectedQuiz.op4};
        
        //모든 답들은 리스트로 받아오기. 거기에 정답 추가시키기
        List<string> allAnswers = new List<string>(wrongAnswers);
        
        allAnswers.Add(selectedQuiz.answer);
        
        Shuffle(allAnswers);

        //하나씩 돌면서 해당 문제의 답을 확인하기
        for (int i = 0; i < answerButtons.Length; i++)
        {
            
            string answer = allAnswers[i];
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = answer;
            
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(()=>CheckAnswer(answer));
        }
    }

    public void Shuffle(List<string> list)
    {
        //하나씩 돌면서 답들을 랜덤으로 집어넣기
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void CheckAnswer(string answer)
    {
        // 누른 버튼이 정답이면? 혹은 아니면?
        if (answer == currentCorrectAnswer)
        {
            ClearGame();
        }
        else
        {
            WrongAnswer();
        }
    }

    public void ClearGame()
    {
        
        if (!_clear)
        {
            _clear = true;
            ShowClearUI();
            StartCoroutine(WaitinTime());
        }
        
    }
    
    IEnumerator WaitinTime()
    {
        if(_clear == false) yield break;
        
        yield return new WaitForSeconds(1f);
        
        if (StageManager.instance != null)
        {
            StageManager.instance.MiniGameResult(true);
        }
    }

    public void WrongAnswer()
    {
        Debug.Log("실패지롱");
        if (StageManager.instance != null)
        {
            StageManager.instance.MiniGameResult(false);
        }
        SettingQuiz();
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
        rt.pivot = new Vector2(0.5f, 0f);         // 하단 중앙
        rt.anchoredPosition = Vector2.zero;       // 캔버스 하단 중앙
        rt.localScale = Vector3.zero;             // 초기 스케일 0
        
        // 3) Scale 애니메이션
        StartCoroutine(ScaleUp(rt, 0.5f));        // 0.5초 동안
    }
}
