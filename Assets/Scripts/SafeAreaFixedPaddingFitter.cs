using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaWithRelativePaddingFitter : MonoBehaviour
{
    [Header("비대칭 패딩 비율 (0.05=5%)")]
    [Tooltip("좌, 우, 상, 하 순서")]
    public Vector4 paddingRatio = new Vector4(0.05f, 0.05f, 0.05f, 0.05f);

    [Header("동적 패딩 제한 (화면 크기 %)")]
    [Range(0.01f, 0.1f)]
    public float minPaddingRatio = 0.03f;
    [Range(0.05f, 0.15f)]
    public float maxPaddingRatio = 0.08f;


    private ScreenOrientation lastOrientation;
    private Vector2 lastResolution; //마지막 해상도 저장
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        lastOrientation = Screen.orientation;
        lastResolution = new Vector2(Screen.width, Screen.height); // [추가] 초기 해상도 저장
        ApplySafeArea();
    }

    void Update()
    {
        // 화면 방향, 해상도, Safe Area 중 하나라도 바뀌면 Safe Area 적용
        // 화면 회전 감지
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            ApplySafeArea();
        }
        // 해상도 변화 감지
        if (Screen.width != lastResolution.x || Screen.height != lastResolution.y)
        {
            lastResolution = new Vector2(Screen.width, Screen.height);
            ApplySafeArea();
        }
    }

    private void OnRectTransformDimensionsChange() => ApplySafeArea();

    void ApplySafeArea()
    {

        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        Rect safeArea = Screen.safeArea;

        // 안전 영역 무결성 검사
        if (safeArea.width <= 10 || safeArea.height <= 10)
            safeArea = new Rect(0, 0, Screen.width, Screen.height);

        // 동적 패딩 계산 (화면 비율 기반)
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 개별 방향 패딩 계산 및 클램핑
        float leftPad = Mathf.Clamp(
            safeArea.width * paddingRatio.x,
            screenWidth * minPaddingRatio,
            screenWidth * maxPaddingRatio
        );

        float rightPad = Mathf.Clamp(
            safeArea.width * paddingRatio.y,
            screenWidth * minPaddingRatio,
            screenWidth * maxPaddingRatio
        );

        float topPad = Mathf.Clamp(
            safeArea.height * paddingRatio.z,
            screenHeight * minPaddingRatio,
            screenHeight * maxPaddingRatio
        );

        float bottomPad = Mathf.Clamp(
            safeArea.height * paddingRatio.w,
            screenHeight * minPaddingRatio,
            screenHeight * maxPaddingRatio
        );

        // 앵커 계산 (비대칭 적용)
        float anchorMinX = (safeArea.xMin + leftPad) / screenWidth;
        float anchorMinY = (safeArea.yMin + bottomPad) / screenHeight;
        float anchorMaxX = (safeArea.xMax - rightPad) / screenWidth;
        float anchorMaxY = (safeArea.yMax - topPad) / screenHeight;

        rectTransform.anchorMin = new Vector2(anchorMinX, anchorMinY);
        rectTransform.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
    }
}