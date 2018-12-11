using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scierie : Atelier {
    
    //input : bois, output : planche

    public override void Update()
    {
        base.Update();
        //gérer quand est-ce que ça produit
        if (activity && tm.dayChanged())
        {
            transformResource();
        }
    }
}
