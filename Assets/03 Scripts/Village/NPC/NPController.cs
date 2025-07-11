using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPController : MonoBehaviour
{
    public NPCData npcData;
    public GameObject talkImage;
    public TMPro.TMP_Text talkText;
    
    private float _startPositionX;
    private bool _movingLeft = true;
    private bool _isWaiting = false;

    void Start()
    {
        _startPositionX = transform.localPosition.x;
    }

    void Update()
    {
        //나중에 여기에 대사 집어넣기.
        if (_isWaiting)
        {
            return;
        }
        
        float newX = transform.localPosition.x;

        if (_movingLeft)
        {
            newX -= npcData.MoveSpeed*Time.deltaTime;
            if (newX <= _startPositionX - npcData.MoveDistance)
            {
                _movingLeft = false;
                StartCoroutine(WaitBeforeTurn(false));
            }
        }
        else
        {
            newX += npcData.MoveSpeed*Time.deltaTime;
            if (newX >= _startPositionX + npcData.MoveDistance)
            {
                _movingLeft = true;
                StartCoroutine(WaitBeforeTurn(true));
            }
        }
        transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
    }

    private void Flip(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? -1 : 1;
        transform.localScale = scale;
    }

    private IEnumerator WaitBeforeTurn(bool turnToRight)
    {
        _isWaiting = true;

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
