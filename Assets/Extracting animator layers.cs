using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    Animator ac;
    int layerCount;

    private void Start()
    {
        ac = GetComponent<Animator>();
        layerCount = ac.layerCount;

        for (int layer = 0; layer < layerCount; layer++)
        {
            Debug.Log(string.Format("Layer {0}: {1}", layer, ac.GetLayerName(layer)));
        }


    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {

        }
    }
}
