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

    [System.Serializable]
    public class Quiz
    {
        public string question;
        public string answer;
        public List<string> wroungAnswers;
    }
    
    public List<Quiz> quizes = new List<Quiz>();
    private string currentCorrectAnswer;
    
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
        //선택 퀴즈를 랜덤으로 뽑기
        Quiz selectedQuiz = quizes[Random.Range(0, quizes.Count)];
        
        //퀴즈텍스트는 선택한 퀴즈로 & 현재 정답은 선택한 답으로
        quizText.text = selectedQuiz.question;
        currentCorrectAnswer = selectedQuiz.answer;
        
        //모든 답들은 리스트로 받아오기. 거기에 정답 추가시키기
        List<string> allAnswers = new List<string>(selectedQuiz.wroungAnswers);
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
        Debug.Log("성공");
    }

    public void WrongAnswer()
    {
        Debug.Log("실패지롱");
    }
}
