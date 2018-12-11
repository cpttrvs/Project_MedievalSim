using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_StateWork : NPC_State {

	public NPC_StateWork(ANPC npc) : base (npc){}


	public override void OnStateEnter(){
        npc.registerAtWork();
	}

	public override void OnStateExit(){
        npc.leaveWork();
	}

	public override void Tick(){
        npc.Work();
        if (npc is ANPC_Recolteur)
        {
            ANPC_Recolteur r = (ANPC_Recolteur)npc;
            if (r.isFull())
            {
                r.stopWork();
            }
        }
    }

}
