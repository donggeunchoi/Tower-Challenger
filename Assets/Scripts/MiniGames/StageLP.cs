using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageLP : MonoBehaviour
{
    public const int DEFALT_LP = 4;
    public int bonusLP;
    public int currentLP;
    public Sprite nomalHeart;
    public Sprite brokenHeart;
    public Image[] heartIcon;
    private int beforLP;
    private Coroutine brokenHeartCor;


    public void ResetLP()
    {
        currentLP = DEFALT_LP + bonusLP;
        for (int i = 0; i < heartIcon.Length; i++)
        {
            heartIcon[i].sprite = nomalHeart;
            heartIcon[i].gameObject.SetActive(true);
        }
    }

    public void LPdown()
    {
        beforLP = currentLP;
        currentLP = Mathf.Max(currentLP - 1, 0);

        if (currentLP <= beforLP)
        {
            if (brokenHeartCor != null)
            {
                StopCoroutine(brokenHeartCor);
                heartIcon[currentLP].gameObject.SetActive(false);
            }
            brokenHeartCor = StartCoroutine(BrokenHeartImage());
        }
    }

    public void HealLP(int amount)
    {
        currentLP = Mathf.Min(currentLP + amount, DEFALT_LP + bonusLP);
    }

    private IEnumerator BrokenHeartImage()
    {
        heartIcon[currentLP].sprite = brokenHeart;
        heartIcon[currentLP].gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        heartIcon[currentLP].gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        heartIcon[currentLP].gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        heartIcon[currentLP].gameObject.SetActive(false);

        brokenHeartCor = null;
    }
}