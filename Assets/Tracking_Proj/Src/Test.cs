using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;

public class Test : MonoBehaviour
{/*
    [SerializeField] private int m_WebCamIndex = 0;
    private Texture2D m_Texture;
    private VideoCapture m_Video;
    private Mat m_Frame;
    // Start is called before the first frame update
    void Start()
    {
        // m_WebCamIndexによって指定されたウェブカメラを初期化
        m_Video = new VideoCapture(m_WebCamIndex);

        // Texture2Dをウェブカメラのサイズ、フォーマットで初期化
        m_Texture = new Texture2D(
            m_Video.FrameWidth, // ウェブカメラのwidth
            m_Video.FrameHeight, // ウェブカメラのheight
            TextureFormat.RGB24, // フォーマットを指定
            false // ミップマップ設定（よくわかっていない）
        );

        // テクスチャを適用したいオブジェクトにスクリプトを登録する
        // 以下どちらか
        // 1. スクリプトをuGUIのRawImageに登録している場合
        GetComponent<RawImage>().texture = m_Texture;
        // 2. スクリプトをオブジェクトに登録している場合
        //GetComponent<Renderer>().material.mainTexture = m_Texture;
    }

    // Update is called once per frame
    void Update()
    {
        // ウェブカメラから画像読み込み
        m_Video.Read(m_Frame);
        // MatオブジェクトをPNGデータに変換、読み込み
        m_Texture.LoadImage(m_Frame.ImEncode());
    }
    private void OnDestroy()
    {
        // カメラを閉じる処理を書かないと起動し続ける
        // .NETのバージョンをイジってないと動かないかもしれません
        m_Video?.Dispose();
        m_Video = null;
    }*/
}
