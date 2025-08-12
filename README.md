# Tower Challenger!
**더 높은 곳으로 나아가는 성장형 아케이드 게임!!**

# 팀원소개 및 역할
- **김재의** : 팀장, 리드 기획자 , 미니 / 보스 게임 기획서 작성, 게임 UI/ 핵심 시스템 기획서 작성, 와이어 프레임 작성
- **박지훈** : 서브 기획자, 미니 / 보스 게임 기획서 작성, 게임 UI/ 핵심 시스템 기획서 작성, 와이어 프레임 작성
- **이해성** : 리드 개발자, 게임의 전반적인 시스템 개발, 미니 게임 병합 및 오류 수정 작업
- **최동근** : 개발자, 미니 게임 개발, 게임 내의 대다수 UI 작업, 게임 내의 시스템의 디테일 작업
- **차우진** : 개발자, 각종 미니 게임 개발 및 미니 게임 디테일 수정
- **장태현** : 개발자, 각종 미니 게임 개발 및 미니 게임 디테일 수정
- **최동혁** : 디자이너, 인게임 내의 대부분의 에셋들을 작업

### [🎞 시연 영상](https://www.youtube.com/watch?v=rJi4zztVWto)


# 게임 소개
![게임 시작 화면](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/02%20Images/StartBackGround/ChatGPT_Image_2025_6_27_09_20_12.png)
- **개발기간** : 6 / 20 ~ 8 / 11
- **플랫폼** : 모바일(Andriod)
- **타겟층** : 10 ~ 30 대 중 캐주얼 모바일 게임을 좋아히는 분들
- **조작방식** : 조이스틱, 버튼
- **게임 스토리** :
타워 챌린저는 캐주얼과 아케이드 장르가 결합된 성장형 게임입니다.
평화로운 마을에 갑작스러운 대지진과 함께 하늘로 솟아오른 신비한 거대한 타워가 등장하며, 전설에 따르면 정상에 오르면 운명을 바꿀 힘을 얻을 수 있다고 합니다.
수많은 종족과 모험가들이 각자의 목표를 품고 이 타워에 도전합니다.
플레이어는 다양한 미니게임을 클리어하며 점차 난이도가 올라가는 층을 올라가야 합니다.
## 핵심 기능
- **빌리지 시스템** : 타워에 오르기 전, 캐릭터 관리와 준비를 할 수 있는 생활 공간
- **타워 도전** : 층마다 다른 미니게임과 퍼즐이 존재하며, 제한 시간 내에 클리어 해야 다음 층으로 진입 가능
- **스태미나 & 입장권 시스템** : 도전에는 스테미나가 필요하며, 시간 경과로 충전되거나 아이템을 사용해야 도전 가능
- **다양한 미니게임** : 순발력, 창의력, 기억력의 여러 테마를 가지고 있는 미니게임들을 층별로 배치되어 지루할 틈 없이 도전 가능
- **NPC 및 스토리텔링** : 입구를 지키는 고양이 석상 **먀우라** 등 개성 있는 NPC들과 대화하며 세계관에 몰입


## 게임 미리보기
![Village](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EB%B9%8C%EB%A6%AC%EC%A7%80.gif)&nbsp;&nbsp;&nbsp;
![Tower Map](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%ED%94%8C%EB%A0%88%EC%9D%B4%EC%96%B4%20%EC%9D%B4%EB%8F%99.gif)


![MiniGame1](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EA%B3%B5%EC%A3%BC%EC%A7%80%ED%82%A4%EA%B8%B0.gif)&nbsp;&nbsp;&nbsp;
![MiniGame2](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%8A%AC%EB%9D%BC%EC%9E%84%20%ED%83%80%EC%9B%8C.gif)<br>


![BossGame1](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%A3%BC%EC%A0%95%EB%B1%85%EC%9D%B4%20%EC%95%84%EC%A0%80%EC%94%A8%20%EA%B2%8C%EC%9E%84.gif)<br>

