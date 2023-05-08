using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]

public class UITextMS : MonoBehaviour
{
    private MotherShip motherShip;
    public int msEnergy;
    public int energyNeeded;

    void Awake()
    {
        motherShip = GameObject.Find("MotherShip").GetComponent<MotherShip>();
    }

    void Update()
    {
        msEnergy = motherShip.collectedEnergy;
        energyNeeded = motherShip.neededEnergy;

        GetComponent<Text>().text = msEnergy + " / " + energyNeeded;
    }
}
