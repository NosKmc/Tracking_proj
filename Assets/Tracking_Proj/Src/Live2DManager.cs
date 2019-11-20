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
        _t += (Time.deltaTime * 1.5f);
        var value = Mathf.Sin(_t) * 1.0f;
        var breath = _model.Parameters[18];
        breath.Value = value;
        /*
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
        float reye = faceParam.RightEyeRatio * 6 - 0.04f;
        float leye = faceParam.LeftEyeRatio * 6 - 0.04f;
        reyeParameter.Value = reye < 0.4 ? 0.0f : reye;
        leyeParameter.Value = leye < 0.4 ? 0.0f : leye;
        Debug.Log(reyeParameter.Value);
        faceYawParameter.Value = faceParam.FaceYaw * -2.0f;
        facePitchParameter.Value = (faceParam.FacePitch - 180.0f) * 13.0f;
        faceRollParameter.Value = faceParam.FaceRoll * 1.2f;
    }
}
