using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeJumpManager : MonoBehaviour
{
    public static SlimeJumpManager Instance;
    
    [Header("References")]
    public Transform playerTransform;
    public TextMeshProUGUI TargetText;
    public TextMeshProUGUI CurrnetText;

    private float startY;
    [SerializeField]private float TargetY;
    
    
    private void Awake()
    {
       Instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startY =playerTransform.position.y;
        UpdateText(100);
    }

    // Update is called once per frame
    void Update()
    {
        float currentY = playerTransform.position.y;
        if (currentY > TargetY)
        {
            //여기가 게임 클리어
            // SlimeJumpManager.Instance.ClearGame();
        }
        
        CurrnetText.text = $"{currentY.ToString("N0")}m";
        
    }
    private void UpdateText(float value)
    {
        int currentY = Mathf.FloorToInt(value);
        TargetText.text = $"{currentY.ToString()}m";
    }


    public void ClearGame()
    {
        StageManager.instance.MiniGameResult(true);
        //이쪽에 다시 씬으로 넘기기
    }

    public void SlimeHit()
    {
        StageManager.instance.MiniGameResult(false);
        //여기에 패널 열어뿌지뭐
    }
}
