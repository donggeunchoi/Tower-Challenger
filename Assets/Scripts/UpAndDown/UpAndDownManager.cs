using UnityEngine;

public class UpAndDownManager : MonoBehaviour
{
    public static UpAndDownManager instance;

    public UpAndDownUI upAndDownUI;
    public UpAndDown upAndDown;

    public float randomNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upAndDownUI.InitUi();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
