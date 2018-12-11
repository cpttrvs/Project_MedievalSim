using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champ : Atelier {

    // matières
    [SerializeField] public float wheat = 100;
    [SerializeField] public float growRate = 1f;

    // Update is called once per frame
    public override void Update () {
        base.Update();
        if (activity && tm.dayChanged())
        {
            foreach(GameObject go in currentWorkers)
            {
                NPC_Paysan peasant = go.GetComponent<NPC_Paysan>();
                peasant.harvest();
                --wheat;
            }
        }
        if(tm.weekChanged())
        {
            wheat += growRate;
        }

        if (wheat <= 0)
        {
            wheat = 0;
        }
	}
}
