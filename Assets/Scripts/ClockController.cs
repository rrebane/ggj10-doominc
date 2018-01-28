﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour {

    private float currentValue;
    private float targetDegrees;

    public float defaultValue;
    public float minValue;
    public float maxValue;
    public float offsetDegrees;
    public float minDegrees;
    public float maxDegrees;
    public float moveSpeed;

    public float endIsNighValue;
    public AudioSource doomPlus;
    public AudioSource doomMinus;
    public AudioSource endIsNigh;

    // Debug stuff
    //private float updateDelta = 3.0f;
    //private float nextUpdate = float.NegativeInfinity;

    void Awake () {
        SetValue(defaultValue);
        SetRotation(Quaternion.Euler(0f, 0f, targetDegrees));
    }

    void Update () {
        // Debug stuff
        //float t = Time.time;

        //if (nextUpdate < t) {
        //    SetValue(Random.Range(minValue, maxValue));
        //    nextUpdate = t + updateDelta;
        //}

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetDegrees);
        SetRotation(Quaternion.Lerp(gameObject.transform.localRotation, targetRotation, moveSpeed * Time.deltaTime));
    }

    private void SetRotation(Quaternion q) {
        gameObject.transform.localRotation = q;
    }

    public float GetValue() {
        return currentValue;
    }

    public void SetValue(float val) {
        currentValue = Mathf.Clamp(val, minValue, maxValue);
        if (endIsNigh) {
            if (currentValue <= endIsNighValue) {
                endIsNigh.loop = true;
                endIsNigh.Play();
            } else {
                endIsNigh.Stop();
                endIsNigh.loop = false;
            }
        }

        float valueFraction = (currentValue - minValue) / (maxValue - minValue);
        targetDegrees = Mathf.Repeat(-(offsetDegrees + minDegrees + (maxDegrees - minDegrees) * valueFraction), 360f);
    }

    public void SetValueOffset(float offset) {
        if (offset < 0f) {
            if (doomMinus) {
                doomMinus.Play();
            }
        } else {
            if (doomPlus) {
                doomPlus.Play();
            }
        }
        SetValue(currentValue + offset);
    }
}
