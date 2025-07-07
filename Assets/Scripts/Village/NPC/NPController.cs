using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class NPController : MonoBehaviour
{
    public NPCData npcData;
    
    private float startPositionX;
    private bool movingLeft = true;
    private bool isWaiting = false;

    void Start()
    {
        startPositionX = transform.localPosition.x;
    }

    void Update()
    {
        //나중에 여기에 대사 집어넣기.
        if (isWaiting) return;
        
        float newX = transform.localPosition.x;

        if (movingLeft)
        {
            newX -= npcData.MoveSpeed*Time.deltaTime;
            if (newX <= startPositionX - npcData.MoveDistance)
            {
                movingLeft = false;
                Flip(true);
                StartCoroutine(WaitBeforeTurn(false));
            }
        }
        else
        {
            newX += npcData.MoveSpeed*Time.deltaTime;
            if (newX >= startPositionX + npcData.MoveDistance)
            {
                movingLeft = true;
                Flip(false);
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

    private System.Collections.IEnumerator WaitBeforeTurn(bool turnToLeft)
    {
        isWaiting = true;
        
        yield return new WaitForSeconds(npcData.StopDuration);
        
        movingLeft = turnToLeft;
        Flip(!turnToLeft);
        isWaiting = false;
    }
}
