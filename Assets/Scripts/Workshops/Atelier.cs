using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atelier : MonoBehaviour {
    
    protected TimeManager tm;

    [SerializeField] public List<GameObject> workers;
    [SerializeField] protected List<GameObject> currentWorkers;
    [SerializeField] public List<NPC_Commis> commis;

    public bool activity = false;

    [SerializeField] public float inputResource;
    [SerializeField] public float outputResource;
    [SerializeField] public float requiredResource;
    [SerializeField] protected float productivity = 1f;
    protected float resourceCreated;

    public int seuilOver = 10;
    public int seuilUnder = 10;

	// Use this for initialization
	public virtual void Start () {
        tm = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        if (tm == null)
        {
            Debug.Log("Workshop : erreur chargement TimeManager");
        }
    }
	
	// Update is called once per frame
	public virtual void Update () {
        activity = (currentWorkers.Count > 0);

        // productivité = moyenne du wellbeing des travailleurs
        float temp = 0f; int cpt = 0;
        foreach(GameObject w in currentWorkers)
        {
            temp += (w.GetComponent<ANPC>().getWellbeing() / 100f);
            cpt++;
        }
        productivity = (temp / cpt) * cpt;

        // resourceCreated par mois

	}

    public virtual void addInputResource(float _value)
    {
        inputResource += _value;
    }

    public virtual float takeInputResource(float _value)
    {
        if (inputResource >= _value)
        {
            inputResource -= _value;
            return (_value * 10) / 10; // nb flotant arrondi
        }
        return 0;
    }

    public virtual void transformResource()
    {
        if(inputResource >= requiredResource)
        {
            inputResource -= requiredResource;
            outputResource += (currentWorkers.Count * productivity);
            resourceCreated += (currentWorkers.Count * productivity);
        }
    }

    public virtual void addOutputResource(float _value)
    {
        outputResource += _value;
    }

    public virtual float takeOutputResource(float _value)
    {
        if (outputResource >= _value)
        {
            outputResource -= _value;
            return (_value * 10) / 10; // nb flotant arrondi
        }
        return 0;
    }

    public virtual float getResourceCreated()
    {
        return resourceCreated;
    }

    public virtual float getResourceCreatedMonthly()
    {
        return resourceCreated / tm.getMonth();
    }

    public virtual float getResourceCreatedDaily()
    {
        return resourceCreated / tm.getDay();
    }

    public virtual float getProductivity()
    {
        return productivity;
    }



    public void addWorker(GameObject worker)
    {
        if(!workers.Contains(worker))
            workers.Add(worker);
    }

    public void removeWorker(GameObject worker)
    {
        if (workers.Contains(worker))
            workers.Remove(worker);
    }

    public void addCurrentWorker(GameObject worker)
    {
        if (!currentWorkers.Contains(worker))
            currentWorkers.Add(worker);
    }

    public void removeCurrentWorker(GameObject worker)
    {
        if (currentWorkers.Contains(worker))
            currentWorkers.Remove(worker);
    }

    public bool isOverProducing()
    {
        return outputResource > (currentWorkers.Count + 1) * seuilOver;
    }

    public bool isUnderProducing()
    {
        return inputResource > (currentWorkers.Count + 1) * seuilUnder;
    }

    public void addCommis(NPC_Commis _commis)
    {
        if (!commis.Contains(_commis))
            commis.Add(_commis);
    }

    public void removeCommis(NPC_Commis _commis)
    {
        if (commis.Contains(_commis))
            commis.Remove(_commis);
    }
}
