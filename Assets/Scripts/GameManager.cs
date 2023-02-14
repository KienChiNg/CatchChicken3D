using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;
    private UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void IncrementSpeed()
    {
        if (playerController.Coin >= uIManager.SpeedPrice)
        {
            playerController.DecrementScore(uIManager.SpeedPrice);
            playerController.MoveSpeed += 2;
            uIManager.SpeedPrice += 1;
            uIManager.SetSpeedPriceTxt();
        }else{
            Debug.Log("KhongDuTien");
        }
    }

    public void IncrementRadius()
    {
        if (playerController.Coin >= uIManager.RadiusPrice)
        {
            playerController.DecrementScore(uIManager.RadiusPrice);
            playerController.Radius += 0.1f;
            playerController.DrawCircle();
            uIManager.RadiusPrice += 1;
            uIManager.SetRadiusPriceTxt();
        }else{
            Debug.Log("KhongDuTien");
        }
    }
    public void CloseShop()
    {
        uIManager.StateShop(false);
    }
    public void OpenShop()
    {
        uIManager.StateShop(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
