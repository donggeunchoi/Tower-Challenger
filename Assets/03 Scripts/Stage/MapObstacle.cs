using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class MapObstacle : MonoBehaviour
{

    [SerializeField] private Trap[] trabs;
    [SerializeField] private InteractionBox[] interactionBoxs;

    [SerializeField] private int difficulty;

    public void Init()
    {
        SetTrab();
        SetBox();
    }

    private void SetTrab()
    {
        if (StageManager.instance != null && StageTable.trapCountTableList.Find(x => x.floor == StageManager.instance.floor) != null)
        {
            int oneTrapPer = StageTable.trapCountTableList.Find(x => x.floor == StageManager.instance.floor).oneDisPer;
            int twoTrapPer = StageTable.trapCountTableList.Find(x => x.floor == StageManager.instance.floor).twoDisPer;
            int thrTrapPer = StageTable.trapCountTableList.Find(x => x.floor == StageManager.instance.floor).thrDisPer;
            int fourTrapPer = StageTable.trapCountTableList.Find(x => x.floor == StageManager.instance.floor).foDisPer;
            int fivTrapPer = StageTable.trapCountTableList.Find(x => x.floor == StageManager.instance.floor).fiveDisPer;

            int randomTrapMax = oneTrapPer + twoTrapPer + thrTrapPer + fourTrapPer + fivTrapPer;
            int randomTrap = Random.Range(1, randomTrapMax + 1);

            int trapCount;
            if (randomTrap < oneTrapPer)
                trapCount = 1;
            else if (randomTrap < oneTrapPer + twoTrapPer)
            {
                trapCount = 2;
            }
            else if (randomTrap < oneTrapPer + twoTrapPer + thrTrapPer)
            {
                trapCount = 3;
            }
            else if (randomTrap < oneTrapPer + twoTrapPer + thrTrapPer + fourTrapPer)
            {
                trapCount = 4;
            }
            else
            {
                trapCount = 5;
            }
            int activeTrabs = Random.Range(0, Mathf.Min(trapCount, trabs.Length));

            Trap[] shuffleTrabs = trabs.OrderBy(x => Random.value).ToArray();

            for (int i = 0; i < shuffleTrabs.Length; i++)
            {
                if (i < activeTrabs)
                {
                    shuffleTrabs[i].gameObject.SetActive(true);
                }
                else
                {
                    shuffleTrabs[i].DestroyTrapInfo();
                    shuffleTrabs[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void SetBox()
    {
        if (StageManager.instance != null && BoxDataTable.boxDataList.Find(x => x.floor == StageManager.instance.floor) != null)
        {
            int oneBoxPer = BoxDataTable.boxDataList.Find(x => x.floor == StageManager.instance.floor).oneBoxPer;
            int twoBoxPer = BoxDataTable.boxDataList.Find(x => x.floor == StageManager.instance.floor).thrBoxPer;
            int thrBoxPer = BoxDataTable.boxDataList.Find(x => x.floor == StageManager.instance.floor).thrBoxPer;

            int randomBoxMax = oneBoxPer + twoBoxPer + thrBoxPer;
            int randomBox = Random.Range(1, randomBoxMax + 1);

            int BoxCount;

            if (randomBox < oneBoxPer)
                BoxCount = 1;
            else if (randomBox < oneBoxPer + twoBoxPer)
            {
                BoxCount = 2;
            }
            else
            {
                BoxCount = 3;
            }

            InteractionBox[] shuffleBoxs = interactionBoxs.OrderBy(x => Random.value).ToArray();

            int activeBox = Random.Range(0, Mathf.Min(BoxCount, interactionBoxs.Length));

            for (int i = 0; i < shuffleBoxs.Length; i++)
            {
                if (i < activeBox)
                {
                    shuffleBoxs[i].gameObject.SetActive(true);
                }
                else
                {
                    shuffleBoxs[i].DestroyBoxInfo();
                    shuffleBoxs[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
