using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

public class Live2DManager : MonoBehaviour
{
    private CubismModel _model;
    private float _t;
    public FaceParam faceParam;

    // Start is called before the first frame update
    void Start()
    {
        _model = this.FindCubismModel();
    }

    public void SetFaceParam(FaceParam param)
    {
        faceParam = param;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*
        _t += (Time.deltaTime * 4f);
        var value = Mathf.Sin(_t) * 30f;
        var parameter = _model.Parameters[2];
        parameter.Value = value;
        var mouthParameter = _model.Parameters[18];
        var reyeParameter = _model.Parameters[5];
        var leyeParameter = _model.Parameters[3];
        var faceYawParameter = _model.Parameters[0];
        var facePitchParameter = _model.Parameters[1];
        var faceRollParameter = _model.Parameters[2];
        */
        var mouthParameter = _model.Parameters[12];
        var reyeParameter = _model.Parameters[5];
        var leyeParameter = _model.Parameters[3];
        var faceYawParameter = _model.Parameters[0];
        var facePitchParameter = _model.Parameters[1];
        var faceRollParameter = _model.Parameters[2];

        mouthParameter.Value = faceParam.MouthRatio * 2;
        reyeParameter.Value = faceParam.RightEyeRatio * 6 - 0.02f;
        leyeParameter.Value = faceParam.LeftEyeRatio * 6 - 0.02f;
        faceYawParameter.Value = faceParam.FaceYaw * -4.0f;
        facePitchParameter.Value = (faceParam.FacePitch - 180.0f) * 13.0f;
        faceRollParameter.Value = faceParam.FaceRoll * 3.0f;
    }
}
