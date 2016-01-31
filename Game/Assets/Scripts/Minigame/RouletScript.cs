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

    public delegate void boolReturnEventHandler(RouletScript item, bool isOK);

    public event boolReturnEventHandler onSpin;

    public event boolReturnEventHandler onEnd;

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

    public void LoadItens()
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
            newItemFrame.ChangeIcon();

            if (i == 0)
            {
                StartCoroutine("ActivateAnimation", newItemFrame);
                newItemFrame.IsActive = true;
            }
            else
            {
                newItemFrame.transform.localScale = new Vector3(1, 1, 0);
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

    IEnumerator ActivateAnimation(ItemFrameScript item)
    {
        yield return null;

        float time = 0;
        float totalTime = 0.4f;

        Vector3 startScale = item.transform.localScale;
        Vector3 endScale = startScale * 2f;

        while (time < totalTime && item.IsActive)
        {
            time += Time.deltaTime;
            yield return null;
			if(item)
		        item.transform.localScale = Vector3.Lerp(startScale, endScale, time / totalTime);

        }

        if (item.IsActive)
        {
            item.transform.localScale = endScale;
        }
        else
        {
            item.transform.localScale = startScale;
        }

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
            lSender.ChangeIcon();
            lSender.transform.localScale = new Vector3(1, 1, 0);
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
            StartCoroutine(ActivateAnimation(itemFrameList[nextItem % actualPool]));
            StartCoroutine(ActiveNext(itemFrameList[nextItem % actualPool]));
        }
        else
        {
            onEnd(this, true);
        }

        nextItem++;
    }
}
