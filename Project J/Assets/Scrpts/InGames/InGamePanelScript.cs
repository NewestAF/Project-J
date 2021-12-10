using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InGamePanelScript : MonoBehaviour
{

    public int moneyMax;

    public int moneyCurrent;

    public int moneyIncome;

    float _incomeTimer;

    public UnitData[] Units;
    public GameObject buttonPrefab;
    public Text moneyText;
    public GameObject unitSummonGruops;


    void Start()
    {
        moneyMax = 1000;
        moneyCurrent = 0;
        moneyIncome = 5;

        for (int i = 0; i < Units.Length; i++)
        {
            GameObject newbutton = Instantiate(buttonPrefab, unitSummonGruops.transform);
            Button newButton_ = newbutton.GetComponent<Button>();
            newButton_.image.sprite = Units[i].UnitIcon;
            newbutton.transform.GetChild(0).GetComponent<Text>().text = Units[i].UnitPrice.ToString();
                              //UnitPrice
            int index = i;
            newButton_.onClick.AddListener(delegate { BuyUnit(index); });
        }   
        
    }

    void Update()
    {
        IncomeMoney();
        DisplayMoney();
    }

    void IncomeMoney()
    {
        _incomeTimer += Time.deltaTime;

        if (_incomeTimer >= 0.1f)
            if (moneyCurrent < moneyMax)
            {
                moneyCurrent++;
                _incomeTimer = 0;
            }
    }

    void DisplayMoney()
    {
        moneyText = gameObject.transform.GetChild(0).GetComponent<Text>();
        moneyText.text = moneyCurrent.ToString() + "/" + moneyMax.ToString();
    }


    public void BuyUnit(int i)
    {
        if (Units[i].UnitPrice <= moneyCurrent)
        {
            Debug.Log(Units[i].UnitName + " bought");
            moneyCurrent -= Units[i].UnitPrice;
            UnitQueue.GetInstance().EnqueueUnit(Units[i]);
        }
        else
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
    }



}
