﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class AspectController : MonoBehaviour
{
    [SerializeField] private float orthographicSize = 1;
    [SerializeField] private int screenHeight = 1080;
    [SerializeField] private int screenWidth = 1920;

    private void Start()
    {
        var camera = GetComponent<Camera>();
        var heightAsFloat = (float)screenHeight;
        var widthAsFloat = (float)screenWidth;
        var aspectRatio = widthAsFloat / heightAsFloat;

        camera.projectionMatrix = Matrix4x4.Ortho(
        -orthographicSize * aspectRatio,
            orthographicSize * aspectRatio,
        -orthographicSize,
            orthographicSize,
        camera.nearClipPlane,
            camera.farClipPlane);
    }

}
