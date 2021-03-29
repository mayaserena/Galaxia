﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private List<CraftTypeScriptableObject> craftList;
    private ItemScriptableObject[] itemSOList;
    private int[] amountList;
    private int itemCount;
    private int amountNeeded;
    private bool notEnough;
    private CraftTypeScriptableObject craftType;
    public List<GameObject> buttonList;

    private void Start()
    {
        GameObject currentTemplate;
        GameObject craftTemplate = transform.GetChild(0).gameObject;

        foreach (CraftTypeScriptableObject craft in craftList)
        {
            currentTemplate = Instantiate(craftTemplate, transform);
            currentTemplate.GetComponent<InitiateBuild>().craftTypeSO = craft;
            currentTemplate.transform.GetChild(0).GetComponent<Text>().text = craft.craftName;
            currentTemplate.transform.GetChild(1).GetComponent<Image>().sprite = craft.craftSprite;
            currentTemplate.transform.GetChild(2).GetComponent<Image>().sprite = craft.itemsNeeded[0].itemSprite;
            currentTemplate.transform.GetChild(3).GetComponent<Text>().text = craft.amountNeeded[0].ToString();

            if (craft.itemsNeeded.Length == 2)
            {
                currentTemplate.transform.GetChild(4).GetComponent<Image>().sprite = craft.itemsNeeded[1].itemSprite;
                currentTemplate.transform.GetChild(5).GetComponent<Text>().text = craft.amountNeeded[1].ToString();
            }

            buttonList.Add(currentTemplate);
        }

        Destroy(craftTemplate);
    }

    private void Update()
    {
        // Check which crafts are buildable
        // Iterate through craft button list
        foreach (GameObject button in buttonList)
        {
            notEnough = false;
            craftType = button.GetComponent<InitiateBuild>().craftTypeSO;
            itemSOList = craftType.itemsNeeded;
            amountList = craftType.amountNeeded;

            // Iterate over items needed, check amount needed
            for (int i = 0; i < itemSOList.Length; i++)
            {
                itemCount = itemSOList[i].count;
                amountNeeded = amountList[i];

                // If player doesn't have enough items, disable the craft button
                if (itemCount < amountNeeded)
                {
                    button.GetComponent<Button>().interactable = false;
                    button.GetComponent<CanvasGroup>().alpha = 0.5f;
                    notEnough = true;
                }
            }

            // If player has enough items, enable button
            if (!notEnough)
            {
                button.GetComponent<Button>().interactable = true;
                button.GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
    }
}
