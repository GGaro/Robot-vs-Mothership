using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]

public class UITextHero : MonoBehaviour
{
    private PlayerInventory playerInventory;
    public int playerEnergy;

     void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    void Update()
    {
        playerEnergy = playerInventory.collectedEnergy;
        GetComponent<Text>().text = playerEnergy.ToString();
    }
}
