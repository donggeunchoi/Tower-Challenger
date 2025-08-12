# Tower Challenger!
**더 높은 곳으로 나아가는 성장형 아케이드 게임!!**

# 팀원소개 및 역할
- **김재의** : 팀장, 리드 기획자 , 미니 / 보스 게임 기획서 작성, 게임 UI/ 핵심 시스템 기획서 작성, 와이어 프레임 작성
- **박지훈** : 서브 기획자, 미니 / 보스 게임 기획서 작성, 게임 UI/ 핵심 시스템 기획서 작성, 와이어 프레임 작성
- **이해성** : 리드 개발자, 게임의 전반적인 시스템 개발, 미니 게임 병합 및 오류 수정 작업
- **최동군** : 개발자, 미니 게임 개발, 게임 내의 대다수 UI 작업, 게임 내의 시스템의 디테일 작업
- **차우진** : 개발자, 각종 미니 게임 개발 및 미니 게임 디테일 수정
- **장태현** : 개발자, 각종 미니 게임 개발 및 미니 게임 디테일 수정
- **최동혁** : 디자이너, 인게임 내의 대부분의 에셋들을 작업

# 게임 미리보기
![Village](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EB%B9%8C%EB%A6%AC%EC%A7%80.gif)&nbsp;&nbsp;&nbsp;
![Tower Map](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%ED%94%8C%EB%A0%88%EC%9D%B4%EC%96%B4%20%EC%9D%B4%EB%8F%99.gif)<br>


![MiniGame1](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EA%B3%B5%EC%A3%BC%EC%A7%80%ED%82%A4%EA%B8%B0.gif)&nbsp;&nbsp;&nbsp;
![MiniGame2](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%8A%AC%EB%9D%BC%EC%9E%84%20%ED%83%80%EC%9B%8C.gif)<br>


![BossGame1](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/06%20ReadMeImage/IMAGIF/%EC%A3%BC%EC%A0%95%EB%B1%85%EC%9D%B4%20%EC%95%84%EC%A0%80%EC%94%A8%20%EA%B2%8C%EC%9E%84.gif)<br>
## 게임 소개
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

## **매니저**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
[BackGroundPool](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/BackGroundPool.cs) | 배경 오브젝트 풀기능 | 최동근|

## **스테이지**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
[BackGroundPool](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/BackGroundPool.cs) | 배경 오브젝트 풀기능 | 최동근|

## **빌리지**

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

---
## **타워**

<details>
<summary>열기</summary>

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
[RewardManager](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/RewardTableData/RewardManager.cs) | 보상 관리 | 최동근
[RewardTavleData](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/RewardTableData/RewardTableData.cs) | 보상 CSV 파일 관련 변수 선언 | 최동근

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

</details>

---

## **미니게임**

<details>
<summary>열기</summary>

## 미니게임 슬라임 점프1

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

## 미니게임 슬라임런2

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

## 미니게임 스피드 퀴즈3

<details>
<summary>열기</summary>  

| 스크립트 이름 | 내용 | 당담자 |
| --- | --- | --- |
| [QuizBase](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SpeedQuizGame/QuizBase.cs) | CSV 파일 파싱 | 최동근
| [SpeedQuizData](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SpeedQuizGame/SpeedQuizData.cs) | CSV 파일 파싱관련 변수선언 |최동근
| [nonSenseGame](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/SpeedQuizGame/nonSenseGame.cs) | 전반적인 스피드퀴즈 게임 관리 | 최동근

</details>

---

## 미니게임 슬라임 타워4

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

</details>

## **Json**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
[BackGroundPool](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/MiniGames/CatTowerJump/BackGroundPool.cs) | 배경 오브젝트 풀기능 | 최동근|

---


<br>
<br>

# 4. 사용 에셋

- [KUBIKOS - 3D Cube World](https://assetstore.unity.com/packages/3d/environments/kubikos-3d-cube-world-117341)
- [Fantasy Skybox FREE](https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353)
- [Interactive Physical Door Pack](https://assetstore.unity.com/packages/tools/physics/interactive-physical-door-pack-163249)
- [Forest - Low Poly Toon Battle Arena / Tower Defense Pack](https://assetstore.unity.com/packages/3d/environments/forest-low-poly-toon-battle-arena-tower-defense-pack-100080)
- [URP Stylized Water Shader - Proto Series](https://assetstore.unity.com/packages/vfx/shaders/urp-stylized-water-shader-proto-series-187485)
- [Fantasy Wooden GUI : Free](https://assetstore.unity.com/packages/2d/gui/fantasy-wooden-gui-free-103811)
- [3D The Blacksmith's House](https://assetstore.unity.com/packages/3d/environments/fantasy/3d-the-blacksmith-s-house-252972)
- [Magic Effects FREE](https://assetstore.unity.com/packages/vfx/particles/spells/magic-effects-free-247933)
- [The Portal Collection](https://assetstore.unity.com/packages/3d/environments/fantasy/the-portal-collection-205438)
- [Epic Toon VFX 2](https://assetstore.unity.com/packages/vfx/particles/spells/epic-toon-vfx-2-157651)
- [Nature Sound FX](https://assetstore.unity.com/packages/audio/sound-fx/nature-sound-fx-180413)
- [Toon Fantasy Nature](https://assetstore.unity.com/packages/3d/environments/landscapes/toon-fantasy-nature-215197)
