using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_StateDeposit : NPC_State
{
    public NPC_StateDeposit(ANPC npc) : base (npc){ }

    public override void Tick()
    {
        //condition parce qu'on sait jamais
        if (npc is ANPC_Recolteur)
        {
            ANPC_Recolteur r = (ANPC_Recolteur)npc;
            r.deposit();
            if (r.getCapacity() == 0)
            {
                r.SetDestWork();
                r.SetState(new NPC_StateWalk(r));
            }
        }
    }
}
