using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class MoveRandomly : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private int newTimeTarget;
    [SerializeField] private float speed;
    [SerializeField] private float posXTop;
    [SerializeField] private float posXBottom;
    [SerializeField] private float posZLeft;
    [SerializeField] private float posZRight;
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Vector3 Target;
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
    public void Return(){
        isDead = false;
        isCatch = false;
        billboard.heathBar.value = 1;
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

            // StartCoroutine(ReturnAnimal());
            playerController.avaiableCatch += 1;
        }
        // if(!gameObject.activeSelf){

        //     StartCoroutine(ReturnAnimal());
        // }
    }
    void PointInsideSphere()
    {
        float dis = Vector3.Distance(transform.position, playerController.transform.position);
        if (dis <= playerController.radius)
        {
            // Destroy(gameObject);
            if (playerController.avaiableCatch > 0)
            {
                // Debug.Log(playerController.curIsCatch + " " + playerController.avaiableCatch);
                billboard.minusHp = true;
                playerController.avaiableCatch -= 1;
                isCatch = true;
            }
        }
        else
        {
            if (playerController.avaiableCatch == 0 && isCatch)
            {
                playerController.avaiableCatch += 1;
                isCatch = false;
            }
            billboard.minusHp = false;
            // animalGo.Remove(gameObject);
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
