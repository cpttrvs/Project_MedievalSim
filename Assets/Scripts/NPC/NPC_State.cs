using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC_State{
	protected ANPC npc;

	public NPC_State (ANPC npc){
		this.npc = npc;
	}

	public virtual void OnStateEnter(){}
	public virtual void OnStateExit(){}
	public abstract void Tick();
}