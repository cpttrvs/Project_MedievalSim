using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : Atelier {

    // transforme la ressource input en ressource output, en gagnant de l'or sur la transaction
    
    [SerializeField] protected float buyingPrice = 1f;
    [SerializeField] protected float sellingPrice = 1f;

    [SerializeField] private bool canBuyInputResource = false;

    [SerializeField] public float money;

    public override void Update()
    {
        base.Update();
        if (activity && tm.dayChanged())
        {
            transformResource();
        }

        if(canBuyInputResource && tm.dayChanged() && inputResource == 0)
        {
            if(money > buyingPrice * 10)
            {
                addInputResource(10);
                money -= buyingPrice * 10;
            }
        }

        if(tm.monthChanged())
        {
            // paye des npc concernés
            if(gameObject.name == "Pain")
            {
                int nb = FindObjectsOfType<NPC_Paysan>().Length + FindObjectsOfType<NPC_Boulanger>().Length;
                nb += FindObjectOfType<Moulin>().commis.Count;
                float salary = money / nb;
                foreach(NPC_Paysan paysan in FindObjectsOfType<NPC_Paysan>())
                {
                    paysan.house.GetComponent<Foyer>().addMoney(salary);
                }
                foreach(NPC_Boulanger boulanger in FindObjectsOfType<NPC_Boulanger>())
                {
                    boulanger.house.GetComponent<Foyer>().addMoney(salary);
                }
                foreach(NPC_Commis commis in FindObjectOfType<Moulin>().commis)
                {
                    commis.house.GetComponent<Foyer>().addMoney(salary);
                }
            }
            if (gameObject.name == "Bois")
            {
                int nb = FindObjectsOfType<NPC_Bucheron>().Length + FindObjectsOfType<NPC_Charpentier>().Length;
                nb += FindObjectOfType<Scierie>().commis.Count;
                float salary = money / nb;
                foreach (NPC_Bucheron bucheron in FindObjectsOfType<NPC_Bucheron>())
                {
                    bucheron.house.GetComponent<Foyer>().addMoney(salary);
                }
                foreach (NPC_Charpentier charpentier in FindObjectsOfType<NPC_Charpentier>())
                {
                    charpentier.house.GetComponent<Foyer>().addMoney(salary);
                }
                foreach (NPC_Commis commis in FindObjectOfType<Scierie>().commis)
                {
                    commis.house.GetComponent<Foyer>().addMoney(salary);
                }
            }
            if (gameObject.name == "Outils")
            {
                int nb = FindObjectsOfType<NPC_Forgeron>().Length;
                nb += FindObjectOfType<Forge>().commis.Count;
                float salary = money / nb;
                foreach (NPC_Forgeron forgeron in FindObjectsOfType<NPC_Forgeron>())
                {
                    forgeron.house.GetComponent<Foyer>().addMoney(salary);
                }
                foreach (NPC_Commis commis in FindObjectOfType<Forge>().commis)
                {
                    commis.house.GetComponent<Foyer>().addMoney(salary);
                }
            }

            if(gameObject.name == "Minerai")
            {

            } else
            {
                money = 0;
            }
        }
    }

    public override void addInputResource(float _value)
    {
        inputResource += _value;
    }

    public override float takeOutputResource(float _value)
    {
        if (outputResource >= _value)
        {
            outputResource -= _value;
            money += sellingPrice * ((_value * 10) / 10);
            return (_value * 10) / 10; // nb flotant arrondi
        }
        return 0;
    }

    public float getBuyingPrice() { return buyingPrice; }
    public float getSellingPrice() { return sellingPrice; }
}
