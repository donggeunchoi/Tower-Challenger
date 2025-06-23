using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaWithRelativePaddingFitter : MonoBehaviour
{
    [Header("SafeArea 패딩 비율 (0.05 = 5%)")]
    [Range(0.01f, 0.2f)]
    public float safeAreaPaddingRatio = 0.05f;

    [Header("최소/최대 패딩(픽셀)")]
    public int minPaddingPx = 16;
    public int maxPaddingPx = 48;

    private void Start() => ApplySafeArea();
    private void OnRectTransformDimensionsChange() => ApplySafeArea();

    public void ApplySafeArea()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Rect safeArea = Screen.safeArea;

        // 비율로 계산한 패딩
        float padX = safeArea.width * safeAreaPaddingRatio;
        float padY = safeArea.height * safeAreaPaddingRatio;

        // Clamp로 최소/최대 픽셀 보정
        padX = Mathf.Clamp(padX, minPaddingPx, maxPaddingPx);
        padY = Mathf.Clamp(padY, minPaddingPx, maxPaddingPx);

        // 앵커 계산
        float anchorMinX = (safeArea.xMin + padX) / Screen.width;
        float anchorMinY = (safeArea.yMin + padY) / Screen.height;
        float anchorMaxX = (safeArea.xMax - padX) / Screen.width;
        float anchorMaxY = (safeArea.yMax - padY) / Screen.height;

        rt.anchorMin = new Vector2(anchorMinX, anchorMinY);
        rt.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}