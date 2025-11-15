using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject timeUI;
    [SerializeField] private GameObject numOfKillsUI;
    [SerializeField] private GameObject healthBar;
    private float timer;
    [HideInInspector] public int numOfKills;
    [HideInInspector] public float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controlTimer();


    }

    void controlTimer()
    {
        timer+=Time.deltaTime;
        int minutes=0;
        int seconds=0;
        if (timer >= 60f)
        {
            timer=0f;
            minutes++;
        }
        seconds = Mathf.RoundToInt(timer);

        string finalTime=minutes+":"+seconds;

        timeUI.GetComponent<TMPro.TMP_Text>().text = finalTime;

    }

    public void addKill()
    {
        numOfKills++;
        numOfKillsUI.GetComponent<TMPro.TMP_Text>().text=numOfKills+"";
    }

    public void controlHealth()
    {
        
    }
}
