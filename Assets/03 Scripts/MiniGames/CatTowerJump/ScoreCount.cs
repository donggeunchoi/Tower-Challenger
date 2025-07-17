using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform;
    public TextMeshProUGUI heightText;

    private float startY;
    [SerializeField]private float TargetY;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startY =playerTransform.position.y;
        UpdateText(0);
    }

    // Update is called once per frame
    void Update()
    {
        float currentY = playerTransform.position.y;
        if (currentY > TargetY)
        {
            Debug.Log("게임 클리어");
        }
    }
    private void UpdateText(float value)
    {
        // 소수점 없이 정수로만 표시하고 싶으면 Mathf.FloorToInt 사용
        int meters = Mathf.FloorToInt(value);
        heightText.text = $"Height: {meters} m";
    }
}
