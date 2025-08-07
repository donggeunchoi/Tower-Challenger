using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPController : NPCBase
{
    protected override IEnumerator WaitBeforeTurn(bool turnToRight)
    {
        _isWaiting = true;
        
        _animator.SetBool("IsMove", false);

        if (!turnToRight)
        {
            talkImage.SetActive(true);
            RandomDescription();
        }
        else
        {
            talkImage.SetActive(false);
            GetComponent<Image>().enabled = false;
        }
        
        yield return new WaitForSeconds(npcData.StopDuration);
        
        GetComponent<Image>().enabled = true;
        _movingLeft = turnToRight;
        Flip(!turnToRight);
        _isWaiting = false;
        talkImage.SetActive(false);
    }
    
    private void RandomDescription()
    {
        if (npcData.npcDescription != null && npcData.npcDescription.Length > 0)
        {
            int randomIndex = Random.Range(0, npcData.npcDescription.Length);
            talkText.text = npcData.npcDescription[randomIndex];
            talkImage.SetActive(true);
        }
    }
}
