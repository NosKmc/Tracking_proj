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
        parameter.Value = value;*/
        var mouthParameter = _model.Parameters[18];
        mouthParameter.Value = faceParam.MouthRatio * 2;
        var eyeParameter = _model.Parameters[5];
        eyeParameter.Value = faceParam.RightEyeRatio * 6 - 0.03f;
        Debug.Log(faceParam.RightEyeRatio);
    }
}
