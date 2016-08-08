using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class XPandLife : MonoBehaviour 
{
		
	
	//Bloc gestion XP
	public Text RecapXP; // poser le texte rappelant le montant d'XP actuel et necessaire
	public Image FillXP; // foreground de la barre d'XP
	int CurrentXP = 0; //j'ai 0 XP au début
	int MaxXP = 100; // il me faut 100 XP pour up
	int Excedent = 0; 
	float Reste = 0f;
	float ValeurXP = 0f;
	//Bloc Gestion Level
	public Text RecapLevel; 
	int CurrentLevel = 1;
	int MaxLevel = 50;
	//Bloc Gestion PV
	public Text RecapPV;
	public Image FillPV;
	  int CurrentPV = 1;
	  int MaxPV = 50;
	  float ValeurVie = 0f;
	//Bloc Gestion PM
	public Text RecapPM;
	public Image FillPM;
	  int CurrentPM = 1;
	  int MaxPM = 50;
	  float ValeurMagie = 0f;
	  //Bloc Multiplicateurs et regen
	  float Regen = 1.0f;
	  float ActualPM;
	  float ActualPV;
	  float ActualXP;
	  float MultiPM = 1.08f;
	  float MultiPV = 1.1f;
	  float MultiXP = 1.15f;
	  float MultiRegen = 0.18f;

	
	public float ValueXP 
    {
	get 
	{
	    if(FillXP != null)
	        return (float)(FillXP.fillAmount*100);	
	    else
	        return 0;	
	}
	set 
	{
 	    if(FillXP != null)
	        FillXP.fillAmount = value/100f;	
	} 
     }

public float ValuePV
    {
	get 
	{
	    if(FillPV != null)
	        return (float)(FillPV.fillAmount*100);	
	    else
	        return 0;	
	}
	set 
	{
 	    if(FillPV != null)
	        FillPV.fillAmount = value/100f;	
	} 
     }

     public float ValuePM
    {
	get 
	{
	    if(FillPM != null)
	        return (float)(FillPM.fillAmount*100);	
	    else
	        return 0;	
	}
	set 
	{
 	    if(FillPM != null)
	        FillPM.fillAmount = value/100f;	
	} 
     }


	void Start()
	{
		
		//Debug.Log("Regeneration à " + Regen);
		//Debug.Log("XP Max " + MaxXP + " - XPActu " + CurrentXP + " - Reste " + Reste );

		FillXP = FillXP.GetComponent<Image>();
		ValueXP = (CurrentXP / MaxXP) * 100f;

		RecapXP.text = CurrentXP + " / " + MaxXP + " Exp";
		RecapLevel.text = "Niveau " + CurrentLevel;


		FillPV = FillPV.GetComponent<Image>();
		ValuePV = (CurrentPV / MaxPV) * 100f;
		RecapPV.text = CurrentPV + " / " + MaxPV;


		FillPM = FillPM.GetComponent<Image>();		
		ValuePM = (CurrentPM / MaxPM) * 100f;
		RecapPM.text = CurrentPM + " / " + MaxPM;


		
	}

	

	void Update()
	{
		FillXP = FillXP.GetComponent<Image>();
		ValueXP = ValeurXP;
		ValeurXP = (ActualXP / MaxXP) * 100f;

		/*TEST
		ActualXP = ActualXP + 1500.0f;
         
        CurrentXP = Mathf.RoundToInt(ActualXP);
        if(CurrentLevel >= MaxLevel)
        {
        	CurrentXP = MaxXP;
        	CurrentPV = MaxPV;
        	CurrentPM = MaxPM;
        }
		TEST*/


		RecapXP.text = CurrentXP + " / " + MaxXP + " Exp";
		RecapLevel.text = "Niveau " + CurrentLevel;


		FillPV = FillPV.GetComponent<Image>();
		ValuePV = ValeurVie;
		ValeurVie = (ActualPV / MaxPV) * 100f;
		RecapPV.text = CurrentPV + " / " + MaxPV;


		FillPM = FillPM.GetComponent<Image>();		
		ValuePM = ValeurMagie;
		ValeurMagie = (ActualPM / MaxPM) * 100f;
		RecapPM.text = CurrentPM + " / " + MaxPM;
		
		
		
		RegenSystem();

		XPGain();
		
		
		
	}


		void XPGain()
		{
			if(CurrentXP >= MaxXP && CurrentLevel != MaxLevel)
		{
			Reste = ActualXP - MaxXP;

			LevelUpSystem();
		}
		if(CurrentXP >= MaxXP && CurrentLevel == MaxLevel)
		{
			CurrentXP = MaxXP;
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			ActualXP = ActualXP + 15.0f;
         
            CurrentXP = Mathf.RoundToInt(ActualXP);
            

		}


		}

		void OnTriggerEnter(Collider XPPoint)
		{
			ActualXP = ActualXP + 15;
			CurrentXP = Mathf.RoundToInt(ActualXP);
		}

		

		void LevelUpSystem()
		{
			RecapXP.text = CurrentXP + " / " + MaxXP + " Exp";

			CurrentLevel++;
			Excedent = Mathf.RoundToInt(Reste);
			
			ValueXP = 0f;
			ActualXP = 0f;
			CurrentXP = 0;
			Regen += (Regen * MultiRegen);

			//Debug.Log("XP Max " + MaxXP + " - XPActu " + CurrentXP + " - Reste " + Reste );
			//Debug.Log("Regeneration à " + Regen); 

			MaxPM = Mathf.RoundToInt(MaxPM * MultiPM);
						

			MaxPV = Mathf.RoundToInt(MaxPV * MultiPV);
						

			MaxXP = Mathf.RoundToInt(MaxXP * MultiXP);
			CurrentXP = CurrentXP + Excedent;
			Excedent = 0;
			Reste = 0;

			//Debug.Log("XP Max " + MaxXP + " - XPActu " + CurrentXP + " - Reste " + Reste );

			RegenSystem();			

			

		}


		void RegenSystem()
		{
			if(CurrentPV != MaxPV)
			{
				CurrentPV = Mathf.RoundToInt(ActualPV);
				ActualPV += Regen * Time.deltaTime;
			}

			if(CurrentPM != MaxPM)
			{
				CurrentPM = Mathf.RoundToInt(ActualPM);
				ActualPM += Regen * Time.deltaTime;
			}

			


			/*if(Collider )  si le joueur passe dans le cube de regen
			{
				  augmenter la regen de 4 points
			}*/



		}
		
}
