using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageLP : MonoBehaviour
{
    public const int DEFALT_LP = 4;
    public int bonusLP;
    public int currentLP;
    public Sprite brokenHeart;
    public Image[] heartIcon;
    private int beforLP;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    
    public void ResetLP()
    {
        currentLP = DEFALT_LP + bonusLP;
    }

    public void LPdown()
    {
        beforLP = currentLP;
        currentLP = Mathf.Max(currentLP - 1, 0);
        if (currentLP <= beforLP)
        {
            StartCoroutine(BrokenHeartImage());
        }
    }

    public void HealLP(int amount)
    {
        currentLP = Mathf.Min(currentLP + amount, DEFALT_LP + bonusLP);
    }

    private IEnumerator BrokenHeartImage()
    {
        if (heartIcon[currentLP])
        heartIcon[currentLP].sprite = brokenHeart;
        yield return new WaitForSeconds(0.2f);
        heartIcon[currentLP].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        heartIcon[currentLP].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        heartIcon[currentLP].gameObject.SetActive(false);
        yield return null;
    }
}