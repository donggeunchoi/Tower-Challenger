using Unity.VisualScripting;
using UnityEngine;

public class TestBtnClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  public void TestBtnClick1()
   {
        SoundManager.instance.PlaySound2D("Crit");
    }
    public void TestBtnClick2()
    {
        SoundManager.instance.PlaySound2D("DM-CGS-47");
    }
}
