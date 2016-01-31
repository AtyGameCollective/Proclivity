using UnityEngine;
using System.Collections;

public class RouletScript : MonoBehaviour
{

    [SerializeField]
    private Item[] itemList;

    public Item[] ItemList
    {
        get { return itemList; }
        set { itemList = value; }
    }

    [SerializeField]
    private int poolSize = 10;

    [SerializeField]
    private ItemFrameScript baseItemFrame;

    [SerializeField]
    private int itemDistance = 24;

    [SerializeField]
    private Vector2 ampl = new Vector2(3f, 1.5f);

    [SerializeField]
    private int turnSpeed = 2;

    [SerializeField]
    private int initialAngle = 60;

    public delegate void onSpinEventHandler(RouletScript item, bool idOK);
    public event onSpinEventHandler onSpin;

    private ItemFrameScript[] itemFrameList;

    private int nextItem = 0;

    private int nextPoolPosition = 0;

    private int actualPool;

    private Vector3 GetAngularPosition(float position)
    {
        Vector3 rPosition = Vector3.zero;

        float angle = (position * (itemDistance * Mathf.Deg2Rad)) + initialAngle * Mathf.Deg2Rad;

        rPosition.x = Mathf.Cos(angle) * ampl.x;
        rPosition.y = Mathf.Sin(angle) * ampl.y;

        return rPosition;
    }

    // Use this for initialization
    void Start()
    {
    }

    void Awake()
    {
        actualPool = poolSize;

        if (itemList.Length < actualPool)
            actualPool = itemList.Length;

        itemFrameList = new ItemFrameScript[actualPool];

        for (int i = 0; i < actualPool; i++)
        {
            ItemFrameScript newItemFrame = Instantiate(baseItemFrame);
            newItemFrame.transform.localPosition = GetAngularPosition(i);
            newItemFrame.Button = (ItemFrameScript.buttons)(Random.value * 4);
            newItemFrame.realPosition = actualPool;
            newItemFrame.position = actualPool;
            newItemFrame.destinyPosition = i;

            if (i == 0)
            {
                newItemFrame.IsActive = true;
            }
            else
            {
                newItemFrame.IsActive = false;
            }

            newItemFrame.onUse += new ItemFrameScript.onUseEventHandler(SpinRoulet);
            newItemFrame.Item = itemList[i];

            newItemFrame.transform.SetParent(transform);

            itemFrameList[i] = newItemFrame;

        }

        nextItem = 1;
        nextPoolPosition = actualPool;
    }

    // Update is called once per frame
    void Update()
    {
        if (itemFrameList == null)
            return;

        //Turn animation
        foreach (var item in itemFrameList)
        {
            if (item != null)
            {

                if (item.position != item.destinyPosition)
                {
                    item.realPosition = Mathf.Lerp(item.realPosition, item.destinyPosition, Time.deltaTime * turnSpeed);

                    if (item.realPosition <= item.destinyPosition)
                    {
                        item.position = item.destinyPosition;
                        item.realPosition = item.position;
                    }

                    item.transform.localPosition = GetAngularPosition(item.realPosition);
                }
            }
        }
    }

    IEnumerator ActiveNext(ItemFrameScript item)
    {
        yield return new WaitForEndOfFrame();
        item.IsActive = true;

    }

    void SpinRoulet(object sender, bool isOK)
    {
        if (!isOK)
        {
            if (onSpin != null)
                onSpin(this, false);

            return;
        }


        ItemFrameScript lSender = (ItemFrameScript)sender;

        foreach (var item in itemFrameList)
        {
            if (item != null)
            {
                item.destinyPosition--;
                item.IsActive = false;
            }
        }

        if (itemList.Length > nextPoolPosition)
        {
            lSender.Item = itemList[nextPoolPosition];
            lSender.ChangeButton((ItemFrameScript.buttons)(Random.value * 4));
            lSender.destinyPosition = actualPool - 1;
            lSender.position = actualPool - 1;
            lSender.transform.localPosition = GetAngularPosition(lSender.position);
            lSender.IsActive = false;

            nextPoolPosition++;
        }
        else
        {
            GameObject.Destroy(lSender.gameObject);
        }

        if (onSpin != null)
            onSpin(this, true);

        if (itemList.Length > nextItem)
        {
            StartCoroutine(ActiveNext(itemFrameList[nextItem % actualPool]));
        }
        else
        {
            //TODO: Finish Minigame
        }

        nextItem++;
    }
}
