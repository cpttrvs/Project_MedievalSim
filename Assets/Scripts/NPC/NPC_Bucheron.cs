using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Bucheron : ANPC_Recolteur {

    private GameObject sawmill;

    protected override void Start()
    {
        base.Start();
        sawmill = GameObject.Find("Scierie");
        if (workPlace != null)
        {
            workshop = workPlace.GetComponent<Foret>();
            workshop.addWorker(gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Work()
    {
        base.Work();
    }

    public void harvest()
    {
        if (tm.dayChanged())
            ++bagCapacity;
    }

    public override void deposit()
    {
        if (tm.dayChanged())
        {
            Atelier _sawmill = sawmill.GetComponent<Atelier>();
            _sawmill.addInputResource(bagCapacity);
            bagCapacity = 0;
        }
    }

    public void SetDestSawmill()
    {
        //print("setted");
        dest.x = sawmill.transform.position.x;
        dest.y = transform.position.y;
        dest.z = sawmill.transform.position.z;
    }

    public override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.gameObject.name == sawmill.name)
            SetState(new NPC_StateDeposit(this));
    }

    public override void stopWork()
    {
        setAtWork(false);
        SetDestSawmill();
        SetState(new NPC_StateWalk(this));
    }
}
