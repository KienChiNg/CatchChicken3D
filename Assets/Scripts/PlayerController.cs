using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator playerAnim;
    [Range(1, 100)] public int segments = 50;
    [Range(1, 500)] private float radius = 5;
    // [Range(1, 500)] public float yRadius = 5; 
    [Range(0.1f, 5)] public float width = 0.1f;
    [Range(0, 100)] public float height = 0;
    Transform target;
    Color c1 = Color.white;
    Color c2 = Color.white;
    Vector3 offset = Vector3.zero;
    public List<GameObject> animalGO;
    private LineRenderer lineRenderer;
    public int avaiableCatch ;
    private int coin = 0;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Radius { get => radius; set => radius = value; }
    public int Coin { get => coin; set => coin = value; }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawCircle();
        // // Debug.DrawRay(transform.position, newDirection*50, Color.red);
    }
    public void DrawCircle()
    {
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = false;
        lineRenderer.widthMultiplier = width;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        float x;
        float y;

        var angle = 20f;
        var points = new Vector3[segments + 1];
        for (int i = 0; i < segments + 1; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * Radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * Radius;

            points[i] = new Vector3(x, height, y);

            angle += (380f / segments);
        }
        lineRenderer.SetPositions(points);
    }
    public void IncrementScore(){
        coin += 1;
        uIManager.SetScoreTxt($"Coin: {coin}");
    }
    public void DecrementScore(int c){
        coin -= c;
        uIManager.SetScoreTxt($"Coin: {coin}");
    }

    private void FixedUpdate()
    {
        playerRb.velocity = new Vector3(joystick.Horizontal * MoveSpeed, playerRb.velocity.y, joystick.Vertical * MoveSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            // Debug.Log(joystick.Horizontal + " " + joystick.Vertical);
            transform.rotation = Quaternion.LookRotation(playerRb.velocity);
            playerAnim.SetFloat("Speed_f", 0.6f);
        }
        else
        {
            playerAnim.SetFloat("Speed_f", 0f);
        }
    }
    void Update()
    {
        if (target) transform.position = target.position + offset;
    }
}
