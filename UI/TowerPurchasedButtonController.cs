using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerPurchasedButtonController : MonoBehaviour
{
    [SerializeField] private TowerConfiguration tower;
    [SerializeField] private Image towerPlacementImage;
    [SerializeField] private TMP_Text priceText;

    public event EventHandler<EventArgTemplate<TowerConfiguration>> TowerPurchased;

    private void Start()
    {
        towerPlacementImage.sprite = tower.TowerImage;
        priceText.text = tower.Price.ToString();
    }


    public void OnTowerSelect()
    {
        SafeEventHandler.SafelyBroadcastEvent(ref TowerPurchased, tower, this);
    }

   
}
