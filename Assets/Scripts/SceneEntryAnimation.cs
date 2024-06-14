using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntryAnimation : MonoBehaviour
{
    public Animator fadeAnimator; // Animator of the fade panel

    void Start()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }
}
