using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinoCounter : MonoBehaviour
{
    public TextMeshPro dinoCountText;
    public Transform dinosParent;

    void Update()
    {
        dinoCountText.text = dinosParent.childCount.ToString();
    }
}
