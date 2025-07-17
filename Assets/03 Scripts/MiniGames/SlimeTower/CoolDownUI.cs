using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    public GameObject[] slotObjs;
    public Image[] slimeCooldownImgs;
    public SlimeTower slimeTower;

    void Update()
    {
        if (slimeTower == null || slotObjs == null || slimeCooldownImgs == null)
            return;

        int maxStack = slotObjs.Length;
        int curStack = Mathf.Clamp(slimeTower.currentStack, 0, maxStack);

        for (int i = 0; i < maxStack; i++)
        {
            bool active = (i < curStack) || (i == curStack && curStack < maxStack);
            slotObjs[i].SetActive(active);

            if (!active) continue;

            if (slimeCooldownImgs[i] != null)
            {
                if (i < curStack)
                {
                    slimeCooldownImgs[i].fillAmount = 1f;
                }
                else if (i == curStack && curStack < maxStack)
                {
                    float ratio = Mathf.Clamp01(slimeTower.stackTimer / slimeTower.coolDown);
                    slimeCooldownImgs[i].fillAmount = ratio;
                }
            }
        }
    }
}
