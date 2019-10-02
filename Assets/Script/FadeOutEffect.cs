using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeOutEffect : MonoBehaviour
{
    SpriteRenderer rend;
    private float time = 10f;

    private void Start() {
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine("waiting");
        
    }

    IEnumerator waiting() {
        yield return new WaitForSeconds(time);
        StartCoroutine("FadeOut");
    }
    
    IEnumerator FadeOut() {
        for(float f = 1f; f>= 0f; f -= 0.05f) {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
            
        }
        yield return new WaitForSeconds(time);
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn() {
        for (float f = 0f; f <= 1f; f += 0.05f) {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(time);
        StartCoroutine("FadeOut");
    }

}
