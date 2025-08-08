using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Color color;

    public GameObject miniGameClearUI;
    public Canvas mainCanvas;

    private bool isInvincible;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        color = spriteRenderer.color;
        isInvincible = false;
    }

    public void Update()
    {
        if (PrincessManager.princessInstance.clear == false)
        {
            ClearStage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        if (isInvincible) return;

        if (StageManager.instance != null)
        {
            if (StageManager.instance.stageLP.currentLP <= 1)
            {
                StartCoroutine(Die());
            }
            else
            {
                StageManager.instance.MiniGameResult(false);
            }
        }
        StartCoroutine(Hit());
    }

    private IEnumerator Die()
    {
        SpriteSet();

        PrincessManager.princessInstance.enemyTime = 100;
        yield return new WaitForSeconds(2f);
        StageManager.instance.MiniGameResult(false);
    }

    private void SpriteSet()
    {
        if (PrincessManager.princessInstance.isDie) return;

        PrincessManager.princessInstance.isDie = true;
        animator.SetBool("isDie", true);

        Vector3 pos = this.transform.position;
        pos.y = -3f;

        this.transform.position = pos;
    }

    private IEnumerator Hit()
    {
        isInvincible = true;

        for (int i = 0; i < 2; i++)
        {
            color.a = 0;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.3f);

            color.a = 1;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.3f);
        }

        isInvincible = false;
    }

    private void ClearStage()
    {
        var p = PrincessManager.princessInstance;
        if (p.currentTime > p.clearTime)
        {

            if (!p.clear)
            {
                p.clear = true;

                ShowClearUI();
                StartCoroutine(WaitinTime());

                p.enemyTime = 100;
            }
        }
    }

    IEnumerator WaitinTime()
    {
        StageManager.instance.MiniGameResult(true);
        if (PrincessManager.princessInstance.clear == false) yield break;

        yield return new WaitForSeconds(1f);

        if (StageManager.instance != null)
            StageManager.instance.MiniGameResult(true);
    }

    IEnumerator ScaleUp(RectTransform rect, float duration)
    {
        float time = 0f;
        Vector3 from = Vector3.zero;
        Vector3 to = Vector3.one;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, time / duration);
            rect.localScale = Vector3.Lerp(from, to, t);
            yield return null;
        }

        rect.localScale = to;
    }

    void ShowClearUI()
    {
        GameObject miniGameClear = Instantiate(miniGameClearUI, mainCanvas.transform);
        miniGameClear.transform.SetAsLastSibling();

        var rt = miniGameClear.GetComponent<RectTransform>();
        rt.pivot = new Vector2(0.5f, 0.5f);         // 하단 중앙
        rt.anchoredPosition = Vector2.zero;       // 캔버스 하단 중앙
        rt.localScale = Vector3.zero;             // 초기 스케일 0

        // 3) Scale 애니메이션
        StartCoroutine(ScaleUp(rt, 0.5f));        // 0.5초 동안
    }
}
