using TMPro;
using UnityEngine;

public class HintUI : MonoBehaviour 
{
   public TMP_Text hintText;
   
   public void Show(string hint) 
   {
      hintText.text = hint;
      hintText.gameObject.SetActive(true);
   }
   
   public void Hide() 
   {
      hintText.gameObject.SetActive(false);
   }
}
