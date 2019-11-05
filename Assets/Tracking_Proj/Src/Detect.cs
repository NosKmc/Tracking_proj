﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using OpenCvSharp.Demo;

public class Detect : WebCamera
{
    public TextAsset faces;
    public TextAsset eyes;
    public TextAsset shapes;

    private FaceProcessorLive<WebCamTexture> processor;

    protected override void Awake()
    {
        base.Awake();
        base.forceFrontalCamera = true; // we work with frontal cams here, let's force it for macOS s MacBook doesn't state frontal cam correctly

        processor = new FaceProcessorLive<WebCamTexture>();
        processor.Initialize(faces.text, eyes.text, shapes.bytes);

        // data stabilizer - affects face rects, face landmarks etc.
        processor.DataStabilizer.Enabled = true;        // enable stabilizer
        processor.DataStabilizer.Threshold = 2.0;       // threshold value in pixels
        processor.DataStabilizer.SamplesCount = 2;      // how many samples do we need to compute stable data

        // performance data - some tricks to make it work faster
        processor.Performance.Downscale = 256;          // processed image is pre-scaled down to N px by long side
        processor.Performance.SkipRate = 0;             // we actually process only each Nth frame (and every frame for skipRate = 0)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        // detect everything we're interested in
        processor.ProcessTexture(input, TextureParameters);

        // mark detected objects
        processor.MarkDetected();

        // processor.Image now holds data we'd like to visualize
        output = Unity.MatToTexture(processor.Image, output);   // if output is valid texture it's buffer will be re-used, otherwise it will be re-created

        return true;
    }
}