# 기능 명세서

<details>
<summary>클라이언트 구조 요약</summary>

## 클라이언트 구조
<img width="1000" src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/Process.webp">

## JSON
![Untitled](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/JsonData.webp)

## 상호작용
<img width="1000" src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/Interaction.webp">

## 플레이어
![Untitled (1)](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/Player.webp)

## 스테이지
![Untitled (2)](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/Stage.webp)

## 빌리지
![Untitled (2)](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/Village.webp)
</details>


---


# 스크립트

<details>
<summary>스크립트 내용 요약</summary>


## **매니저**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [GameManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/GameManager.cs) | 타 매니저 및 정보 관리 | 이해성 |
| [UIManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/UIManager.cs) | UI 인스턴스 | 이해성 |
| [PlayerManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/PlayerManager.cs) | 플레이어 위치 저장 및 참초 할당 | 이해성, 차우진 |
| [SoundManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Sound/SoundManager.cs) | 배경 음악 및 효과음 | 이해성, 장태현 |
| [TowerManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/TowerManager.cs) | 인게임 내 씬전환 관리 | 이해성 |
| [StageManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/StageManager.cs)| 게임 상태 관리 및 스테이지 정보 관리 | 이해성 |
| [MiniGameManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/MiniGameManager.cs) | 미니게임 데이터 관리 | 이해성 |
| [Stamina](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Player/Stamina.cs) | 스테미나 관리 | 이해성 |
| [Account](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Player/Account.cs) | 재화 관리 | 이해성 |
| [Character](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Player/Character.cs) | 캐릭터 데이터 관리 | 이해성 |
| [PoolManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/PoolManager.cs) | 오브젝트 풀링 시스템을 관리하는 핵심 매니저 | 장태현 |

---
<br>

## **빌리지**
<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EB%B9%8C%EB%A6%AC%EC%A7%80.gif" width="400" height="400">
<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [Guild](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Village/Guild.cs) | 길드 관리 | 최동근
| [Inventory](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Village/Inventory.cs) | 인벤토리 관리 | 최동근
| [InventorySlot](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Village/InventorySlot.cs) | 인벤토리 슬롯관리 | 최동근
| [VillageManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Village/VillageManager.cs) | 빌리지 전반적인 관리 | 최동근
| [Store](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Village/Store.cs) | 상점 관리 | 최동근
| [PauseManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Village/PauseManager.cs) | 설정 관리 | 최동근
| [MailBox](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Village/MailBox.cs) | 우편함 관리 | 최동근
| [itemManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Village/ItemManager.cs) | 아이템데이터 관리 | 최동근
| [NPCBase](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Village/NPC/NPCBase.cs) | NPC 기본 관리 | 최동근

</details>


## **타워**

<details>
<summary>열기</summary>

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
[Arrow](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/Arrow.cs) | 상자 기믹 (투사체) | 이해성 |
[RewardManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/RewardTableData/RewardManager.cs) | 보상 관리 | 최동근
[RewardTavleData](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/RewardTableData/RewardTableData.cs) | 보상 CSV 파일 관련 변수 선언 | 최동근
| [StageLP](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/StageLP.cs) | 라이프포인트 관리 | 이해성 |
| [StageTimer](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/StageTimer.cs) | 타이머 관리 | 이해성 |
| [Trap](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/Trap.cs) | 트랩 | 이해성 |
| [Map](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/Map.cs) | 맵 상태 관리 | 이해성, 차우진 |
| [MapObstacle](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/MapObstacle.cs) | 맵 장애물 관리 | 이해성 |
| [DiffcultyObstacles](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/DiffcultyObstacles.cs) | 난이도 별 장애물 관리 | 이해성 |


---

### 튜토리얼

<details>
<summary>열기</summary>

| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [BoxTutorial](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Tutorial/BoxTutorial.cs) | 상자 튜토리얼 관리 | 최동근
| [DashTutorial](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Tutorial/DashTutorial.cs) | 대쉬 튜토리얼 관리 | 최동근
| [HintUI](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Tutorial/HintUI.cs) | 설명 UI 표출관리 | 최동근
| [InventortTutorial](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Tutorial/InventoryTutorial.cs) | 인벤토리 튜토리얼 관리 | 최동근
| [MoveTutorial](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Tutorial/MoveTutorial.cs) | 움직임 튜토리얼 관리 | 최동근
| [PortalTutorial](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Tutorial/PortalTutorial.cs) | 포탈 튜토리얼 관리 | 최동근
| [TutorialBase](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Tutorial/TutorialBase.cs) | 튜토리얼 기본 정보 관리 | 최동근
| [TutorialManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Tutorial/TutorialManager.cs) | 전반적인 튜토리얼 진행 관리 | 최동근
| [TutorialPortalOpen](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Tutorial/TutorialPortalOpen.cs) | 포탈 열림 기능 관리 | 최동근

</details>

---

### 스토리

<details>
<summary>열기</summary>

| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [StoryManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Story/StoryManager.cs) | 스토리 매니저 | 차우진 |
| [StoryUi](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Story/StoryUi.cs) | UI | 차우진 |
| [Story](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Story/Story.cs) | 6층 스토리 오브젝트 관리 | 차우진 |
| [Story_14Floor](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Story/Story_14Floor.cs) | 14층 스토리 오브젝트 관리 | 차우진 |
| [StoryTalk](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Story/StoryTalk.cs) | 스토리 대화 진행 | 차우진 |
| [StoryData](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Story/StoryData.cs) | 스토리 대화 데이터 | 차우진 |

</details>

---

</details>



## **미니게임**

<details>
<summary>열기</summary>

## 미니게임 슬라임 점프

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%8A%AC%EB%9D%BC%EC%9E%84%20%EC%A0%90%ED%94%84.png" width="400" height="400"/>

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [BackGroundPool](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/BackGroundPool.cs) | 배경 오브젝트 풀기능 | 최동근
| [BackGroundSpawner](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/BackGroundSpawner.cs) | 배경 스폰 | 최동근
| [CatController](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/CatController.cs) | 슬라임 점프기능 | 최동근
| [DeadZone](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/DeadZone.cs) | 슬라임 떨어질때 상태관련 | 최동근
| [FollowCamera](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/FollowCamera.cs) | 슬라임을 따라가는 카메라 기능 | 최동근
| [ObstacleBase](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/Obstacle/ObstacleBase.cs) | 장애물기본 데이터 | 최동근
| [DamageTile](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/Obstacle/DamageTile.cs) | 벽장애물 | 최동근
| [HorizontalMoverObstacle](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/Obstacle/HorizontalMoverObstacle.cs) | 수평 움직임 장애물 | 최동근
| [ObstaclePoolManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/Obstacle/ObstaclePoolManager.cs) | 장애물 오브젝트 풀 기능 | 최동근
| [ObvstacleSpawner](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/Obstacle/ObstacleSpawner.cs) | 장애물 스폰 | 최동근
| [SpinnerObstacle](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/Obstacle/SpinnerObstacle.cs) | 회전 장애물 | 최동근
| [ReMoveWall](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/RemoveWall.cs) | 벽 비활성 기능 | 최동근
| [RsetTable](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/ResetTable.cs) | 초기값관련 기능 | 최동근
| [SlimeJumpManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/SlimeJumpManager.cs) | 전반적인 게임 관여 | 최동근
| [WallPool](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/WallPool.cs) | 벽 오브젝트 풀 기능 | 최동근
| [WallSpawner](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/WallSpawner.cs) | 벽 스폰기능 | 최동근

</details>

---

