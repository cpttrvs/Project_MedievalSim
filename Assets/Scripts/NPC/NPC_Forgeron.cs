using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Forgeron : ANPC {

    protected override void Start()
    {
		base.Start();

        if (workPlace != null)
        {
            workshop = workPlace.GetComponent<Atelier>();
            workshop.addWorker(gameObject);
        }
    }

	protected override void Update()
    {
		base.Update();
        NPC_Commis c = workshop.commis[0];
        if(c.getCapacity() == 0)
        {
            if (workshop.inputResource <= workshop.requiredResource)
            {
                if (c.inputAtelierObject.name != "Minerai")
                {
                    // envoyer le commis chercher du minerai
                    c.setInputAtelier(GameObject.Find("Minerai"));
                    c.setOutputAtelier(workshop.gameObject);
                    c.workPlace = c.inputAtelierObject;
                    c.SetDestWork();
                    c.SetState(new NPC_StateWalk(c));
                }

            }
            else
            {
                if (c.inputAtelierObject.name == "Minerai")
                {
                    c.setInputAtelier(workshop.gameObject);
                    c.setOutputAtelier(GameObject.Find("Outils"));
                    c.workPlace = c.inputAtelierObject;
                    c.SetDestWork();
                    c.SetState(new NPC_StateWalk(c));
                }

            }
        }
        
	}
}
