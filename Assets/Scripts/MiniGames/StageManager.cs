using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("정보")]
    public StageTimer stageTimer;     //스테이지 타이머
    public StageLP stageLP;           //스테이지 LP

    [Header("게임 상태")]
    public bool isGameActive = false;  //현재 게임이 실행되고 있는지 여부
    private bool isGameOver = false;  //게임오버가 됬는지

    [Header("진행 정보")]
    public int floor = 1;              //현재층
    public int bestFloor = 1;          //최고 기록
    public int stageCount = 0;         //현재 스테이지
    public int totalStageCount = 1;    //현재 깨야하는 스테이지
    private float timerMultiplier = 1f;//타이머 배속
    private Vector3 playerPosition;    //플레이어 포지션 저장
    private string currentSceneName;   //현재 씬 이름

    [Header("미니게임 데이터")]
    public MiniGameData[] miniGameDatas;    //미니게임 데이터
    private List<MiniGameData> randomGames = new List<MiniGameData>();  //랜덤으로 미니게임 배열이 들어갈 공간

    private const string _mainSceneName = "VillageScene";  //메인씬 이름

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isGameActive = false;  
    }

    public void StartGame()
    {
        isGameOver = false;  //게임오버상태 초기화
        isGameActive = true;  //현재 게임 시작
        stageLP.ResetLP();    //LP초기화
        stageTimer.SetTimer();//타이머 시작
        floor = 1;            //현재층 1층
        totalStageCount = 0;  //현재 스테이지 카운트 초기화
        timerMultiplier = 1f; //배속 초기화
        RandomStage(); // 게임 시작 시 랜덤 스테이지 생성


        Debug.Log("게임시작");
        //다초기화하고 미니게임 장소로 이동
    }

    public void SavePlayerPosition(Vector3 position)
    { 
         playerPosition = position;
    }

    private void RandomStage()  //랜덤한 스테이지 생성
    {
        randomGames.Clear();  // 랜덤 미니게임 배열 초기화

        List<MiniGameData> gameList = new List<MiniGameData>();  //사용가능한 배열생성
        for (int i = 0; i < miniGameDatas.Length; i++)
        {
            MiniGameData game = miniGameDatas[i];
            if (game.allStage || (floor >= game.minStage && floor <= game.maxStage))
            {
                gameList.Add(game);
            }
        }

        for (int i = 0; i < totalStageCount; i++)  //미니게임의 갯수를 비교해서
        {
            MiniGameData randomGames;

            if (gameList.Count > 0)  //미니게임이 더 많으면 중복 미니게임 방지
            {
                int randomNum = Random.Range(0, gameList.Count);
                randomGames = gameList[randomNum];
                gameList.RemoveAt(randomNum); 
            }
            else  //그렇지않으면 중복허용
            {
                int randomNum = Random.Range(0, miniGameDatas.Length);
                randomGames = miniGameDatas[randomNum];
            }
            this.randomGames.Add(randomGames);
        }
    }

    public void StartNextMiniGame()  //미니게임시작
    {
        currentSceneName = SceneManager.GetActiveScene().name;  //현재 씬 이름 가져오기

        RandomStage();  //돌린 배열 가져와서 실행
        if (randomGames.Count == 0)
        {
            return;
        }

        MiniGameData selectedGame = randomGames[Random.Range(0, randomGames.Count)];  //배열에서도 랜덤
        SceneManager.LoadScene(selectedGame.sceneName);  //해당배열에 있는 미니게임 실행
    }  

    public void MiniGameResult(bool result) //게임이 끝나면 호출해줘야 하는 함수
    {
        if (!result)  //실패
        {
            stageLP.LPdown();
            if (stageLP.currentLP <= 0) 
                GameOver();
        }
        else  //성공
        {
            SceneManager.LoadScene(currentSceneName);
            Debug.Log(playerPosition);
            StartCoroutine(LatePlayerPosition());  //플레이어를 원레 위치로
        }
    }

    private IEnumerator LatePlayerPosition() //플레이어를 찾아 해당 포지션으로 이동 (오류나서 코루틴으로 변경)
    {
        yield return new WaitForSeconds(0.1f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");  //플레이어를 찾아서 
        if (player) player.transform.position = playerPosition;   //플레이어 위치를 수정
    }

    public void GameOver()  //게임오버가 되면 로비씬으로 이동
    {
        if (!isGameActive || isGameOver) //현재 게임이 실행상태가 아니거나 게임오버 상태가 아니라면 돌아가기
            return;  

        if (stageTimer.timer <= 0 || stageLP.currentLP <= 0)  //타이머가 0이되거나 LP가 0이되면 게임오버

        isGameOver = true;
        isGameActive = false;
        if (floor > bestFloor) bestFloor = floor;
        SceneManager.LoadScene(_mainSceneName); //메인씬 로드
    }

    public void NextFloor()  //다음층이 되었을때 시간초기화 및 스테이지 갯수 시간배속 계산
    {
        floor++;                    //층 상승
        stageTimer.timer = 60f;     //타이머 초기화

        if (floor % 5 == 0)  //5층마다 스테이지 갯수증가
            totalStageCount = Mathf.Min(totalStageCount + 1, 4);

        if (floor % 10 == 0) //10층마다 타이머 1.2배속
            timerMultiplier *= 1.2f;

        RandomStage();
    }
}