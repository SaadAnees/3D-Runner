using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using QRCodeReaderAndGenerator;

public class QRCodeReaderExample : MonoBehaviour {

	[SerializeField]
	RawImage rawImage;

	[SerializeField]
	Text txtResult;

    public GameObject frame;
    public Text debugTxt;

	// Use this for initialization

	IEnumerator Start()
	{
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        ScanQRCode();
	}


	void OnEnable () {
		QRCodeManager.onError += HandleOnError;
		QRCodeManager.onQrCodeFound += HandleOnQRCodeFound;
	}

	void OnDisable () {
		QRCodeManager.onError -= HandleOnError;
		QRCodeManager.onQrCodeFound -= HandleOnQRCodeFound;
	}

	void HandleOnQRCodeFound (ZXing.BarcodeFormat barCodeType, string barCodeValue)
	{
        //Debug.Log (barCodeType + " __ " + barCodeValue);
        //txtResult.text = "A total of " + barCodeValue + " PTS has been added to your total score";
        AlertError("A total of " + barCodeValue + " PTS has been added to your total score");
	}

	void HandleOnError (string err)
	{
		Debug.LogError (err);
	}
		
	public void ScanQRCode()
	{
		if(rawImage)
		{
			QRCodeManager.CameraSettings camSettings = new QRCodeManager.CameraSettings ();
			string rearCamName = GetRearCamName ();
			if (rearCamName != null) {
				camSettings.deviceName = rearCamName;
				camSettings.maintainAspectRatio = true;
				camSettings.makeSquare = true;
				camSettings.requestedWidth = 1280;
				camSettings.requestedHeight = 720;
				camSettings.scanType = ScanType.CONTINUOUS;
				QRCodeManager.Instance.ScanQRCode (camSettings, rawImage, 1f);
			}
		}
	}

	// this function is require to call to stop scanning when camSettings.scanType = ScanType.CONTINUOUS;
	// no need to call when camSettings.scanType = ScanType.ONCE;
	public void StopScanning()
	{
		QRCodeManager.Instance.StopScanning ();
	}

	string GetRearCamName()
	{
		foreach (WebCamDevice device in WebCamTexture.devices) {
            //if (!device.isFrontFacing) {
            print(device.name);
				return device.name;
			//}
		}
		return null;
	}

	// scene loading
	public void OnPayloadGeneratorClick()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}


    /// <summary>
	/// Displays an error message
	/// </summary>
	public void AlertError(string errorText)
    {
        frame.SetActive(true);
        debugTxt.gameObject.SetActive(true);
        debugTxt.text = errorText;
        StartCoroutine(DisableErrorText());
    }


    /// <summary>
    /// Disables the error message text
    /// </summary>
    IEnumerator DisableErrorText()
    {
        yield return new WaitForSeconds(5.0f);
        frame.SetActive(false);
        debugTxt.text = "";
        debugTxt.gameObject.SetActive(false);

    }

}
