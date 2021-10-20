using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Tradable
{
    Lapuchy,
    WinoCzerw,
    WionBial,
    Fryty
}


public class OfferUI : MonoBehaviour
{



    [Header("Components")]
    public Button moreButton;
    public Text label;
    public Button lessButton;
    [Header("Settings")]
    public Tradable tradeableType;

    public int amount;

    public void logDebug()
    {
        Debug.Log("Hallo");
    }
    void resetState()
    {
        changeAmount(0);
    }

    public void onMoreButton()
    {
        changeAmount(amount + 1);
    }
    public void onLessButton()
    {
        changeAmount(amount - 1);
    }
    void changeAmount(int newAmount)
    {
        int resourceCont = 0 ;
        

        switch (tradeableType)
        {
            case Tradable.Lapuchy:
                resourceCont = StaticValues.Lapuszki;
                break;
            case Tradable.WinoCzerw:
                resourceCont = StaticValues.WinoCzerwone;
                break;
            case Tradable.WionBial:
                resourceCont = StaticValues.WInoBiale;
                break;
            case Tradable.Fryty:
                resourceCont = StaticValues.Frytki;
                break;
            default:
                Debug.LogError("invalid resource count");
                resourceCont = 0;
                break;
        }

        if (newAmount > resourceCont)
        {
            newAmount = resourceCont;
        }

        if (newAmount < 0)
        {
            newAmount = 0;
        }
        amount = newAmount;
        label.text = amount.ToString();
    }

    private void Awake()
    {


        foreach (var image in GetComponentsInChildren<Image>())
        {
            image.raycastTarget = image.GetComponent<Button>() != null;

        }
        foreach (var text in GetComponentsInChildren<Text>())
        {
            text.raycastTarget = text.GetComponent<Button>() != null;
        }
 

        moreButton.onClick.AddListener(onMoreButton);
        lessButton.onClick.AddListener(onLessButton);
        resetState();
    }


}
