using System.Collections;
using UnityEngine;
using static BoxDataTable;

public enum ArrowType
{
    Arrow, Stone
}

public class Arrow : MonoBehaviour
{
    [SerializeField] ArrowType type;
    private Vector2 arrowDir;
    private float arrowSpeed = 7f;
    private float timing = 0.3f;

    private bool isInv;
    private bool hasHit;

    private float destoryDelay = 3f;

    public void Init(Transform playerTrans)
    {
        isInv = false;
        hasHit = false;

        switch (type)
        {
            case ArrowType.Arrow:
                if (StageManager.instance != null)
                {
                    arrowSpeed = BoxDataTable.arrowDataList.Find(r => r.floor == StageManager.instance.floor).arrowSP;
                    timing = BoxDataTable.arrowDataList.Find(r => r.floor == StageManager.instance.floor).timing;
                }
                break;
            case ArrowType.Stone:
                if (StageManager.instance != null)
                {
                    arrowSpeed = BoxDataTable.nekoManDataList.Find(r => r.floor == StageManager.instance.floor).neokSP;
                    timing = BoxDataTable.nekoManDataList.Find(r => r.floor == StageManager.instance.floor).timing;
                }
                break;
        }

        if (playerTrans != null)
        {
            arrowDir = (playerTrans.position - transform.position).normalized;

            float angle = Mathf.Atan2(arrowDir.y, arrowDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            StartCoroutine(ShotCor());
        }

        StartCoroutine(ShotCor());
    }

    private IEnumerator ShotCor()
    {
        yield return new WaitForSeconds(timing);

        isInv = true;

        float timer = 0f;
        while (timer < destoryDelay)
        {
            transform.position += (Vector3)(arrowDir * arrowSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || StageManager.instance != null)
        {
            if (hasHit) return;
            if (!isInv) return;

            hasHit = true;

            switch (type)
            {
                case ArrowType.Arrow:
                    StageManager.instance.MiniGameResult(false);
                    StageManager.instance.MiniGameResult(false);
                    break;
                case ArrowType.Stone:
                    StageManager.instance.MiniGameResult(false);
                    break;
            }
        }
    }
}
