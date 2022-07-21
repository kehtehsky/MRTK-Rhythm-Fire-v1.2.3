using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RightShiftScript : MonoBehaviour
{
    public SpriteRenderer sprite;



    // Start is called before the first frame update
    void Start()
    {


        transform.DOMoveX(0.6f, 1).SetLoops(-1, LoopType.Yoyo);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
