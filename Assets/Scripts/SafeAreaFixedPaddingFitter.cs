using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaWithRelativePaddingFitter : MonoBehaviour
{
    [Tooltip("Safe Area 기준 패딩 비율 (0.05 = 5%)")]
    public float safeAreaPaddingRatio = 0.05f;

    private ScreenOrientation lastOrientation;

    void Start()
    {
        lastOrientation = Screen.orientation;
        ApplySafeArea();
    }

    void Update()
    {
        if (Screen.orientation != lastOrientation)
        {
            ApplySafeArea();
            lastOrientation = Screen.orientation;
        }
    }

    void ApplySafeArea()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Rect safeArea = Screen.safeArea;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Safe Area의 크기
        float safeWidth = safeArea.width;
        float safeHeight = safeArea.height;

        // Safe Area 기준 5% 패딩(픽셀 단위)
        float padX = safeWidth * safeAreaPaddingRatio;
        float padY = safeHeight * safeAreaPaddingRatio;

        // Anchor 계산 (스크린 기준 정규화)
        float anchorMinX = (safeArea.xMin + padX) / screenWidth;
        float anchorMaxX = (safeArea.xMax - padX) / screenWidth;
        float anchorMinY = (safeArea.yMin + padY) / screenHeight;
        float anchorMaxY = (safeArea.yMax - padY) / screenHeight;

        rect.anchorMin = new Vector2(anchorMinX, anchorMinY);
        rect.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }
}