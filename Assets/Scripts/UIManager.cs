using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Transform canvasStats;
    public Transform canvasSeigneur;

    public TimeManager timemanager;
    public Seigneur seigneur;
    public Forge forge;
    public Moulin moulin;
    public Boulangerie boulangerie;
    public Scierie scierie;

    public Market outils;
    public Market bois;
    public Market pain;
    public Market minerai;

    private Slider sliderCens;
    private Slider sliderChampart;
    private Slider sliderBanalite;
    private Text seigneurMoneyDisplay;
    private Text seigneurRessourceDisplay;
    private Text seigneurCensDisplay;
    private Text seigneurChampartDisplay;
    private Text seigneurBanaliteDisplay;

    private Slider timemanagerSlider;
    private Text timemanagerDisplay;
    private Slider growSlider;
    private Text growDisplay;

    private Slider overSlider;
    private Text displayOver;
    private Slider underSlider;
    private Text displayUnder;

    private Text forgeDisplay;
    private Text moulinDisplay;
    private Text boulangerieDisplay;
    private Text scierieDisplay;

    private Text marketOutilsDisplay;
    private Text marketBoisDisplay;
    private Text marketPainDisplay;
    private Text marketMineraiDisplay;
    private Text marketMoneyDisplay;

    private Text npcWellbeingDisplay;
    private Text foyerMoneyDisplay;
    private Text foyerPenurieDisplay;
    private Slider penurieSlider;

    void Start ()
    {
        timemanagerSlider = canvasStats.Find("SliderTime").GetComponent<Slider>();
        timemanagerDisplay = canvasStats.Find("DisplayTimeManager").GetComponent<Text>();
        growSlider = canvasStats.Find("SliderGrow").GetComponent<Slider>();
        growDisplay = canvasStats.Find("DisplayGrow").GetComponent<Text>();

        overSlider = canvasStats.Find("SliderOver").GetComponent<Slider>();
        underSlider = canvasStats.Find("SliderUnder").GetComponent<Slider>();
        displayOver = canvasStats.Find("DisplayOver").GetComponent<Text>();
        displayUnder = canvasStats.Find("DisplayUnder").GetComponent<Text>();

        forgeDisplay = canvasStats.Find("DisplayForge").GetComponent<Text>();
        moulinDisplay = canvasStats.Find("DisplayMoulin").GetComponent<Text>();
        boulangerieDisplay = canvasStats.Find("DisplayBoulangerie").GetComponent<Text>();
        scierieDisplay = canvasStats.Find("DisplayScierie").GetComponent<Text>();

        marketOutilsDisplay = canvasStats.Find("DisplayMarketOutils").GetComponent<Text>();
        marketBoisDisplay = canvasStats.Find("DisplayMarketBois").GetComponent<Text>();
        marketPainDisplay = canvasStats.Find("DisplayMarketPain").GetComponent<Text>();
        marketMineraiDisplay = canvasStats.Find("DisplayMarketMinerai").GetComponent<Text>();
        marketMoneyDisplay = canvasStats.Find("DisplayMarketMoney").GetComponent<Text>();

        penurieSlider = canvasSeigneur.Find("SliderPenurie").GetComponent<Slider>();
        npcWellbeingDisplay = canvasSeigneur.Find("DisplayWellbeing").GetComponent<Text>();
        foyerPenurieDisplay = canvasSeigneur.Find("DisplayPenurie").GetComponent<Text>();
        foyerMoneyDisplay = canvasSeigneur.Find("DisplayHouseMoney").GetComponent<Text>();

        sliderCens = canvasSeigneur.Find("SliderCens").GetComponent<Slider>();
        sliderChampart = canvasSeigneur.Find("SliderChampart").GetComponent<Slider>();
        sliderBanalite = canvasSeigneur.Find("SliderBanalite").GetComponent<Slider>();
        seigneurMoneyDisplay = canvasSeigneur.Find("DisplaySeigneurMoney").GetComponent<Text>();
        seigneurRessourceDisplay = canvasSeigneur.Find("DisplaySeigneurRessource").GetComponent<Text>();
        seigneurCensDisplay = canvasSeigneur.Find("DisplaySeigneurCens").GetComponent<Text>();
        seigneurChampartDisplay = canvasSeigneur.Find("DisplaySeigneurChampart").GetComponent<Text>();
        seigneurBanaliteDisplay = canvasSeigneur.Find("DisplaySeigneurBanalite").GetComponent<Text>();
    }

    void Update ()
    {
        timemanagerDisplay.text = "Jour: " + timemanager.getDay().ToString() + " (x" + ((timemanager.speedMultiplicator*10)/10) + ")";
        growDisplay.text = "Croissance matières: " + growSlider.value.ToString();

        displayOver.text = "Seuil surproduction: " + overSlider.value.ToString();
        displayUnder.text = "Seuil sousproduction: " + underSlider.value.ToString();

        forgeDisplay.text = "Outils produits: " + forge.getResourceCreatedDaily().ToString("0.00") + " (" +forge.getProductivity().ToString("0.00") +")";
        moulinDisplay.text = "Farine produite: " + moulin.getResourceCreatedDaily().ToString("0.00") + " (" + moulin.getProductivity().ToString("0.00") + ")";
        boulangerieDisplay.text = "Pains produits: " + boulangerie.getResourceCreatedDaily().ToString("0.00") + " (" + boulangerie.getProductivity().ToString("0.00") + ")";
        scierieDisplay.text = "Planches produites: " + scierie.getResourceCreatedDaily().ToString("0.00") + " (" + scierie.getProductivity().ToString("0.00") + ")";

        marketOutilsDisplay.text = "Outils en stock: " + outils.outputResource.ToString("0.00");
        marketBoisDisplay.text = "Bois en stock: " + bois.outputResource.ToString("0.00");
        marketPainDisplay.text = "Pains en stock: " + pain.outputResource.ToString("0.00");
        marketMineraiDisplay.text = "Minerai en stock: " + minerai.outputResource.ToString("0.00");

        float temp = 0; float cpt = 0;
        temp = outils.money + bois.money + pain.money + minerai.money;
        marketMoneyDisplay.text = "Argent total du marché: " + temp; 

        temp = 0; cpt = 0;
        foreach(ANPC npc in FindObjectsOfType<ANPC>())
        {
            temp += npc.getWellbeing(); cpt++;
        }
        temp /= cpt;
        npcWellbeingDisplay.text = "Moyenne de bien-être: " + temp.ToString("0.00") + " (" + cpt + ")" ;
        temp = 0; cpt = 0;
        foreach (Foyer foyer in FindObjectsOfType<Foyer>())
        {
            temp += foyer.gold; cpt++;
        }
        foyerMoneyDisplay.text = "Argent total des foyers: " + temp + " (" + cpt + ")";
        foyerPenurieDisplay.text = "Impact de la penurie: " + penurieSlider.value.ToString("0.00");

        seigneurMoneyDisplay.text = "Argent total: " + seigneur.money;
        seigneurRessourceDisplay.text = "Ressources: bois " + seigneur.wood.ToString("0") + " blé " + seigneur.wheat.ToString("0");
        seigneurCensDisplay.text = "Cens: " + seigneur.censValue;
        seigneurChampartDisplay.text = "Champart: " + seigneur.champartPercentage + "%";
        seigneurBanaliteDisplay.text = "Banalité: " + seigneur.banaliteValue;
    }

    public void SliderTimeValueChanged()
    {
        timemanager.speedMultiplicator = timemanagerSlider.value;
    }

    public void SliderGrowValueChanged()
    {
        foreach(Champ champ in FindObjectsOfType<Champ>())
        {
            champ.growRate = growSlider.value;
        }
        foreach(Foret foret in FindObjectsOfType<Foret>())
        {
            foret.growRate = growSlider.value;
        }
    }

    public void SliderPenurieValueChanged()
    {
        foreach (Foyer foyer in FindObjectsOfType<Foyer>())
            foyer.setPenurieFactor(penurieSlider.value);
    }

    public void SliderCensValueChanged()
    {
        seigneur.censValue = sliderCens.value;
    }
    public void SliderChampartValueChanged()
    {
        seigneur.champartPercentage = sliderChampart.value;
    }
    public void SliderBanaliteValueChanged()
    {
        seigneur.banaliteValue = sliderBanalite.value;
    }

    public void SliderOverValueChanged()
    {
        foreach(Atelier atelier in FindObjectsOfType<Atelier>())
        {
            atelier.seuilOver = Mathf.RoundToInt(overSlider.value);
        }
    }

    public void SliderUnderValueChanged()
    {
        foreach (Atelier atelier in FindObjectsOfType<Atelier>())
        {
            atelier.seuilUnder = Mathf.RoundToInt(underSlider.value);
        }
    }
}
