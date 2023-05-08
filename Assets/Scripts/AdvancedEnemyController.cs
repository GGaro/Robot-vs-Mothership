using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AdvancedEnemyController : MonoBehaviour
{
	bool KO = false;
	Transform player;              
	UnityEngine.AI.NavMeshAgent nav;               
	
	private MotherShip motherShip;
	private PlayerInventory playerInventory;
	AdvancedPlayerController playerController;

	float StolenEnergy = 0;

	Animator anim;
	Collider col;
	[SerializeField]float value;
	[SerializeField] Image EnergyBar;
	[SerializeField] float kotime = 4f;
	[SerializeField] GameObject sparks;
	[SerializeField] CapsuleCollider TriggerCollider; 


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		anim = GetComponent<Animator>();
		col = GetComponent<Collider>();
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		motherShip = GameObject.Find("MotherShip").GetComponent<MotherShip>();
		playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<AdvancedPlayerController>();
	}
	
	void Update ()
	{
		if (KO)
        {
			return;
		}
      
		if(motherShip.collectedEnergy != motherShip.neededEnergy)
		{
			nav.SetDestination (player.position);
		}
		else
		{
			nav.enabled = false;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			StolenEnergy = playerInventory.collectedEnergy;
			playerInventory.collectedEnergy = 0;
			playerController.happiness = 0;
			playerController.StartCoroutine(playerController.ReturnToDefaultIdle());

		}

		if(other.tag == "Bullet")
        {
			if (value >= 1000)
            {
				KnockedDown();
				return;
            }
			value += 100;
			changeBar(value);
		}
	}

	void KnockedDown()
    {
		sparks.SetActive(true);
		anim.Play("KnockeOut");
		col.enabled = false;
		KO = true;
		TriggerCollider.enabled = false;
		Invoke("Lower", 0f);
		Invoke("getUp", kotime);
	}

	void getUp()
    {
		sparks.SetActive(false);
		value = 0;
		changeBar(value);
		col.enabled = true;
		KO = false;
		anim.Play("Walk");
		TriggerCollider.enabled = true;
	}

	void changeBar(float new_value)
    {
		EnergyBar.rectTransform.sizeDelta = new Vector2(new_value, 100);
	}

	void Lower()
    {
		float decrease = 1000 / kotime;
		value -= decrease;
		changeBar(value);
		if(KO) Invoke("Lower", 1f);
	}

}