using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    Material theMaterial;
    public Animator UIanim;
    [Range(0f, 5f)] [SerializeField]
    public float shown;

    void Start()
    {
        theMaterial = this.GetComponent<Image>().material;
    }

    void Update()
    {
        theMaterial.SetFloat("_Show", shown);
    }    
}
