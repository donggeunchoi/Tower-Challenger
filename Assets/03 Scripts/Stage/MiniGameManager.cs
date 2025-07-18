using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;

    [Header("미니게임 데이터")]
    public MiniGameDatas[] miniGameDatas;    //미니게임 데이터
    public List<MiniGameDatas> randomGames = new List<MiniGameDatas>();
    [SerializeField] private List<MiniGameDatas> unUsedGames = new List<MiniGameDatas>();

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void UnUsedGames()
    {
        unUsedGames.Clear();
        for (int i = 0; i < miniGameDatas.Length; i++)  //사용가능 한 미니게임 리스트 생성
        {
            MiniGameDatas game = miniGameDatas[i];
            if (!game.isBoss)
            {
                if (game.allStage)
                {
                    unUsedGames.Add(game);
                }
            }
        }
    }

    public void RandomStage()  //랜덤한 스테이지 생성
    {
        randomGames.Clear();

        List<MiniGameDatas> gameList = new List<MiniGameDatas>();  //사용가능한 배열생성
        for (int i = 0; i < miniGameDatas.Length; i++)  //사용가능 한 미니게임 리스트 생성
        {
            MiniGameDatas game = miniGameDatas[i];
            if (!game.isBoss)
            {
                if (game.allStage || (StageManager.instance.floor >= game.minStage && StageManager.instance.floor <= game.maxStage))
                {
                    gameList.Add(game);
                }
            }
        }

        if (gameList.Count == 0)
        {
            Debug.Log("사용가능한 게임이 없습니다/ gameList가 비었습니다");
            return;
        }

        if (unUsedGames.Count > 0)
        {
            if (unUsedGames.Count >= StageManager.instance.totalStageCount)
            {
                unUsedGames = unUsedGames.OrderBy(x => Random.value).ToList();
                for (int i = 0; i < StageManager.instance.totalStageCount; i++)
                {
                    randomGames.Add(unUsedGames[0]);
                    unUsedGames.RemoveAt(0);
                }
            }
            else
            {
                randomGames.AddRange(unUsedGames);  //남은거 다 추가하고
                if (randomGames.Count < StageManager.instance.totalStageCount)
                {
                    gameList = gameList.OrderBy(x => Random.value).ToList();  //랜덤으로 돌려놓고  

                    foreach (MiniGameDatas unused in unUsedGames)  //썻던게임은 뒤로보내고
                    {
                        if (gameList.Remove(unused))
                        {
                            gameList.Add(unused);
                        }
                    }
                    randomGames.AddRange(gameList.Take(Mathf.Min(gameList.Count, StageManager.instance.totalStageCount - randomGames.Count)));  //남은거만큼 추가

                    while (randomGames.Count < StageManager.instance.totalStageCount)  //그래도 모잘라면 중복허용
                    {
                        randomGames.Add(gameList[Random.Range(0, gameList.Count)]);
                    }
                    unUsedGames.Clear();
                }
            } 
        }
        else
        {
            gameList = gameList.OrderBy(x => Random.value).ToList();
            for (int k = 0; k < Mathf.Min(gameList.Count, StageManager.instance.totalStageCount); k++)
            {
                randomGames.Add(gameList[k]);
            }

            while (randomGames.Count < StageManager.instance.totalStageCount)
            {
                randomGames.Add(gameList[Random.Range(0, gameList.Count)]);
            }
        }
    }
    public void BossStage()
    {
        randomGames.Clear();

        if (UIManager.Instance != null)
        {
            if (UIManager.Instance.timerUI != null)
                UIManager.Instance.timerUI.ResetTimer();
        }

        List<MiniGameDatas> bossGameList = new List<MiniGameDatas>();  //사용가능한 배열생성
        for (int i = 0; i < miniGameDatas.Length; i++)  //사용가능 한 미니게임 리스트 생성
        {
            MiniGameDatas game = miniGameDatas[i];
            if (game.isBoss)
            {
                bossGameList.Add(game);
            }
        }

        if (StageManager.instance.floor == 0)
            return;

        int bossIndex = (StageManager.instance.floor / StageManager.BOSS_FLOOR) - 1;


        if (bossIndex >= 0 && bossIndex < bossGameList.Count)
        {
            randomGames.Add(bossGameList[bossIndex]);
        }
        else
        {
            if (bossGameList.Count > 0)
            {
                MiniGameDatas randomBoss = bossGameList[Random.Range(0, bossGameList.Count)];
                randomGames.Add(randomBoss);
            }
            else
            {
                RandomStage();
            }
        }
        StageManager.instance.StartGameLoad(true);
    }
}