## 미니게임 슬라임런

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%8A%AC%EB%9D%BC%EC%9E%84%EB%9F%B0.png" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [BackGround](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/DinoRun_Donggeun/BackGround.cs) | 배경 | 최동근
| [BackGroundLooper](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/DinoRun_Donggeun/BackGroundLooper.cs) | 배경루퍼기능 | 최동근
| [DinoMiniGame](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/DinoRun_Donggeun/DinoMiniGame.cs) | 전반적인 미니게임 관리 | 최동근
| [GroundTile](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/DinoRun_Donggeun/GroundTile.cs) | 땅타일생성 및 파괴 | 최동근
| [GroundTileSpawner](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/DinoRun_Donggeun/GroundTileSpawner.cs) | 땅 스폰기능 | 최동근
| [Obstacle](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/DinoRun_Donggeun/Obstacle.cs) | 장애물 | 최동근
| [PivotObject](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/DinoRun_Donggeun/PivotObject.cs) | 장애물 회전 | 최동근
| [Player](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/DinoRun_Donggeun/Player.cs) | 플레이어(슬라임 점프, 슬라이딩) | 최동근

</details>

## 미니게임 스피드 퀴즈

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%8A%A4%ED%94%BC%EB%93%9C%20%ED%80%B4%EC%A6%88.png" width="400" height="400">

<details>
<summary>열기</summary>  

| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [QuizBase](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SpeedQuizGame/QuizBase.cs) | CSV 파일 파싱 | 최동근
| [SpeedQuizData](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SpeedQuizGame/SpeedQuizData.cs) | CSV 파일 파싱관련 변수선언 |최동근
| [nonSenseGame](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SpeedQuizGame/nonSenseGame.cs) | 전반적인 스피드퀴즈 게임 관리 | 최동근

</details>

---

## 미니게임 슬라임 타워

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%8A%AC%EB%9D%BC%EC%9E%84%20%ED%83%80%EC%9B%8C.gif" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [Bird](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SlimeTower/Bird.cs) | 좌우 움직이는 새기능 | 최동근
| [Slime](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SlimeTower/Slime.cs) | 떨어지는 슬라임 | 최동근
| [SlimeTower](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SlimeTower/SlimeTower.cs) | 게임 전반을 관리하는 기능 | 최동근
| [CoolDownUI](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SlimeTower/CoolDownUI.cs) | 쿨타임 기능 | 이해성
| [BarSize](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SlimeTower/BarSize.cs) | 바닥 바 기능 | 이해성
  
</details>

---

## 미니게임 순발력 테스트

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%88%9C%EB%B0%9C%EB%A0%A5%20%ED%85%8C%EC%8A%A4%ED%8A%B8.png" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [MiniGameSpeedTest](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/MiniGameSpeedTest/MiniGameSpeedTest.cs) | 미니게임 순발력테스트 | 이해성 |
  
</details>

---

## 미니게임 똥피하기

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EB%98%A5%ED%94%BC%ED%95%98%EA%B8%B0.png" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [FallingBlockPlayer](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/FallingBlock/FallingBlockPlayer.cs) | 미니게임 똥피하기 | 이해성 |
  
</details>

## 미니게임 알게임

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%95%8C%EA%B2%8C%EC%9E%84.png" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [EggClickHandler](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/EggGame/EggClickHandler.cs) | 알 클릭 이벤트 처리 | 장태현 |
| [EggGameManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/EggGame/EggGameManager.cs) | 알게임의 나머지 모든 기능 관리 | 장태현 |
  
</details>

---

## 미니게임 어려운 게임

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%96%B4%EB%A0%A4%EC%9A%B4%EA%B2%8C%EC%9E%84.png" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [ReflectOnWall](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/HardGame/ReflectOnWall.cs) | 고양이의 방향 전환 처리 X축 좌우반전 | 장태현 |
| [ReflectOnWall2](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/HardGame/ReflectOnWall2.cs) | 고양이의 방향 전환 처리 위보고 시작하는 애 Y축 상하반전 | 장태현 |
| [ReflectOnWall3](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/HardGame/ReflectOnWall3.cs) | 고양이의 방향 전환 처리 아래보고 시작하는 애 Y축 상하반전 | 장태현 |
| [HardGamePlayer](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/HardGame/HardGamePlayer.cs) | 장애물 피격 시 해당 맵의 스폰 포인트로 플레이어 이동 | 장태현 |
| [HardGameGoalTrigger](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/HardGame/HardGameGoalTrigger.cs) | 장애물이 없을 경우 다음 층으로 이동 | 장태현 |
| [HardGameGameManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/HardGame/HardGameGameManager.cs) | 위 기능들을 제외한 모든 어려운 게임 기능 관리 | 장태현 |
  
