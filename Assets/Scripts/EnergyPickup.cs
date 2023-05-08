using UnityEngine;
using System.Collections;

public class EnergyPickup : MonoBehaviour 
{
	PlayerInventory playerInventory;
	AdvancedPlayerController playerController;
	
	void Start()
	{
		playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvancedPlayerController>();
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			playerInventory.collectedEnergy ++;
			playerController.happiness = 2;
			gameObject.SetActive(false);
		}
	}
}