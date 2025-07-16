using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color color;

    private void Start()
    {
        color = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        //if (StageManager.instance != null)
        //{
        //    if (StageManager.instance.stageLP.currentLP == 1)
        //    {
        //        StartCoroutine(Die());
        //    }
        //    else
        //    {
        //        StageManager.instance.MiniGameResult(false);
        //    }
        //}
        StartCoroutine(Hit());

    }

    private IEnumerator Die()
    {
        SpriteSet();
        yield return new WaitForSeconds(2f);
        StageManager.instance.MiniGameResult(false);
    }

    private void SpriteSet()
    {
        spriteRenderer.gameObject.SetActive(false);
        PrincessManager.princessInstance.spriteRenderer.gameObject.SetActive(true);
    }

    private IEnumerator Hit()
    {
        for (int i = 0; i < 3; i++)
        {
            color.a = 0;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.15f);

            color.a = 1;
            spriteRenderer.color = color;

            yield return new WaitForSeconds(0.15f);
        }
    }
}
