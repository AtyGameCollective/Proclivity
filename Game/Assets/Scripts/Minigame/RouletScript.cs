using UnityEngine;
using System.Collections;

public class RouletScript : MonoBehaviour
{

    [SerializeField]
    private Item[] itemList;

    [SerializeField]
    private int poolSize = 10;

    [SerializeField]
    private ItemFrameScript baseItemFrame;

    [SerializeField]
    private int itemDistance = 1;

    private ItemFrameScript[] itemFrameList;

    private int nextItem = 0;

    private int nextPoolPosition = 0;

    private int actualPool;

    // Use this for initialization
    void Start()
    {

        actualPool = poolSize;

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

        if (itemList.Length < actualPool)
            actualPool = itemList.Length;

        itemFrameList = new ItemFrameScript[actualPool];

        int positionX = (actualPool / 2) * itemDistance;

        for (int i = 0; i < actualPool; i++)
        {
            ItemFrameScript newItemFrame = Instantiate(baseItemFrame);
            newItemFrame.transform.position = new Vector3(positionX, 0, 0);
            newItemFrame.Button = (ItemFrameScript.buttons)(Random.value * 4);

            if (i == 0)
            {
                newItemFrame.IsActive = true;
            }
            else
            {
                newItemFrame.IsActive = false;
            }

            newItemFrame.onUse += SpinRoulet;
            newItemFrame.Item = itemList[i];

            itemFrameList[i] = newItemFrame;

            positionX -= itemDistance;

        }

        nextItem = 1;
        nextPoolPosition = actualPool;

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ActiveNext(ItemFrameScript item)
    {
        yield return new WaitForEndOfFrame();
        item.IsActive = true;

    }

    void SpinRoulet(object sender, System.EventArgs e)
    {
        ItemFrameScript lSender = (ItemFrameScript)sender;

        Debug.Log("S: " + lSender.Item.Name);

        foreach (var item in itemFrameList)
        {
            if (item != null)
            {
                item.transform.position = new Vector3(item.transform.position.x + itemDistance, 0, 0);
                item.IsActive = false;
            }
        }

        if (itemList.Length > nextPoolPosition)
        {
            lSender.Item = itemList[nextPoolPosition];
            lSender.ChangeButton((ItemFrameScript.buttons)(Random.value * 4));
            lSender.transform.position = new Vector3(-((poolSize / 2 - 1) * itemDistance), 0, 0);
            lSender.IsActive = false;

            nextPoolPosition++;
        }
        else
        {
            GameObject.Destroy(lSender.gameObject);
        }

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