</details>

---

## 미니게임 탄막 피하기 게임

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%ED%83%84%EB%A7%89%20%ED%94%BC%ED%95%98%EA%B8%B0%20%EA%B2%8C%EC%9E%84.png" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [Ball](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/ProGame/Ball.cs) | PoolManager에서 반환된 발사체 속도 설정 | 장태현 |
| [ProGamePlayer](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/ProGame/ProGamePlayer.cs) | 발사체 피격 시 무적 시간 부여 및 LP 감소 이벤트 발생 | 장태현 |
| [ProGameManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/ProGame/ProGameManager.cs) | 위 기능들을 제외한 모든 죽림고수 시스템 관리 | 장태현 |
  
</details>

---

## 미니게임 야바위 게임

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%95%BC%EB%B0%94%EC%9C%84%20%EA%B2%8C%EC%9E%84.png" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [SellGameManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SellGame/SellGameManager.cs) | 야바위 게임의 모든 기능 담당 | 장태현 |

</details>

---

## 미니게임 주정뱅이 게임

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%A3%BC%EC%A0%95%EB%B1%85%EC%9D%B4%20%EC%95%84%EC%A0%80%EC%94%A8%20%EA%B2%8C%EC%9E%84.gif" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| -- | -- | -- |
| [WalkTheStorkGameManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/WalkTheStork/WalkTheStorkGameManager.cs) | 주정뱅이 게임의 모든 기능 담당 | 장태현 |

</details>

---

## 미니게임 공주 지키기

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EA%B3%B5%EC%A3%BC%EC%A7%80%ED%82%A4%EA%B8%B0.gif" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| -- | -- | -- |
| [PrincessManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Princess/PrincessManager.cs) | 공주지키기 매니저 | 차우진 |
| [BatAttack](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Princess/BatAttack.cs) | 박쥐 공격 | 차우진 |
| [FireAttack](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Princess/FireAttack.cs) | 불덩이 공격 | 차우진 |
| [EnemyPos](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Princess/EnemyPos.cs) | 박쥐 및 불덩이 위치 생성 | 차우진 |
| [GameOver](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Princess/GameOver.cs) | 게임 오버 및 클리어 | 차우진 |
| [ShieldMove](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Princess/ShieldMove.cs) | 박쥐랑 불덩이 막는 플레이어 | 차우진 |

</details>

---

## 미니게임 그림자 맞추기

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EA%B7%B8%EB%A6%BC%EC%9E%90%20%ED%80%B4%EC%A6%88.png" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| -- | -- | -- |
| [ShadowManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Shadow/ShadowManager.cs) | 그림자맞추기 매니저 | 차우진 |
| [Shadow](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Shadow/Shadow.cs) | 문제 생성 | 차우진 |
| [ShadowUI](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Shadow/ShadowUI.cs) | UI | 차우진 |
| [ShadowData](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_Shadow/ShadowData.cs) | 그림자 및 이미지 데이터 | 차우진 |

</details>

---

## 미니게임 Up&Down

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%97%85%EC%95%A4%EB%8B%A4%EC%9A%B4.png" width="400" height="400">

<details>
<summary>열기</summary>
  
| 스크립트 이름 | 내용 | 당담자 |
| -- | -- | -- |
| [UpAndDownManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_UpAndDown/UpAndDownManager.cs) | 업다운숫자맞추기 매니저 | 차우진 |
| [UpAndDown](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_UpAndDown/UpAndDown.cs) | 숫자 생성 및 업다운 확인 | 차우진 |
| [UpAndDownUI](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/CWJ_UpAndDown/UpAndDownUI.cs) | UI | 차우진 |


