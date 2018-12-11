using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foret : Atelier {

    // matières
    [SerializeField] public float wood = 100;
    [SerializeField] public float growRate = 1f;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (activity && tm.dayChanged())
        {
            foreach (GameObject go in currentWorkers)
            {
                NPC_Bucheron lumberjack = go.GetComponent<NPC_Bucheron>();
                lumberjack.harvest();
                --wood;
            }
        }

        if (tm.weekChanged())
        {
            // repousse
            wood += growRate;
        }

        if(wood <= 0)
        {
            wood = 0;
        }
    }
}
