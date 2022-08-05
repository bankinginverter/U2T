using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;

public class QRCodeGen : MonoBehaviour
{
    [SerializeField]
    private RawImage _finalImageQRcode;


    private Texture2D _encoderTexture;

    private void Start()
    {
        _encoderTexture = new Texture2D(256, 256);
    }

    private Color32 [] Encode(string textForEncoding, int width, int height)
    {
        BarcodeWriter write = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };

        return write.Write(textForEncoding);
    }

    public void EncodeTextToQRcode()
    {
        Color32[] _convertPixelTotexture = Encode("https://www.facebook.com/", _encoderTexture.width, _encoderTexture.height);
        _encoderTexture.SetPixels32(_convertPixelTotexture);
        _encoderTexture.Apply();
        _finalImageQRcode.texture = _encoderTexture;
    }

}
