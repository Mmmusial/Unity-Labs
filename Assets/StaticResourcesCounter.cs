using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticResourcesCounter : MonoBehaviour
{

    public Text WinoCzerwoneText;
    public Text WInoBialeText;
    public Text LapuszkiText;
    public Text FrytkiText;
    public Text HajsText;



    void updateResources()
    {
        WinoCzerwoneText.text = StaticValues.WinoCzerwone.ToString();
        WInoBialeText.text = StaticValues.WInoBiale.ToString();
        LapuszkiText.text = StaticValues.Lapuszki.ToString();
        FrytkiText.text = StaticValues.Frytki.ToString();
        HajsText.text = StaticValues.Hajs.ToString();

    }

    // Start is called before the first frame update
    void Start()
    {
        updateResources();
        StaticValues.updateEvent += updateResources;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
