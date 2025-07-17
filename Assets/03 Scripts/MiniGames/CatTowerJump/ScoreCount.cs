using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform;
    public TextMeshProUGUI TargetText;
    public TextMeshProUGUI CurrnetText;

    private float startY;
    [SerializeField]private float TargetY;
    
    
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
            Debug.Log("게임 클리어");
        }
        
        CurrnetText.text = $"{currentY.ToString("N0")}m";
        
    }
    private void UpdateText(float value)
    {
        int currentY = Mathf.FloorToInt(value);
        TargetText.text = $"{currentY.ToString()}m";
    }
}
