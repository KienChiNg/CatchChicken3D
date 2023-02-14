using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class MoveRandomly : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private int newTimeTarget;
    [SerializeField] private float posXTop;
    [SerializeField] private float posXBottom;
    [SerializeField] private float posZLeft;
    [SerializeField] private float posZRight;
    [SerializeField] private float speedAnimal = 10;
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Vector3 Target;
    [SerializeField] private float displacementDist = 10f;
    List<GameObject> animalGo;
    private PlayerController playerController;
    private Billboard billboard;
    public bool isCatch;
    public bool isDead;
    void Start()
    {
        isDead = false;
        isCatch = false;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        billboard = gameObject.GetComponentInChildren<Billboard>();
        animalGo = playerController.animalGO;
        nav = GetComponent<NavMeshAgent>();
        newTarget();
    }
    public void Return()
    {
        isDead = false;
        isCatch = false;
        billboard.heathBar.value = 1;
        gameObject.SetActive(true);
    }
    void Update()
    {
        PointInsideSphere();
        timer += Time.deltaTime;
        if (timer >= newTimeTarget)
        {
            newTarget();
            timer = Random.Range(0, 2);
        }
        if (billboard.heathBar.value <= 0)
        {
            gameObject.SetActive(false);
            playerController.IncrementScore();
            // StartCoroutine(ReturnAnimal());
            playerController.avaiableCatch += 1;
        }
    }
    void MoveToPos(Vector3 pos)
    {
        nav.SetDestination(pos);
        nav.speed = speedAnimal;
    }
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawLine(transform.position, playerController.gameObject.transform.position);
    // }
    void PointInsideSphere()
    {
        float dis = Vector3.Distance(transform.position, playerController.transform.position);
        if (dis <= playerController.Radius)
        {
            if (playerController.avaiableCatch > 0 && !isCatch)
            {
                billboard.minusHp = true;
                playerController.avaiableCatch -= 1;
                isCatch = true;
            }
        }
        else
        {
            if (isCatch)
            {
                playerController.avaiableCatch += 1;
                isCatch = false;
            }
            billboard.minusHp = false;
            // animalGo.Remove(gameObject);
        }
        if (isCatch)
        {
            Vector3 normDir = (transform.position - playerController.gameObject.transform.position).normalized;
            MoveToPos(playerController.gameObject.transform.position + (normDir * displacementDist));
            // Debug.Log("Covaodaykhong");
            if(dis > playerController.Radius){
                isCatch = false;
            }
        }
    }
    void newTarget()
    {
        float myX = transform.position.x;
        float myZ = transform.position.z;

        float xPos = Random.Range(myX - 10, myX + 10);
        float zPos = Random.Range(myZ - 10, myZ + 10);

        if (xPos > posXTop || xPos < posXBottom)
        {
            if (xPos > posXTop)
            {
                xPos = Random.Range(myX - 10, 0);
            }
            else
            {
                xPos = Random.Range(0, myX + 10);
            }
        }
        if (zPos > posZLeft || zPos < posZRight)
        {
            if (zPos > posZLeft)
            {
                zPos = Random.Range(myZ - 10, 0);
            }
            else
            {
                zPos = Random.Range(0, myZ + 10);
            }
        }

        Target = new Vector3(xPos, transform.position.y, zPos);
        nav.SetDestination(Target);
    }
}
