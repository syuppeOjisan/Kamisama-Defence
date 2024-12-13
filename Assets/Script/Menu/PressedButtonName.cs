using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PressedButtonName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DownKeyCheck();
    }

    void DownKeyCheck()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //èàóùÇèëÇ≠
                    Debug.Log("ç°âüÇ≥ÇÍÇΩÉLÅ[ÇÕ" + code + "Ç≈Ç∑");
                    break;
                }
            }
        }
    }
}
