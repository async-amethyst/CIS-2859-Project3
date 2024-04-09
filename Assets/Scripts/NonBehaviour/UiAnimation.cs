using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UiAnimation
{
    public static IEnumerator LerpElement(Transform obj, Vector3 goal, float duration = 1.0f)
    {
        float lerpAmt = 0.0f;
        float lerpFactor = 0.01f*(1.0f / duration);
        Vector3 startPos = obj.position;
        WaitForSeconds waitObj = new WaitForSeconds(0.01f);
        while(lerpAmt < 1.0f)
        {
            lerpAmt += lerpFactor;
            obj.position = Vector3.Lerp(startPos, goal, lerpAmt);
            yield return waitObj;
        }
    }
}
