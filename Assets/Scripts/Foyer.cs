using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foyer : MonoBehaviour
{

    private TimeManager tm;

    [SerializeField] private List<ANPC> family = new List<ANPC>();
    [SerializeField] private int food = 100;
    [SerializeField] private int water = 100;
    [SerializeField] public float gold = 100;
    [SerializeField] private int firewood = 10;//pour se chauffer
    [SerializeField] private int hygiene = 0;

    private bool weekUpdate = false;

    private Market outils;
    private Market bois;
    private Market pain;

    // si on peut pas acheter : enlever ImpactPenurie en wellbeing à toute la famille
    private float impactPenurie = 2;
    private bool penuerie = false;


    // Use this for initialization
    void Start()
    {
        tm = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        if (tm == null)
        {
            Debug.Log("Home : erreur chargement TimeManager");
        }

        outils = GameObject.Find("Outils").GetComponent<Market>();
        bois = GameObject.Find("Bois").GetComponent<Market>();
        pain = GameObject.Find("Pain").GetComponent<Market>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!penuerie)
        {
            foreach (ANPC npc in family)
            {
                if(impactPenurie == 0)
                    npc.addWellbeing(1);
                else 
                    npc.addWellbeing(impactPenurie);
            }
        }

        int value = family.Count;
        if (tm.weekChanged())
        {
            consumeFood(value);
            buyFood(value);
        }
        if (tm.monthChanged())
        {
            consumeFirewood(value);
            buyFirewood(value);

        }
    }

    public void addFood(int value) { food += value; }
    public void addWater(int value) { water += value; }
    public void addMoney(float value) { gold += value; }
    public void addFirewood(int value) { firewood += value; }
    public void addMember(ANPC npc) { if (!family.Contains(npc)) family.Add(npc); }

    public void consumeFood(int value)
    {
        food -= value;

        if (food <= 0)
        {
            food = 0;
            foreach (ANPC npc in family)
            {
                penuerie = true;
                npc.addWellbeing(-impactPenurie);
            }
        }
        else
        {
            foreach (ANPC npc in family)
            {
                penuerie = false;
                npc.addWellbeing(impactPenurie);
            }
        }

    }

    public void consumeFirewood(int value)
    {
        firewood -= value;

        if (firewood <= 0)
        {
            firewood = 0;
            foreach (ANPC npc in family)
            {
                penuerie = true;
                npc.addWellbeing(-impactPenurie);
            }
        }
        else
        {
            foreach (ANPC npc in family)
            {
                penuerie = false;
                npc.addWellbeing(impactPenurie);
            }
        }
    }

    public void buyFood(int value)
    {
        if (gold < pain.getSellingPrice() * value)
        {
            //Debug.Log(gameObject.name + " pas assez d'argent pour manger");
            // pas assez d'argent pour se payer
            foreach (ANPC npc in family)
            {
                penuerie = true;
                npc.addWellbeing(-impactPenurie);
            }
        }
        else
        {
            float temp = pain.takeOutputResource(value);
            if (temp == 0)
            {
                //Debug.Log(gameObject.name + " pas de pain en vente");
                // pas de pain en vente
                foreach (ANPC npc in family)
                {
                    penuerie = true;
                    npc.addWellbeing(-impactPenurie);
                }
            }
            else
            {
                food += Mathf.RoundToInt(temp);
                gold -= pain.getSellingPrice() * value;
            }
        }
    }

    public void buyFirewood(int value)
    {
        if (gold < bois.getSellingPrice() * value)
        {
            //Debug.Log(gameObject.name + " pas assez d'argent pour se chauffer");
            // pas assez d'argent pour se payer
            foreach (ANPC npc in family)
            {
                penuerie = true;
                npc.addWellbeing(-impactPenurie);
            }
        }
        else
        {
            float temp = bois.takeOutputResource(value);
            if (temp == 0)
            {
                //Debug.Log(gameObject.name + " pas de bois en vente");
                // pas de bois en vente
                foreach (ANPC npc in family)
                {
                    penuerie = true;
                    npc.addWellbeing(-impactPenurie);
                }
            }
            else
            {
                firewood += Mathf.RoundToInt(temp);
                gold -= bois.getSellingPrice() * value;
            }
        }
    }

    public float takeMoney(float value) {
        float temp = gold-value;
        if(temp <= 0)
        {
            temp = gold;
            foreach (ANPC npc in family)
            {
                npc.addWellbeing(-value);
            }
            gold = 0;
            return temp;
        }
        gold -= value;
        return value;
    }

    public void setPenurieFactor(float value)
    {
        impactPenurie = value;
    }
}
