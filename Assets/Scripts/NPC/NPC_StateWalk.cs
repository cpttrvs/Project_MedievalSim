using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_StateWalk : NPC_State {

	public NPC_StateWalk(ANPC npc) : base (npc){}


	public override void OnStateEnter(){
		npc.Move();
	}

	public override void Tick(){
        if (npc.IsAtWork())
        {
            npc.SetState(new NPC_StateWork(npc));
        }
	}
}
