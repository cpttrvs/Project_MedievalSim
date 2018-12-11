using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ANPC : MonoBehaviour {

	protected TimeManager tm;

	public GameObject house;
    protected Foyer foyer;
	public GameObject workPlace;
    protected Atelier workshop;

	protected Vector3 dest;

    private float speed = 10f;
	protected NavMeshAgent agent;
    protected NPC_State currentState;
	protected Animator animator;

    [SerializeField]
    private bool isAtWork;

    [SerializeField] protected float wellBeing = 100; // entre 0 et 100

    // Use this for initialization
    protected virtual void Start () {
		tm = GameObject.Find("TimeManager").GetComponent<TimeManager>();
		if(tm == null) {
			Debug.Log("ANPC : erreur chargement TimeManager");
		}


		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();

        if (house != null)
        {
            foyer = house.GetComponent<Foyer>();
            registerAtHouse();
        }

        SetDestWork();
        isAtWork = false;
		SetState(new NPC_StateWalk(this));
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        agent.speed = speed;
        agent.acceleration = speed;

        currentState.Tick();
	}

	public virtual void Sleep() {
		animator.SetBool ("isSleeping", true);
        print("S");
	}

	public virtual void Work(){
	    animator.SetBool ("isWorking", true);
        //print("W");
	}
	
	public virtual void Move(){
	    animator.SetBool ("isWorking", false);
		animator.SetBool ("isSleeping", false);
		agent.SetDestination(dest);
        //print(dest);
	}
	
	public virtual void SetState (NPC_State state){
		if (currentState != null)
			currentState.OnStateExit();
		currentState = state;

		if (currentState != null)
			currentState.OnStateEnter();	
	}

	public bool IsAtWork(){
        return isAtWork;
	}

    public void setAtWork(bool isAtWork)
    {
        this.isAtWork = isAtWork;
    }

    //home ou grenier (grenier dans le script du paysan)
	/*public void SetDestHome(){
		dest.x = house.transform.position.x;
		dest.y = transform.position.y;
		dest.z = house.transform.position.z;
	}*/

	public void SetDestWork(){
		dest.x = workPlace.transform.position.x;
		dest.y = transform.position.y;
		dest.z = workPlace.transform.position.z;
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == workPlace.name)
        {
            //print(gameObject.name + " In work");
            isAtWork = true;
        }
    }

    public virtual void registerAtHouse()
    {
        foyer.addMember(this);
    }

    public virtual void registerAtWork()
    {
        workshop.addCurrentWorker(gameObject);
    }

    public virtual void leaveWork()
    {
        workshop.removeCurrentWorker(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    public void SetSpeed(float _value) { speed = _value; }
    public float GetSpeed() { return speed; }

    public void setHouse(GameObject house)
    {
        this.house = house;
    }

    public void setWorkplace(GameObject workPlace)
    {
        this.workPlace = workPlace;
    }

    public void setWellbeing(float _value)
    {
        wellBeing = _value;
    }

    public void addWellbeing(float _value)
    {
        wellBeing += _value;
        if (wellBeing < 0)
            wellBeing = 0;
        if (wellBeing > 100)
            wellBeing = 100;
    }

    public float getWellbeing()
    {
        return wellBeing;
    }
}
