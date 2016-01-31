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

    [SerializeField]
    private SpriteRenderer demonho;

    [SerializeField]
    private BallonScript ballons;

    [SerializeField]
    private AudioSource errorSound;

    [SerializeField]
    private AudioSource rightSoud;

    [SerializeField]
    private AudioSource victorySound;

    [SerializeField]
    private AudioSource looseSound;

    private float actualTime;

    private int itemsCount;

    private bool isPlaying = true;

    // Use this for initialization
    void Start()
    {
        Roulet.onSpin += new RouletScript.boolReturnEventHandler(RouletSpined);
    }

    void Awake()
    {
        int initialItens = 0;
        int baseItenCount = 8;

        int baseInitialTime = 5;
        int baseTime = 5;

        int level = ApplicationModel.CurrentLevel + 1;

        //---------------------

        int itemQuantity = initialItens + baseItenCount * level;
        itemList = new Item[itemQuantity];
        for (int i = 0; i < itemQuantity; i++)
        {
            itemList[i] = new Item()
            {
                Name = i.ToString(),
                ImageID = i
            };
        }

        //--------------------

        initialTime = baseInitialTime + baseTime * level;

        actualTime = initialTime;
        itemsCount = itemList.Length;

        Roulet.onEnd += Roulet_onEnd;

        Roulet.ItemList = itemList;
        Roulet.LoadItens();
    }

    void Roulet_onEnd(RouletScript item, bool isOK)
    {
        if (isOK)
        {
            Victory();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("House");
            }
        }
        else
        {
            actualTime -= Time.deltaTime;
            UpdateHud();
        }
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

        if (actualTime <= 0)
        {
            Loose();
        }
    }

    private void Victory()
    {
        ballons.ShowBallon(true);
        ApplicationModel.CurrentLevel++;
        StartCoroutine(WinAnimation());
    }

    private void Loose()
    {
        ballons.ShowBallon(false);
        StartCoroutine(LooseAnimation());
    }

    private void Finish()
    {
        Roulet.gameObject.SetActive(false);
        isPlaying = false;
    }

    IEnumerator WinAnimation()
    {
        yield return null;

        float time = 0;
        float totalTime = 1f;
        
        float finalPosition = 0.5f;

        Color startColor = Color.clear;
        Color endColor = demonho.color;

        StartCoroutine(vAnimation(demonho.gameObject, finalPosition, totalTime*2));

        while (time < totalTime)
        {
            time += Time.deltaTime;
            yield return null;
            demonho.color = Color.Lerp(startColor, endColor, time / totalTime);
        }

        time = 0;

        while (time < totalTime)
        {
            time += Time.deltaTime;
            yield return null;
            demonho.color = Color.Lerp(endColor, startColor, time / totalTime);
        }

        demonho.gameObject.SetActive(false);

        Finish();
    }

    IEnumerator vAnimation(GameObject gObject, float yFinal, float totalTime)
    {
        float time = 0;

        while (time < totalTime)
        {
            time += Time.deltaTime;
            yield return null;
            gObject.transform.position += new Vector3(0, Mathf.Lerp(0, yFinal, time / totalTime), 0);
        }
    }

    IEnumerator LooseAnimation()
    {
        yield return null;
        Finish();
    }

}
