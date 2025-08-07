using System.Collections;
using TMPro;
using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    public NPCData npcData;
    public GameObject talkImage;
    public TMP_Text talkText;
    
    protected Animator _animator;
    protected float _startPositionX;
    protected bool _movingLeft = true;
    protected bool _isWaiting = false;
    
    protected virtual void Start()
    {
        _startPositionX = transform.localPosition.x;
       _animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (_isWaiting) return;

        _animator.SetBool("IsMove", true);
        float newX = transform.localPosition.x;

        if (_movingLeft)
        {
            newX -= npcData.MoveSpeed * Time.deltaTime;
            if (newX <= _startPositionX - npcData.MoveDistance)
            {
                _movingLeft = false;
                StartCoroutine(WaitBeforeTurn(false));
            }
        }
        else
        {
            newX += npcData.MoveSpeed * Time.deltaTime;
            if (newX >= _startPositionX + npcData.MoveDistance)
            {
                _movingLeft = true;
                StartCoroutine(WaitBeforeTurn(true));
            }
        }

        transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
    }

    protected void Flip(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceLeft ? -1 : 1;
        transform.localScale = scale;
    }

    protected virtual IEnumerator WaitBeforeTurn(bool turnToRight)
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
        }

        yield return new WaitForSeconds(npcData.StopDuration);

        _movingLeft = turnToRight;
        Flip(!turnToRight);
        _isWaiting = false;
        talkImage.SetActive(false);
    }

    protected virtual void RandomDescription()
    {
        if (npcData.npcDescription != null && npcData.npcDescription.Length > 0)
        {
            int randomIndex = Random.Range(0, npcData.npcDescription.Length);
            talkText.text = npcData.npcDescription[randomIndex];
            talkImage.SetActive(true);
        }
    }
}
