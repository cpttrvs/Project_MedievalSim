using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seigneur : MonoBehaviour {

    private TimeManager tm;

    [SerializeField] public float money = 1000f;
    public float wheat = 0;
    public float wood = 0;

    // taxes
    public bool cens = true;
    public bool champart = true;
    public bool banalite = true;

    public float censValue = 10f;
    public float champartPercentage = 5f; // 0 à 100
    public float banaliteValue = 10f;

	// Use this for initialization
	void Start () {
        tm = GameObject.Find("TimeManager").GetComponent<TimeManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(tm.monthChanged())
        {
            // impot fixe
            if(cens)
            {
                Debug.Log("Seigneur : prélèvement cens " + censValue);
                foreach(Foyer foyer in FindObjectsOfType<Foyer>())
                {
                    float res = foyer.takeMoney(censValue);
                    if (res < censValue)
                    {
                        // pas payé au complet
                    }
                    money += res;
                }
            }

            // impot sur les terres et recoltes
            if(champart)
            {
                Debug.Log("Seigneur : prélèvement champart " + champartPercentage/100);
                foreach (NPC_Paysan paysan in FindObjectsOfType<NPC_Paysan>())
                {
                    float temp = paysan.workPlace.GetComponent<Champ>().wheat;
                    temp = temp * (champartPercentage / 100);
                    wheat += temp;
                }
                foreach(NPC_Bucheron bucheron in FindObjectsOfType<NPC_Bucheron>())
                {
                    float temp = bucheron.workPlace.GetComponent<Foret>().wood;
                    temp = temp * (champartPercentage / 100);
                    wood += temp;
                }
            }

            // impot sur les moulins/fours/pressoirs (ateliers pour simplifier)
            if(banalite)
            {
                Debug.Log("Seigneur : prélèvement banalite " + banaliteValue);
                foreach (Atelier atelier in FindObjectsOfType<Atelier>())
                {
                    if(!(atelier is Market))
                    {
                        foreach(GameObject go in atelier.workers)
                        {
                            Foyer foyer = go.GetComponent<ANPC>().house.GetComponent<Foyer>();
                            float res = foyer.takeMoney(banaliteValue / atelier.workers.Count);
                            if(res < (banaliteValue / atelier.workers.Count))
                            {
                                // pas payé au complet
                            }
                            money += res;
                        }
                    }
                }
            }
        }
	}
}
