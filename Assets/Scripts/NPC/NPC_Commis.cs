using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Commis : ANPC_Recolteur {

    public GameObject inputAtelierObject;
    public GameObject outputAtelierObject;

    private Atelier inputAtelier; // moulin par ex
    private Atelier outputAtelier; // boulangerie par ex
    
	protected override void Start () {
        base.Start();
        inputAtelier = inputAtelierObject.GetComponent<Atelier>();
        outputAtelier = outputAtelierObject.GetComponent<Atelier>();
        SetDestOutputAtelier();

        inputAtelier.addCommis(this);
	}

    protected override void Update () {
        base.Update();
	}

    public override void Work()
    {
        // moulin -> take farine
        if(inputAtelier is Market)
        {
            // gestion de l'argent
            bagCapacity += inputAtelier.takeOutputResource(bagSize);
        } else
        {
            bagCapacity += inputAtelier.takeOutputResource(bagSize);
        }
    }


    public void SetDestOutputAtelier()
    {
        dest.x = outputAtelierObject.transform.position.x;
        dest.y = outputAtelierObject.transform.position.y;
        dest.z = outputAtelierObject.transform.position.z;
    }

    public override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        if (col.gameObject.name == outputAtelier.name)
            SetState(new NPC_StateDeposit(this));
    }

    public void fillBag(float flour)
    {
        bagCapacity += (int)flour;
    }

    public override void deposit()
    {
        if (tm.dayChanged())
        {
            // boulangerie -> give farine
            if(outputAtelier is Market)
            {
                // gestion de l'argent
                outputAtelier.addInputResource(bagCapacity);
                bagCapacity = 0;
            } else
            {
                outputAtelier.addInputResource(bagCapacity);
                bagCapacity = 0;
            }
        }
    }

    public override void registerAtWork(){}

    public override void leaveWork(){}

    public override void stopWork()
    {
        setAtWork(false);
        SetDestOutputAtelier();
        SetState(new NPC_StateWalk(this));
    }

    public void setInputAtelier(GameObject _go)
    {
        inputAtelierObject = _go;
        inputAtelier = _go.GetComponent<Atelier>();
    }
    public void setOutputAtelier(GameObject _go)
    {
        outputAtelierObject = _go;
        outputAtelier = _go.GetComponent<Atelier>();
    }
}
