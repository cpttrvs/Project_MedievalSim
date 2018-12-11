using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvolutionManager : MonoBehaviour {

    public List<Atelier> ateliers;
    public TimeManager tm;

    public GameObject boulanger;
    public GameObject meunier;
    public GameObject paysan;
    public GameObject bucheron;
    public GameObject charpentier;
    public GameObject forgeron;
    public GameObject commis;
    public GameObject maison;
    public GameObject champ;
    public GameObject foret;

    private GameObject champBase;

	// Use this for initialization
	void Start () {
		foreach(Atelier a in GameObject.FindObjectsOfType<Atelier>())
        {
            if(!(a is Champ) && !(a is Foret))
                ateliers.Add(a);
        }
        champBase = GameObject.Find("Champ1");
	}
	
	// Update is called once per frame
	void Update () {
        if (tm.monthChanged()){
            foreach (Atelier a in ateliers)
            {
                //pas assez de stocks
                if (a.isOverProducing())
                {
                    GameObject m;
                    GameObject b;
                    
                    switch (a.GetType().ToString())
                    {
                        case "Moulin": //creation de paysans
                            m = spawnHouse();
                            b = Instantiate(paysan, GameObject.Find("Paysans").transform, false) as GameObject;
                            b.GetComponent<NavMeshAgent>().Warp(m.transform.position);
                            GameObject c = spawnChamp();
                            ANPC npc = b.GetComponent<NPC_Paysan>();
                            npc.setHouse(m);
                            npc.setWorkplace(c);
                            break;
                        case "Boulangerie": //creation de commis
                            m = spawnHouse();
                            b = Instantiate(commis, GameObject.Find("Artisans").transform, false) as GameObject;
                            b.GetComponent<NavMeshAgent>().Warp(m.transform.position);
                            NPC_Commis com = b.GetComponent<NPC_Commis>();
                            com.setHouse(m);
                            com.setWorkplace(a.gameObject);
                            com.setInputAtelier(GameObject.Find("Moulin"));
                            com.setOutputAtelier(a.gameObject);
                            com.workPlace.GetComponent<Atelier>().addCommis(com);
                            break;
                        case "Scierie": //creation de paysans
                            m = spawnHouse();
                            b = Instantiate(bucheron, GameObject.Find("Paysans").transform, false) as GameObject;
                            b.GetComponent<NavMeshAgent>().Warp(m.transform.position);
                            GameObject f = spawnForet();
                            ANPC npcB = b.GetComponent<NPC_Bucheron>();
                            npcB.setHouse(m);
                            npcB.setWorkplace(f);
                            break;
                        default:
                            Debug.Log("Overproducing : " + a.GetType().ToString());
                            break;
                    }
                }
                else if (a.isUnderProducing()) //trop de stock d'input : plus de main d'oeuvre
                {
                    GameObject m;
                    GameObject b;
                    switch (a.GetType().ToString())
                    {
                        case "Moulin":
                            m = spawnHouse();
                            b = Instantiate(meunier, GameObject.Find("Artisans").transform, false) as GameObject;
                            b.GetComponent<NavMeshAgent>().Warp(m.transform.position);
                            NPC_Meunier npc = b.GetComponent<NPC_Meunier>();
                            npc.setHouse(m);
                            npc.setWorkplace(a.gameObject);
                            break;
                        case "Boulangerie":
                            m = spawnHouse();
                            b = Instantiate(boulanger, GameObject.Find("Artisans").transform, false) as GameObject;
                            b.GetComponent<NavMeshAgent>().Warp(m.transform.position);
                            NPC_Boulanger npcB = b.GetComponent<NPC_Boulanger>();
                            npcB.setHouse(m);
                            npcB.setWorkplace(a.gameObject);
                            break;
                        case "Scierie":
                            m = spawnHouse();
                            b = Instantiate(charpentier, GameObject.Find("Artisans").transform, false) as GameObject;
                            b.GetComponent<NavMeshAgent>().Warp(m.transform.position);
                            NPC_Charpentier npcC = b.GetComponent<NPC_Charpentier>();
                            npcC.setHouse(m);
                            npcC.setWorkplace(a.gameObject);
                            break;
                        default:
                            Debug.Log("Underproducing : " + a.GetType().ToString());
                            break;
                    }
                }
                
            }
        }
	}

    private float minX = 164.95f, minZ = 75.81f, maxX = 218.7f, maxZ = 170.9f;
    private GameObject spawnHouse()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        Transform parent = GameObject.Find("Foyers").transform;

        Vector3 pos = new Vector3(x, parent.position.y, z);
        /*
        while(Physics.CheckSphere(pos, 10, 9))
        {
            x = Random.Range(minX, maxX);
            z = Random.Range(minZ, maxZ);
        }
        */
        GameObject m = Instantiate(maison) as GameObject;
        m.transform.SetParent(parent);
        m.transform.position = new Vector3(x, m.transform.parent.position.y, z);
        return m;
    }

    private int champCreated = 0;
    
    private GameObject spawnChamp()
    {
        GameObject c = Instantiate(champ) as GameObject;
        Transform parent = GameObject.Find("Champs").transform;
        c.transform.SetParent(parent);
        int decalageX = champCreated / 8;
        c.transform.localPosition = new Vector3(champBase.transform.localPosition.x - (champCreated % 8) * 8, champBase.transform.localPosition.y, champBase.transform.localPosition.z + (1 + decalageX) * 20);
        c.transform.rotation = new Quaternion();
        ++champCreated;

        return c;
    }

    private float minFX = 197.8f, maxFZ = 72.3f, maxFX = 218.5f, minFZ = 14.5f;
    private GameObject spawnForet()
    {
        GameObject f = Instantiate(foret) as GameObject;
        Transform parent = GameObject.Find("Forets").transform;
        f.transform.SetParent(parent);

        float x = Random.Range(minFX, maxFX);
        float z = Random.Range(minFZ, maxFZ);

        f.transform.position = new Vector3(x, parent.position.y, z);
        f.transform.rotation = new Quaternion();

        return f;
    }
}
