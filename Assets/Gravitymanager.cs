using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GravityManager : MonoBehaviour
{
    public Button stopButton;
    public Button start;
    public Button addPlanetButton;
    public GameObject sun;
    private Vector2[] v;
    private Rigidbody2D[] rigidBody2Ds;
    private Spacebody[] spaceBodies;
    private float gravitaionalConstant = 6.67430e-11f;
    public Vector2[] initialVelocities;
    public float sunMass = 6.6e7f;
    public bool started;
    public Object planetPrefab;
    public GameManager gameManaer;
    GameObject[] planets;

    private void Awake()
    {
        start.onClick.AddListener(OnStart);
        stopButton.onClick.AddListener(Stopsim);
    }
    void OnStart()
    {
        started = true;

        planets = GameObject.FindGameObjectsWithTag("Planet");

        //initializes arrays
        v = new Vector2[planets.Length];
        rigidBody2Ds = new Rigidbody2D[planets.Length];
        spaceBodies = new Spacebody[planets.Length];
        initialVelocities = new Vector2[planets.Length];
        

        for (int i = 0; i < planets.Length; ++i)
        {
            //sets the arrays
            rigidBody2Ds[i] = planets[i].GetComponent<Rigidbody2D>();
            spaceBodies[i] = planets[i].GetComponent<Spacebody>();
            initialVelocities[i] = spaceBodies[i].initialvelocity;
            v[i] = initialVelocities[i];
        }
    }

    void Update()
    {
        
        if (started)
        {
            

            for (int i = 0; i < planets.Length; ++i)
            {
                GameObject currentPlanet = planets[i];
                Applyforces(currentPlanet, i);
            }
        }
    }


    void Applyforces(GameObject planet, int i)
    {
        Vector2 gravityPos = Gravitycalculatoin(planet, i) * Time.deltaTime;

        v[i] += gravityPos;

        rigidBody2Ds[i].position -= v[i] * Time.deltaTime;
    }
    Vector2 Gravitycalculatoin(GameObject planet, int i)
    {
        Vector2 distanceVector = planet.transform.position - sun.transform.position;
        float distance = distanceVector.magnitude;

        if (distance == 0)
        {
            return Vector2.zero;
        }

        float f = gravitaionalConstant * (rigidBody2Ds[i].mass * sunMass / (distance * distance));
        return f * distanceVector.normalized;

    }

    void Stopsim()
    {
        started = false;
    }
}
