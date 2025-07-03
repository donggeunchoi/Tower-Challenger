using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
   public static ItemManager instance;
   public Transform SlotParent;
   public GameObject SlotPrefab;
   
   public List<string> itemNames = new List<string>();
   public List<Sprite> itemIcons = new List<Sprite>();

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         DontDestroyOnLoad(this);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   private void Start()
   {
      SlotParent = GameObject.Find("Content").transform;
   }
   
   

   public void AddItem(string name, Image image)
   {
      GameObject newSlot = Instantiate(SlotPrefab, SlotParent);
      
      newSlot.SetActive(true);
      itemNames.Add(name);
      itemIcons.Add(image.sprite);

      newSlot.GetComponentInChildren<TMPro.TMP_Text>().text = name;
      newSlot.GetComponent<InventorySlot>().itemName = name;
      newSlot.GetComponent<Image>().sprite = image.sprite;
   }
}