</details>

---

</details>

## **플레이어**

<img src="https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%ED%94%8C%EB%A0%88%EC%9D%B4%EC%96%B4%20%EC%9D%B4%EB%8F%99.gif" width="400" height="400">

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [PlayerInput](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Player/PlayerInput.cs) | 플레이어 이동 | 이해성, 최동근 |
| [PlayerInteraction](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Player/PlayerInteraction.cs) | 플레이어 상호작용 | 이해성 |
| [PlayerBuff](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Player/PlayerBuff.cs) | 플레이어 디버프 상태 | 이해성 |
| [Interactable](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Interaction%20Object/NPC/Interactable.cs) | 상호작용 인터페이스 | 이해성 |
| [InteractionBox](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Interaction%20Object/NPC/InteractionBox.cs) | 상호작용 상자 | 이해성 |
| [InteractionPortal](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Interaction%20Object/NPC/InteractionPortal.cs) | 상호작용 포탈 | 이해성 |
| [PlayerCamera](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Player/PlayerCamera.cs) | 플레이어 카메라 관리 | 이해성 |
| [PlayerAnimation](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Player/PlayerAnimation.cs) | 플레이어 움직임 애니메이션 제어 | 장태현 |


## **Json**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [SaveManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/SaveManager.cs) | 세이브매니저 | 이해성 |
| [Save](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/SaveData/Save.cs) | 저장 유틸 메서드 | 이해성 |
| [PlayerData](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/SaveData/PlayerData.cs) | 저장 데이터 관리 | 이해성 |
| [CSVLoader](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/SaveData/CVSLoader.cs) | CSV 데이터 테이블 불러오기 | 이해성 |
| [StageTable](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Stage/StageTable.cs) | 스테이지 데이터 테이블 관리 | 이해성 |

</details>

---


<br>

###  사용 이미지 에셋

- [Pixel Art Top Down - Basic](https://assetstore.unity.com/packages/2d/environments/pixel-art-top-down-basic-187605)
- [Pixel Art Platformer - Village Props](https://assetstore.unity.com/packages/2d/environments/pixel-art-platformer-village-props-166114)
- 그 외 이미지 아트는 Chatgpt 와 최동혁님의 아트로 이뤄졌습니다.


### 사운드 에셋 사용
- [8-bit Fantasy & Adventure Music](https://assetstore.unity.com/packages/audio/music/electronic/8-bit-fantasy-adventure-music-211334)
- [8bit Quirky BGM](https://assetstore.unity.com/packages/audio/music/electronic/8bit-quirky-bgm-205076)
- [8Bit Music - 062022](https://assetstore.unity.com/packages/audio/music/8bit-music-062022-225623)
- [Retro-Fit Adventure (Free 8-Bit Video Game Music)](https://assetstore.unity.com/packages/audio/music/electronic/retro-fit-adventure-free-8-bit-video-game-music-205627)
- [Retro-Fit Adventure (Free 8-Bit Video Game Music)](https://assetstore.unity.com/packages/audio/music/electronic/retro-fit-adventure-free-8-bit-video-game-music-205627)
- [8Bit Music Album - 051321](https://assetstore.unity.com/packages/audio/music/8bit-music-album-051321-196147)
- [8-Bit RPG/Adventure Music Pack I](https://assetstore.unity.com/packages/audio/music/8-bit-rpg-adventure-music-pack-i-230766)
- [Fantasy Music Package](https://assetstore.unity.com/packages/p/fantasy-music-package-270899)
- [8 Bit Music Tracks}](https://assetstore.unity.com/packages/audio/music/8-bit-music-tracks-291369)
- [Ultimate UI Sound Effect Pack[SFX]](https://jdsherbert.itch.io/ultimate-ui-sfx-pack)




