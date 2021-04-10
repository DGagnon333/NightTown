using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SFX : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void PlayClick()
    {
        click.Play();
    }
}
