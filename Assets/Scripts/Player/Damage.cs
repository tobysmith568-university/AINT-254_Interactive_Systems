using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    [SerializeField]
    Image firstDamageRing;
    [SerializeField]
    Image secondDamageRing;


    private void Start()
    {
        SetAlpha(firstDamageRing, 0f);
        SetAlpha(secondDamageRing, 0f);

        StartCoroutine(Fade());
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }

    /// <summary>
    /// Called by a target if they shoot the player
    /// </summary>
    public void Shot()
    {
        if (secondDamageRing.color.a > 0.2f)
            GameController.Killed();

        if (firstDamageRing.color.a <= 0)
            SetAlpha(firstDamageRing, 1);
        else
            SetAlpha(secondDamageRing, 1);

        Debug.Log("1: " + firstDamageRing.color.a + " | 2: " + secondDamageRing.color.a);
    }

    private IEnumerator Fade()
    {
        while (true)
        {
            if (firstDamageRing.color.a > 0)
                SetAlpha(firstDamageRing, firstDamageRing.color.a - 0.025f);
            if (secondDamageRing.color.a > 0)
                SetAlpha(secondDamageRing, secondDamageRing.color.a - 0.025f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
