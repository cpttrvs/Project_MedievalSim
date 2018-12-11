using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ANPC_Recolteur : ANPC {

    [SerializeField]
    protected float bagCapacity;
    [SerializeField]
    protected float bagInitialSize = 10f;
    public float bagSize = 10;

    protected override void Update()
    {
        base.Update();
        bagSize = Mathf.Round((bagInitialSize * wellBeing)/100f);
        if (bagSize < 0)
            bagSize = 0f;
    }

    public bool isFull()
    {
        return bagCapacity == bagSize;
    }

    public float getCapacity()
    {
        return bagCapacity;
    }

    public float getSize()
    {
        return bagSize;
    }

    public abstract void stopWork();

    public abstract void deposit();

}
