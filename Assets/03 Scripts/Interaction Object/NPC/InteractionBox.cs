using System.Collections;
using UnityEngine;
using static BoxDataTable;

public enum BoxType
{
    None,
    GoldBox,
    ArrowBox,
    NekoManBox,
    LpBox
}  //우선 상자 타입을 나눠놓기

public class InteractionBox : MonoBehaviour, IInteractable
{
    public Animator animator;

    [SerializeField] private BoxType boxType;
    [SerializeField] private int reward;

    public string boxId;

    [SerializeField] private GameObject warningSign;
    [SerializeField] private GameObject goldSign;
    [SerializeField] private GameObject lpSign;


    [SerializeField] private Transform objectPosition;
    [SerializeField] private Arrow arrow;
    [SerializeField] private Arrow stone;

    private bool isIntract;

    private void Start()
    {
       
        isIntract = true;

        int currentFloor = StageManager.instance != null ? StageManager.instance.floor : 1;  // 기본값 1층

        var goldRow = BoxDataTable.goldBoxDataList.Find(r => r.floor == currentFloor);
        var nekoRow = BoxDataTable.nekoManDataList.Find(r => r.floor == currentFloor);
        var lpRow = BoxDataTable.plusLPDataList.Find(r => r.floor == currentFloor);
        var arrowRow = BoxDataTable.arrowDataList.Find(r => r.floor == currentFloor);

        int goldPer = goldRow != null ? goldRow.goldPer : 0;
        int nekoPer = nekoRow != null ? nekoRow.nekoPer : 0;
        int lpPer = lpRow != null ? lpRow.lpPer : 0;
        int arrowPer = arrowRow != null ? arrowRow.arrowPer : 0;

        int total = goldPer + lpPer + nekoPer + arrowPer;
        if (total == 0) total = 1;

        int rand = UnityEngine.Random.Range(1, total + 1);

        if (rand <= goldPer)
            boxType = BoxType.GoldBox;
        else if (rand <= goldPer + lpPer)
            boxType = BoxType.ArrowBox;
        else if (rand <= goldPer + lpPer + arrowPer)
            boxType = BoxType.NekoManBox;
        else
            boxType = BoxType.LpBox;
    }

    public void Interact()  //플레이어 상호작용시 포탈 타입에 맞춰 각각의 코드를 진행
    {
        if (!isIntract) return;
        if(SoundManager.instance != null)
         SoundManager.instance.PlaySound2D("OpenBox");
        switch (boxType)
        {
            case BoxType.GoldBox:
                if (StageManager.instance != null)
                    reward = goldBoxDataList.Find(r => r.floor == StageManager.instance.floor).goldRe;
                if (GameManager.Instance != null)
                    GameManager.Instance.account.AddGold(reward);
                SoundManager.instance.PlaySound2D("goldGetSFX");

                Debug.Log(reward);

                StartCoroutine(WarningCor(null, goldSign));
                break;
            case BoxType.ArrowBox:
                if (arrow != null)
                {
                    StartCoroutine(WarningCor(arrow, warningSign));
                    SoundManager.instance.PlaySound2D("warningSFX");
                }
                break;
            case BoxType.NekoManBox:
                if (stone != null)
                {
                    StartCoroutine(WarningCor(stone, warningSign));
                    SoundManager.instance.PlaySound2D("warningSFX");
                }
                break;
            default:
                if (StageManager.instance != null)
                {
                    int oneLP = plusLPDataList.Find(r => r.floor == StageManager.instance.floor).oneLPPer;
                    int twoLP = plusLPDataList.Find(r => r.floor == StageManager.instance.floor).twoLPPer;

                    int rand = UnityEngine.Random.Range(1, 101);

                    if (rand <= oneLP)
                    {
                        StageManager.instance.stageLP.HealLP(1);
                        StartCoroutine(WarningCor(null, lpSign));
                    }
                    else if (rand <= oneLP + twoLP)
                    {
                        StageManager.instance.stageLP.HealLP(2);
                        StartCoroutine(WarningCor(null, lpSign));
                    }
                    SoundManager.instance.PlaySound2D("lpGetSFX");
                }
                break;
        }
        isIntract = false;
        StartCoroutine(BoxDestroyCor());
    }

    public IEnumerator WarningCor(Arrow spawnArrow, GameObject sign)
    {
        if (sign != null)
        {
            float chestHeight = 1.0f; 
            float warningSignHeight = 0.45f;

            Vector3 warningPos = transform.position + Vector3.up * (chestHeight / 2f + warningSignHeight / 2f + 0.05f);

            GameObject warSig = Instantiate(sign, warningPos, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
            Destroy(warSig);
        }

        if (spawnArrow != null)
        {
            Arrow spawnStone = Instantiate(spawnArrow, this.transform.position, Quaternion.identity);
            spawnStone.Init(objectPosition);
        }
        yield return null;
    }

    public void DestroyBoxInfo()
    {
        MapInfo.StageTempMemory.destroyedInfo.destroyedChestIds.Add(boxId);
    }

    public IEnumerator BoxDestroyCor()
    {
        DestroyBoxInfo();

        if (animator != null)
            animator.SetBool("IsOpened", true);

        yield return new WaitForSeconds(1);

        Destroy(gameObject);

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectPosition = collision.transform;
        }
    }
}