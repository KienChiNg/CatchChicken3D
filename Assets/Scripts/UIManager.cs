using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject iconOpenShop;
    [SerializeField] private TextMeshProUGUI radiusPriceTxt;
    [SerializeField] private TextMeshProUGUI speedPriceTxt;
    [SerializeField] private int speedPrice = 1 ;
    [SerializeField] private int radiusPrice = 1 ;

    public int SpeedPrice { get => speedPrice; set => speedPrice = value; }
    public int RadiusPrice { get => radiusPrice; set => radiusPrice = value; }

    // Start is called before the first frame update
    void Start()
    {
        SetSpeedPriceTxt();
        SetRadiusPriceTxt();
        SetScoreTxt("Coin: 0");
    }

    public void StateShop(bool state){
        shop.SetActive(state);
        iconOpenShop.SetActive(!state);
    }

    public void SetScoreTxt(string txt){
        scoreTxt.text = txt;
    }

    public void SetSpeedPriceTxt(){
        speedPriceTxt.text = $"{SpeedPrice}";
    }
    public void SetRadiusPriceTxt(){
        radiusPriceTxt.text = $"{RadiusPrice}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
