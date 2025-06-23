using UnityEngine;

public class Dino_Ground_Manager : MonoBehaviour
{
    public static Dino_Ground_Manager instance;
    public Dino_Ground_Move move;
    [HideInInspector] public Dino_Ground_Spawner spawner;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (spawner == null)
        {
            spawner = GetComponent<Dino_Ground_Spawner>();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (move.groiundCollider == null)
        {
            move.groiundCollider = move.GetComponent<Collider2D>();
        }

        spawner.GroundSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
