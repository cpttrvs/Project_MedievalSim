using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulangerie : Atelier
{
    //input : farine, output = pain

    public override void Update()
    {
        base.Update();
        if (activity && tm.dayChanged())
        {
            transformResource();
        }
    }
}
