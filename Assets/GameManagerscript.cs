

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button start;
    public Button stopButtton;
    public Button addPlanetButton;
    public bool started;
    public Object planetPrefab;
    public TMP_Text fpsText;

    void Awake()
    {
        start.onClick.AddListener(OnStart);
        addPlanetButton.onClick.AddListener(Addplanet);
        stopButtton.onClick.AddListener(StopSim);
    }
    void Addplanet()
    {
        if (started != true)
        {
            Vector2 randompos = new(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
            Vector2 instancepos = Camera.main.ScreenToWorldPoint(randompos);

            Instantiate(planetPrefab, instancepos, Quaternion.identity);
        }
    }
    void Update() {
        float fps = 1 / Time.deltaTime;
        float mS = Time.deltaTime * 1000;
        
        fpsText.text = mS.ToString("F2") + " MS";
    }

    void OnStart()
    {
        started = true;
    }
    void StopSim()
    {
        started = false;
    }
}
