using UnityEngine;

public class Dino_Ground_Manager : MonoBehaviour
{
    public static Dino_Ground_Manager instance;
    public Dino_Ground_Spawner spawner;

    public SpriteRenderer groundSprite;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            groundSprite = GetComponent<SpriteRenderer>();
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
