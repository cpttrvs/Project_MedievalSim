using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Paysan : ANPC_Recolteur {

    private GameObject mill;

	protected override void Start()
    {
		base.Start();
        mill = GameObject.Find("Moulin");
        if (workPlace != null)
        {
            workshop = workPlace.GetComponent<Champ>();
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
        if(tm.dayChanged())
            ++bagCapacity;
    }

    public override void deposit()
    {
        if (tm.dayChanged())
        {
            Atelier moulin = mill.GetComponent<Atelier>();
            moulin.addInputResource(bagCapacity);
            bagCapacity = 0;
        }
    }

    public void SetDestMill()
    {
        //print("setted");
        dest.x = mill.transform.position.x;
        dest.y = transform.position.y;
        dest.z = mill.transform.position.z;
    }

    public override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.gameObject.name == mill.name)
            SetState(new NPC_StateDeposit(this));
    }

    public override void stopWork()
    {
        setAtWork(false);
        SetDestMill();
        SetState(new NPC_StateWalk(this));
    }
}
