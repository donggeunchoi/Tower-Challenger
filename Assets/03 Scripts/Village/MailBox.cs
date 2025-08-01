using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailBox : MonoBehaviour
{
    public GameObject mailPaper;
    public int firstGift;
    public Button GetButton;
    public float colorA;

    public bool isButton;

    [Header("받은 메일")]
    public GameObject imageMail;

    //private void Awake()
    //{
    //    PlayerPrefs.DeleteKey("MailReceived");
    //}

    private void OnEnable()
    {
        isButton = PlayerPrefs.GetInt("MailReceived", 0) == 1;

        if (isButton == true)
        {
            // 씬전환 후에도 반투명과  Destroy(GetButton.gameObject); 유지
            ColorA();

            if (GetButton != null)
            {
                Destroy(GetButton.gameObject);
            }
        }
    }

    private void Start()
    {
        isButton = false;
    }

    // 받고 나면 반투명하게 , 한번 받은 후 다시 받기 불가하게
    public void OnClickMailPaperOpen()
    {
        mailPaper.SetActive(true);
    }
    
    public void OnClickMailBoxClose()
    {
        mailPaper.SetActive(false);
    }

    public void OnClickGetButton()
    {
        GameManager.Instance.account.AddGold(firstGift);
        isButton = true;

        PlayerPrefs.SetInt("MailReceived", 1); // 수령 상태 저장
        PlayerPrefs.Save(); // 명시적 저장 (안 해도 되지만 안정성↑)
        ColorA();
        Destroy(GetButton.gameObject);
        mailPaper.SetActive(false);
    }

    private void ColorA()
    {
        SetAlphaChildren(imageMail.transform, colorA);
    }

    private void SetAlphaChildren(Transform _parent, float _alpha)
    {
        foreach (Transform child in _parent)
        {
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                Color color = image.color;
                color.a = _alpha;
                image.color = color;
            }

            TextMeshProUGUI tmp = child.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                Color c = tmp.color;
                c.a = _alpha;
                tmp.color = c;
            }           
            SetAlphaChildren(child, _alpha);
        }
    }
}
