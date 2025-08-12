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

## 기획 의도
타워 챌린저는 단순한 클리어 목표 외에도 플레이어가 성장과 준비를 체감할 수 있는 구조를 지향합니다<br>
미니게임마다 다른 난이도와 장를 배치해 반복 플레이에서도 신선함을 유지하며, 게임 내 시간과 자원 관리를 통한 전략적 요소를 가미했습니다<br>
또한, **독특한 세계관과 캐릭터**를 통해 유저가 게임 속 이야기에 몰입하고 지속적으로 도전 의욕을 잃지 않도록 설계 했습니다.<br>

# 3. 기능 명세서

<details>
<summary>UML, 기능 정리</summary>

#### 클라이언트 구조
<img width="1000" src="https://github.com/dlghdwns97/TSEROF_Code/assets/73785455/064d53e4-f6ff-4b3e-b92c-6ce5fdf6596a">

#### JSON
![Untitled](https://github.com/dlghdwns97/TSEROF_Code/assets/73785455/35f9aa2a-85c8-45c9-a4db-edcc2e458789)

#### 상호작용
<img width="1000" alt="어진이와_아이들_(3)" src="https://github.com/dlghdwns97/TSEROF_Code/assets/73785455/3182fc8c-3fa6-4227-b8b9-fd398cc5f4db">

#### 플레이어
![Untitled (1)](https://github.com/dlghdwns97/TSEROF_Code/assets/73785455/88c687d6-285b-4044-a282-988cb3b34639)

#### 스테이지
![Untitled (2)](https://github.com/dlghdwns97/TSEROF_Code/assets/73785455/5a8e1a38-98ea-4857-b238-3e43e15446b3)

#### 빌리지
![Untitled (2)](https://github.com/dlghdwns97/TSEROF_Code/assets/73785455/5a8e1a38-98ea-4857-b238-3e43e15446b3)
</details>

## **매니저**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [GameManager](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Manager/GameManager.cs) | 게임 매니저 | 김형중 |
| [SoundManager](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Manager/SoundManager.cs) | 사운드 매니저 | 김형중 |
| [Stage2Manager](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Manager/Stage2Manager.cs) | Stage 2 관리 | 정재훈, 박지원 |
| [StartStoryUI](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Manager/StartStoryUI.cs) | 스토리 관리 | 김형중 |
| [GimmickForObject](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/GimmickForObject.cs) | Stage 3 관리 | 이홍준 |
| [Stamina](https://github.com/donggeunchoi/Tower-Challenger/blob/main/Assets/03%20Scripts/Player/Stamina.cs) | 스테미나 생성 및 저장 | 이해성 |

## **Scene 변경 관련**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [AsyncLoading](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/ChangeScene/AsyncLoading.cs) | 로딩창 | 김어진 |
| [ChangeSceneManager](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/ChangeScene/ChangeSceneManager.cs) | 스테이지 선택 매니저 | 김형중 |
| [Stage1ClearCutScene](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/ChangeScene/Stage1ClearCutScene.cs) | 스테이지 1 클리어 화면 | 김어진 |

## **시작 화면**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [ConfirmationPopupMenu](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/StartScene/ConfirmationPopupMenu.cs) | 확인 버튼 메뉴 | 김어진 |
| [MainMenu](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/StartScene/MainMenu.cs) | 메인 메뉴 관리 | 김어진 |
| [Menu](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/StartScene/Menu.cs) | 메뉴 버튼 강조 | 김어진 |
| [SaveSlot](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/StartScene/SaveSlot.cs) | 세이브 슬롯 관리 | 김어진 |
| [SaveSlotsMenu](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/StartScene/SaveSlotsMenu.cs) | 세이브 슬롯 메뉴 | 김어진 |

## **스테이지 선택 화면**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [BgmController](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/StageSelect/BgmController.cs) | 배경음악 조절 | 김형중 |
| [MoveSelect](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/StageSelect/MoveSelect.cs) | 캐릭터 선택 화면 총괄 | 김형중 |
| [OptionMenu](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/StageSelect/OptionMenu.cs) | 옵션 창 | 김형중 |

## **데이터 저장(JSON)**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [DataPersistenceManager](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/DataPersistence/DataPersistenceManager.cs) | JSON 데이터 총괄 매니저 | 김어진 |
| [FileDataHandler](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/DataPersistence/FileDataHandler.cs) | JSON 데이터 핸들러 | 김어진 |
| [IDataPersistence](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/DataPersistence/IDataPersistence.cs) | JSON 데이터 불러오기/저장 관리 | 김어진 |
| [GameData](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/Data/GameData.cs) | JSON 데이터 | 김어진 |
| [HiddenItem](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/HiddenItems/HiddenItem.cs) | 히든 아이템 관리 | 김어진 |
| [SerializableDictionary](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/HiddenItems/SerializableTypes/SerializableDictionary.cs) | 히든 아이템 정보를 저장하는 딕셔너리 | 김어진 |
| [PuzzleParticle](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Json/HiddenItems/PuzzleParticle.cs) | 스테이지 별 퍼즐 파티클 | 김형중 |

## **캐릭터**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [Player](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Player/Player.cs) | 플레이어 총괄 | 박지원 |
| [ForceReceiver](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Player/ForceReceiver.cs) | 플레이어 점프 및 상태 관리 | 박지원 |
| [PlayerInput](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Player/PlayerInput.cs) | 플레이어 이동 가능상태 변경 | 박지원 |
| [Respawn](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Player/Respawn.cs) | 플레이어 리스폰 | 이홍준 |
| [RunSFX](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Player/RunSFX.cs) | 지형별 발걸음 소리 | 김형중 |
| [TopViewPlayer](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Player/TopViewPlayer.cs) | 탑뷰에서의 플레이어 | 박지원 |
| [JumpEffect](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Player/JumpEffect/JumpEffect.cs) | 점프 효과 | 김형중 |
| [ObjectPoolJump](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Player/JumpEffect/ObjectPoolJump.cs) | 점프 효과에 필요한 오브젝트 풀링 | 김형중 |

## **카메라**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [CamChange](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Camera/CamChange.cs) | 스테이지 1 카메라 회전 관리 | 김형중 |
| [FollowCam](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Camera/FollowCam.cs) | 스테이지 1 카메라 접근 관리 | 김형중 |
| [TrackingZone](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Camera/TrackingZone.cs) | 스테이지 1 카메라 이동 루트 관리 | 김형중 |
| [CamPos](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Camera/CamPos.cs) | 스테이지 2 카메라 회전 관리 | 정재훈 |

## **기믹**

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [JumpMush](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage1/JumpMush.cs) | 캐릭터를 점프시키는 점프대 | 이홍준 |
| [Log](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage1/Log.cs) | 캐릭터가 밟으면 잠시 후 떨어지는 발판 | 이홍준 |
| [LogChild](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage1/LogChild.cs) | Log 오브젝트에 신호 전달 | 이홍준 |
| [Obstacles](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage1/Obstacles.cs) | 캐릭터가 닿으면 밀쳐버리는 토네이도 | 정재훈, 박지원 |

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [CubeType](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/CubeType.cs) | 큐브 블록 속성 | 정재훈 |
| [FallingObject](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/FallingObject.cs) | 떨어지는 고드름 | 정재훈 |
| [FireTrap](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/FireTrap.cs) | 화염을 뿜는 트랩 | 박지원, 정재훈 |
| [Hammer](https://github.com/KimEoJin24/TSEROF/blob/main/TSEROF/Assets/Scripts/Gimmick/Hammer.cs) | 돌아가면서 플레이어를 공격하는 해머 | 정재훈 |
| [LaserPatternManager](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/LaserPatternManager.cs) | 레이저 패턴을 관리하는 매니저 | 박지원 |
| [LaserReceiver](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/LaserReceiver.cs) | 레이저 수신부 | 박지원 |
| [LaserTransmitter](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/LaserTransmitter.cs) | 레이저 발신부 | 박지원 |
| [Leaf](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/Leaf.cs) | 나뭇잎 발판 | 박지원 |
| [Lebu](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/Lebu.cs) | 레버를 이용한 입구 | 정재훈, 박지원 |
| [LebuMoving](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/LebuMoving.cs) | 레버에 따른 입구 이동통로 생성 | 정재훈 |
| [MovingPlatform](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/MovingPlatform.cs) | 웨이포인트에 따른 이동 발판 | 정재훈, 박지원 |
| [WaypointPath](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/WaypointPath.cs) | 이동 발판 웨이포인트 지정 | 정재훈 |
| [NeedleTrap](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/NeedleTrap.cs) | 가시 트랩 | 박지원 |
| [RotationCube](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/RotationCube.cs) | 큐브 움직임 | 정재훈 |
| [RotationObstacle](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/RotationObstacle.cs) | 움직이는 회전 장애물 | 정재훈 |
| [SideNeedleTraps](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/SideNeedleTraps.cs) | 지정된 곳에 가시 이동 | 정재훈 |
| [Transparent](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/Transparent.cs) | 투명한 가시벽 설정 | 정재훈 |
| [Transparents](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/Transparents.cs) | 전체적인 가시벽을 관리 | 정재훈, 박지원 |
| [TransparentObject](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage2/TransparentObject.cs) | 닿으면 드러나는 투명한 오브젝트 | 박지원, 정재훈 |

| 스크립트 | 내용 | 당담자 |
| -- | -- | -- |
| [Wind](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage3/Wind/Wind.cs) | 캐릭터를 특정 방향으로 밀어내는 바람 구역 | 이홍준 |
| [PopBlock](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage3/PopBlock.cs) | 캐릭터를 더 높게 점프시키는 일회용 점프대 | 이홍준 |
| [BallCannon](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage3/BallCannons/BallCannon.cs) | 지정한 위치로 공을 쏘는 캐논 | 이홍준 |
| [ChangeBtnColor](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage3/LinePlatform/ChangeBtnColor.cs) | 발판을 움직이는 버튼의 시각적 효과 | 이홍준 |
| [PlatformRespawn](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage3/LinePlatform/PlatformRespawn.cs) | 리스폰 될 때 발판 위치를 제자리로 | 이홍준 |
| [PressBtn](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage3/LinePlatform/PressBtn.cs) | 버튼으로 움직이는 발판 | 이홍준 |
| [PillarX](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage3/PillarX.cs) | X축으로 움직이는 기둥 | 이홍준 |
| [PillarZ](https://github.com/dlghdwns97/TSEROF_Code/blob/main/Scripts/Gimmick/Stage3/PillarZ.cs) | Z축으로 움직이는 기둥 | 이홍준 |

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
