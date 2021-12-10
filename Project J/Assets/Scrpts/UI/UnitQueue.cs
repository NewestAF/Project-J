using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitQueue : MonoBehaviour
{
    private static UnitQueue instance;

    bool isTraining = false;

    Queue<UnitData> unitTrainingQueue = new Queue<UnitData>();
    UnitData currentTrainingUnit;

    float trainingTime;
    float currentTrainingTime;

    CastleScript castle;

    [SerializeField] Slider sliderGauge;
    [SerializeField] Image[] image_trainingUnits;


    private void Awake()
    {
        instance = this;
        castle = GameObject.Find("Castle P1").GetComponent<CastleScript>();
    }

    public static UnitQueue GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("UnitQueue �ν��Ͻ��� �������� �ʽ��ϴ�.");
            return null;
        }
        return instance;
    }

    private void Update()
    {
        if (!IsFinish())
        {
            Training();
        }
    }

    bool IsFinish()
    {
        if (unitTrainingQueue.Count == 0 && !isTraining)
        {
            sliderGauge.gameObject.SetActive(false);
            return true;
        }
        else
        {
            sliderGauge.gameObject.SetActive(true);
            return false;
        }
    }

    void Training()
    {
        if (!isTraining && unitTrainingQueue.Count != 0)
        {
            DequeueUnit();
        }
        else if (isTraining) //�Ʒ����� ���
        {
            currentTrainingTime += Time.deltaTime;
            sliderGauge.value = currentTrainingTime;

            if (currentTrainingTime >= trainingTime)
            {
                TrainingComplete();
            }

        }
    }

    void DequeueUnit()
    {
        isTraining = true;
        currentTrainingUnit = unitTrainingQueue.Dequeue();

        trainingTime = currentTrainingUnit.UnitTrainingTime;
        currentTrainingTime = 0;
        sliderGauge.maxValue = trainingTime;

        TraiingImageChange();
    }

    void TraiingImageChange()
    {
        image_trainingUnits[0].gameObject.SetActive(true);

        for (int i = 0; i < unitTrainingQueue.Count + 1; i++)
        {
            image_trainingUnits[i].sprite = image_trainingUnits[i + 1].sprite;

            if (i + 1 == unitTrainingQueue.Count + 1)
            {
                image_trainingUnits[i + 1].gameObject.SetActive(false);
            }
        }

    }

    public void EnqueueUnit(UnitData _unitData)
    {
        if (unitTrainingQueue.Count < 4)
        {
            unitTrainingQueue.Enqueue(_unitData);

            image_trainingUnits[unitTrainingQueue.Count].gameObject.SetActive(true);
            image_trainingUnits[unitTrainingQueue.Count].sprite = _unitData.UnitIcon;
        }
    }

    void TrainingComplete()
    {
        isTraining = false;
        image_trainingUnits[0].gameObject.SetActive(false);

        castle.SummonUnit(currentTrainingUnit);
    }


}
