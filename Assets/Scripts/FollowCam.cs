using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private GameObject playerController;
    [SerializeField] private Vector3 targetCam = new Vector3(0,-10,5);
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerController.transform.position + targetCam;
    }
}
