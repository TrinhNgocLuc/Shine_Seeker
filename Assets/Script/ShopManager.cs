using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public ControlCharacter character;
    public AXMovement ax;
    public int oxiPrice = 1;
    public int axdamagePrice = 1;
    public int lightPrice = 1;
    public Text oxiItemText;
    public Text axItemText;
    public Text lightItemText;

    private void Awake()
    {
        character = FindAnyObjectByType<ControlCharacter>();
        ax = FindAnyObjectByType<AXMovement>();
    }
    void Start()
    {
        oxiItemText.text = oxiPrice.ToString();
        axItemText.text = axdamagePrice.ToString();
        lightItemText.text = lightPrice.ToString();
    }

    public void BuyItem(string item)
    {
        switch (item)
        {
            case "Oxi":
                if (character.coutDiamond >= oxiPrice)
                {
                    character.coutDiamond -= oxiPrice;
                    oxiPrice += 2;
                    character.coutOxiMax += 15;
                    character.coutOxi = character.coutOxiMax;
                    character.coutdianmondText.text = character.coutDiamond.ToString();
                    character.coutOxiText.text = character.coutOxiMax.ToString();
                    oxiItemText.text = oxiPrice.ToString();

                }
                break;
            case "Ax":
                if (character.coutDiamond >= oxiPrice)
                {
                    character.coutDiamond -= oxiPrice;
                    character.coutdianmondText.text = character.coutDiamond.ToString();
                    ax.damage++;
                    axdamagePrice += 2;
                    axItemText.text = axdamagePrice.ToString();

                }
                break;
            case "Light":
                if (character.coutDiamond >= oxiPrice)
                {
                    character.coutDiamond -= oxiPrice;
                    character.coutdianmondText.text = character.coutDiamond.ToString();
                    character.lightCharacter.transform.localScale += new Vector3(5f, 5f, 0f);
                    lightPrice += 3;
                    lightItemText.text = lightPrice.ToString();

                }
                break;
            default:
                return;
        }
    }
}
