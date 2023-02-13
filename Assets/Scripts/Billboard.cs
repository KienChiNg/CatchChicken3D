using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public GameObject cam;
    public Vector3 lookAt;
    public Slider heathBar;
    public bool minusHp = false;
    private GameObject animal;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        // animal = transform.parent.parent.gameObject;
        heathBar = gameObject.GetComponentInChildren<Slider>();
    }

    void Update()
    {
        if(minusHp){
            heathBar.value -= Time.deltaTime/3;
        }else{
            heathBar.value = 1;
        }
        // if (heathBar.value <= 0){
        //     Destroy(animal);
        // }
    }

    // void MinusHp(){

    // }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cam.transform.position + cam.transform.forward);
    }
}
