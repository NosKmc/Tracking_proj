using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using System;
using System.Linq;

public class FaceParam
{
    public float RightEyeRatio { get { return RightEyeWindow.Value; } }
    public float LeftEyeRatio { get { return LeftEyeWindow.Value; } }
    public float MouthRatio { get { return MouthWindow.Value; } }
    public float FaceYaw { get { return FaceYawWindow.Value; } }
    public float FacePitch { get { return FacePitchWindow.Value; } }
    public float FaceRoll { get { return FaceRollWindow.Value; } }

    private MovingWindow RightEyeWindow;
    private MovingWindow LeftEyeWindow;
    private MovingWindow MouthWindow;
    private MovingWindow FaceYawWindow;
    private MovingWindow FacePitchWindow;
    private MovingWindow FaceRollWindow;

    private const int WINDOW_SIZE = 6;

    public FaceParam()
    {
        RightEyeWindow = new MovingWindow(WINDOW_SIZE);
        LeftEyeWindow = new MovingWindow(WINDOW_SIZE);
        MouthWindow = new MovingWindow(WINDOW_SIZE);
        FaceYawWindow = new MovingWindow(WINDOW_SIZE);
        FacePitchWindow = new MovingWindow(WINDOW_SIZE);
        FaceRollWindow = new MovingWindow(WINDOW_SIZE);
    }
    public void CalcEyeRatio(Point[] _points)
    {
        Point[] points = _points;
        Point[] reyePoints = new Point[6];
        Point[] leyePoints = new Point[6];

        //右目は37番から
        Array.Copy(points, 36, reyePoints, 0, 6);
        int horizontal = Point.DistancePow2(reyePoints[0], reyePoints[3]);
        if (horizontal == 0) return;
        int vertical = Point.DistancePow2(reyePoints[1], reyePoints[5]);
        RightEyeWindow.CalcValue((float)vertical / (float)horizontal);
        //左目は43番から
        Array.Copy(points, 42, leyePoints, 0, 6);
        horizontal = Point.DistancePow2(leyePoints[0], leyePoints[3]);
        if (horizontal == 0) return;
        vertical = Point.DistancePow2(leyePoints[1], leyePoints[5]);
        LeftEyeWindow.CalcValue((float)vertical / (float)horizontal);
    }

    public void CalcMouthRatio(Point[] _points)
    {
        Point[] points = _points;
        Point[] mouthPoints = new Point[8];
        //口の内側は61番から
        Array.Copy(points, 60, mouthPoints, 0, 8);
        int horizontal = Point.DistancePow2(mouthPoints[0], mouthPoints[4]);
        if (horizontal == 0) return;
        int vertical = Point.DistancePow2(mouthPoints[2], mouthPoints[6]);
        MouthWindow.CalcValue((float)vertical / (float)horizontal);
    }

    public void CalcHeadPose(Point[] points, int camWidth, int camHeight)
    {
        MatOfPoint3d modelPoints;
        modelPoints = new MatOfPoint3d()
        {
            new Point3d(0.0f, 0.0f, 0.0f),
            new Point3d(0.0f, -330.0f, -65.0f),
            new Point3d(-225.0f, 170.0f, -135.0f),
            new Point3d(225.0f, 170.0f, -135.0f),
            new Point3d(-150.0f, -150.0f, -125.0f),
            new Point3d(150.0f, -150.0f, -125.0f)
        };

        MatOfPoint2d marks;
        marks = new MatOfPoint2d()
        {
            points[30], points[8], points[36],
            points[45], points[48], points[54]
        };

        double focal_length = camWidth;
        double centerX = (double)camWidth / 2.0f;
        double centerY = (double)camHeight / 2.0f;
        double[,] cameraarr;
        cameraarr = new double[3, 3]
        {
            { focal_length, 0.0f, centerX },
            { 0.0f, focal_length, centerY },
            { 0, 0, 1 }
        };
        MatOfDouble cameraMat = MatOfDouble.FromArray(cameraarr);

        MatOfDouble distCoeffs = new MatOfDouble(new Size(1, 4), 0.0d);
        MatOfDouble vecRot = new MatOfDouble();
        MatOfDouble vecTr = new MatOfDouble();

        Cv2.SolvePnP(modelPoints, marks, cameraMat, distCoeffs, vecRot, vecTr);

        MatOfDouble matRot = new MatOfDouble(new Size(3, 3));
        Cv2.Rodrigues(vecRot, matRot);
        MatOfDouble matProj = new MatOfDouble(3, 4, 0.0f); // Sizeとコンストラクタはcolとrowが逆
        matProj[0,3,0,3] = matRot;
        Mat eulerAngles = new Mat();
        Cv2.DecomposeProjectionMatrix(matProj, cameraMat, matRot, vecTr, eulerAngles:eulerAngles);
        float yaw = (float)eulerAngles.At<double>(1);
        float pitch = (float)eulerAngles.At<double>(0);
        float roll = (float)eulerAngles.At<double>(2);
        Debug.Log("yaw: "+yaw+"pitch: "+pitch+"roll: "+roll);
        FaceYawWindow.CalcValue(yaw);
        FacePitchWindow.CalcValue(pitch);
        FaceRollWindow.CalcValue(roll);
    }

    public void CalcParams(Point[] points)
    {
        CalcEyeRatio(points);
        CalcMouthRatio(points);
        CalcHeadPose(points, 256, 192);
    }
}
