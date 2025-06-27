using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public GameObject gameOver;

    [Header("정보")]
    public StageTimer stageTimer;     //스테이지 타이머
    public StageLP stageLP;           //스테이지 LP
    public GameObject infoUI;         //각종 인포메이션이 들어갈 공간 (타이머 LP등)[추후 UI매니저로 이동]

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
    private int layerNumber;           //플레이어 레이어 저장
    private string currentSceneName;   //현재 씬 이름
    public List<int> stageClearPortal = new List<int>(); //여기에 활성화 되는 포탈 인덱스 값만 저장

    [Header("미니게임 데이터")]
    public MiniGameData[] miniGameDatas;    //미니게임 데이터
    private List<MiniGameData> randomGames = new List<MiniGameData>();  //랜덤으로 미니게임 배열이 들어갈 공간
    

    [SerializeField] string[] mapScenes;  //맵씬 모음

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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        isGameActive = false;
        if (infoUI  != null)
        infoUI.SetActive(isGameActive);
    }

    private void Update()
    {

    }
    #region MiniGameCall
    public void StartGame()
    {
        isGameOver = false;  //게임오버상태 초기화
        isGameActive = true;  //현재 게임 시작
        infoUI.SetActive(isGameActive);
        ResetInfo();
        stageLP.ResetLP();
        floor = 1;            //현재층 1층
        totalStageCount = 1;  //현재 스테이지 카운트 초기화
        timerMultiplier = 1f; //배속 초기화
        RandomStage(); // 게임 시작 시 랜덤 스테이지 생성
        LoadRandomMap();

        StartCoroutine(StartGameLoad());
        
        Debug.Log("게임시작");
        //다초기화하고 미니게임 장소로 이동
    }

    private IEnumerator StartGameLoad()
    {
        AsyncOperation ansynLoad = SceneManager.LoadSceneAsync(LoadRandomMap()); //어씬크
        while (!ansynLoad.isDone)
        {
            yield return null;
        }
        
        Map map = FindAnyObjectByType<Map>();
        map.SetRandomPortal();
    }

    //씬로드가 완료가 됬을때 동작

    private void ResetInfo()
    {
        stageCount = 0;
        stageTimer.SetTimer();
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

    public void NextFloor()  //다음층이 되었을때 시간초기화 및 스테이지 갯수 시간배속 계산
    {
        ResetInfo();
        stageCount = 0;
        floor++;

        if (floor % 5 == 1)  //5층마다 스테이지 갯수증가
            totalStageCount = Mathf.Min(totalStageCount + 1, 4);

        if (floor % 10 == 0) //10층마다 타이머 1.2배속
            timerMultiplier *= 1.2f;

        RandomStage();
        StartCoroutine(StartGameLoad());
    }
    #endregion
    #region MiniGameSet
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

    public string LoadRandomMap()
    {
        int randomSceneNum = Random.Range(0, mapScenes.Length);
        if (mapScenes == null)
        {
            return "";
        }
        string mapName = mapScenes[randomSceneNum];
        return mapName;
    }

    public void AddPortal(int portal)
    {
        stageClearPortal.Add(portal);
    }

    public void SaveClearPortal(int clear)
    {
        stageClearPortal.Remove(clear);
        
    }

    public void ResetClearPortal()
    {
        stageClearPortal.Clear();
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
            StartCoroutine(LatePlayerData());  //플레이어를 원레 위치로
        }
    }
    public void GameOver()  //게임오버가 되면 로비씬으로 이동
    {
        if (!isGameActive || isGameOver) //현재 게임이 실행상태가 아니거나 게임오버 상태가 아니라면 돌아가기
            return;

        if (stageTimer.timer <= 0 || stageLP.currentLP <= 0)  //타이머가 0이되거나 LP가 0이되면 게임오버
        {
            isGameOver = true;
            isGameActive = false;
            //infoUI.SetActive(isGameActive);
            if (floor > bestFloor) 
                bestFloor = floor;

            GameObject gameOverPanel =  Instantiate(gameOver, infoUI.transform);
        }
    }

    #endregion
    #region Player
    public void SavePlayerPosition(Vector3 position, int layer)
    { 
        layerNumber = layer;
        playerPosition = position;
    }
    private IEnumerator LatePlayerData() //플레이어를 찾아 해당 포지션으로 이동 (오류나서 코루틴으로 변경)
    {
        AsyncOperation ansynLoad = SceneManager.LoadSceneAsync(currentSceneName); //어씬크
        while (!ansynLoad.isDone)
        {
            yield return null;
        }

        if (stageClearPortal.Count != 0)
        {
            for (int i = 0; i < stageClearPortal.Count; i++)
            {
                Debug.Log(stageClearPortal[i]);
            }
        }

        if (stageClearPortal.Count == 0 && isGameActive)
        {
            Map map = FindAnyObjectByType<Map>();
            map.AllClearFloor();
        }

        GameObject player = null;
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");  //플레이어를 찾아서 
        }
        
        if (player != null)
        {
            player.layer = layerNumber;
            string LayerName;
            switch (layerNumber)
            {
                case 20:
                    LayerName = "Layer 1";
                    break;
                case 21:
                    LayerName = "Layer 2";
                    break;
                default:
                    LayerName = "Layer 3";
                    break;
            }
            player.GetComponent<SpriteRenderer>().sortingLayerName = LayerName;
            player.transform.position = playerPosition;   //플레이어 위치를 수정
        }
        if (isGameActive)
        {
            if (stageCount >= totalStageCount)
            {
                ResetClearPortal();
            }
        }
    }
    #endregion
}