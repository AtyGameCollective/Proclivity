using UnityEngine;
using System.Collections;

public class creditScreen : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButtonDown("Fire1"))
        {
            if (Input.anyKeyDown)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
