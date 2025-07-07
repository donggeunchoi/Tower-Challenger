using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class NPController : MonoBehaviour
{
    public NPCData npcData;
    
    private float startPositionX;
    private bool movingLeft = true;

    void Start()
    {
        startPositionX = transform.localPosition.x;
    }

    void Update()
    {
        float newX = transform.localPosition.x;

        if (movingLeft)
        {
            newX -= npcData.MoveSpeed*Time.deltaTime;
            if (newX <= startPositionX - npcData.MoveDistance)
            {
                movingLeft = false;
                Flip(true);
            }
        }
        else
        {
            newX += npcData.MoveSpeed*Time.deltaTime;
            if (newX >= startPositionX + npcData.MoveDistance)
            {
                movingLeft = true;
                Flip(false);
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
    
}
