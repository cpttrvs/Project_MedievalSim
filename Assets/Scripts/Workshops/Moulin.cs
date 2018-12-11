using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moulin : Atelier {
    
    // input : blé, output : farine

    private Transform fan;

    public override void Start()
    {
        base.Start();
        fan = transform.Find("fan");
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        fan.Rotate(Vector3.down * (Time.deltaTime * 50));
        //gérer quand est-ce que ça produit
        if (activity && tm.dayChanged())
        {
            transformResource();            
        }
    }
}
