using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Boulanger : ANPC
{

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
    }
}