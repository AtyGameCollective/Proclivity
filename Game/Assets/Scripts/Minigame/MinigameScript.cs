using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinigameScript : MonoBehaviour
{

    [SerializeField]
    private Item[] itemList;

    [SerializeField]
    private RouletScript Roulet;

    [SerializeField]
    private float initialTime;

    [SerializeField]
    private float timePenalty;

    [SerializeField]
    private Slider timeSlider;

    [SerializeField]
    private Text quantityNumber;

    private float actualTime;

    private int itemsCount;

    // Use this for initialization
    void Start()
    {
        Roulet.onSpin += new RouletScript.onSpinEventHandler(RouletSpined);
    }

    void Awake()
    {
        //---------------------
        //FAKE Itens
        int fakeQuantity = 30;
        itemList = new Item[fakeQuantity];
        for (int i = 0; i < fakeQuantity; i++)
        {
            itemList[i] = new Item()
            {
                Name = i.ToString(),
                ImageID = i
            };
        }

        //--------------------

        actualTime = initialTime;
        itemsCount = itemList.Length;

        Roulet.ItemList = itemList;
    }

    // Update is called once per frame
    void Update()
    {
        actualTime -= Time.deltaTime;
        UpdateHud();
    }

    private void RouletSpined(RouletScript roulet, bool isOK)
    {
        if (isOK)
        {
            itemsCount--;
        }
        else
        {
            actualTime -= timePenalty;
        }

        UpdateHud();
    }

    private void UpdateHud()
    {
        if (timeSlider != null)
        {
            timeSlider.maxValue = initialTime;
            timeSlider.value = actualTime;
        }

        if (quantityNumber != null)
        {
            quantityNumber.text = itemsCount.ToString();
        }
    }
}
