
using Unity.VisualScripting;
using UnityEngine;

public class Spacebody : MonoBehaviour
{
    public Vector2 initialvelocity;
    private float mass = 0f;
    public object manager;
    private float Planetsize = 0f;
    public float RadiusToMassRatio = 6900000000000f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;
    public bool issun;
    private bool Rdown = false;
    private Vector2 mousepos;
    private Vector2 mousetoselfDir;
    private float distancetomouse;
    private float maxsize;


    void Awake()
    {
        //initiate rigidy bdoy and sprite renderer references
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Notsun())
        {
            Planetsize = CalculatePlanetsize();

            body.mass = Calculatemass();

            transform.localScale = new Vector3(Planetsize, Planetsize, Planetsize);
            spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

    }
    void Update()
    {
        if (Notsun())
        {
            Playeradjustments();
        }

    }
    float CalculatePlanetsize()
    {
        Vector2 posnor = transform.position.normalized;
        maxsize = posnor.x + posnor.y;
        float size = Random.Range(.2f, -.2f * maxsize);
        return size;
    }
    void Playeradjustments()
    {

        //allows the player to change the initial velocity and position of planets
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousetoselfDir = mousepos - new Vector2(transform.position.x, transform.position.y);
        distancetomouse = mousetoselfDir.magnitude;


        if (Input.GetMouseButton(0) && distancetomouse < Planetsize * 2)
        {
            transform.position = mousepos;
        }
        else if (Input.GetMouseButtonDown(1) && distancetomouse < Planetsize * 2)
        {
            Rdown = true;
        }

        if (Input.GetMouseButtonUp(1) && Rdown)
        {
            Rdown = false;
            initialvelocity = mousetoselfDir;
        }
    }

    float Calculatemass()
    {
        mass = RadiusToMassRatio * Planetsize;
        return mass;
    }
    bool Notsun()
    {
        if (!issun)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


