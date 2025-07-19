using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [Header("카메라")] 
    public Camera target;

    [Header("따라오는 Y위치값")]
    public Vector3 offsety;

    [Header("플레이어 리스폰 설정")] 
    public string playerTag = "Player";
    public float respawnOffset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
        
        if (target == null)
        {
            target = Camera.main;
        }
    }

    void LateUpdate()
    {
        transform.position = target.transform.position + offsety;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        
        float zDist = Mathf.Abs(target.transform.position.z - other.transform.position.z);
        Vector3 bottomWorld = target.ViewportToWorldPoint(
            new Vector3(0.5f, 0f, zDist)
        );

        // 2) 플레이어 리스폰 위치
        Vector3 respawnPos = new Vector3(
            other.transform.position.x,
            bottomWorld.y + respawnOffset,
            other.transform.position.z
        );

        // 3) 실제로 이동시키고 물리 초기화
        other.transform.position = respawnPos;
        if (other.TryGetComponent<Rigidbody2D>(out var rb))
            rb.linearVelocity = Vector2.zero;

        // 4) 멀티 점프 카운트도 초기화해 주려면 (CatController에 Reset 기능 추가)
        if (other.TryGetComponent<CatController>(out var cat))
            cat.ResetJump();  
    }
}
