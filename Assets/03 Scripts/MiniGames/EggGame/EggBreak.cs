using UnityEngine;
using System.Collections;

public class EggBreak : MonoBehaviour
{
    public void EggBreakPC()
    {     
        StartCoroutine(ReturnToPoolAfterSeconds());
    }
    private IEnumerator ReturnToPoolAfterSeconds()
    {
        yield return new WaitForSeconds(3f);
        PoolManager.Instance.ReturnObject(this.gameObject);
    }
}
