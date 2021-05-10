using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FPS : MonoBehaviour
{
	//FPS
	private float fps;

	//Text
	[SerializeField] Text fpsText;

    private void Start()
    {
		StartCoroutine(recalculateFPS());
	}

    private void OnEnable()
    {
        StartCoroutine(recalculateFPS());
    }

    private IEnumerator recalculateFPS()
    {
        while (true)
        {
			fps = 1 / Time.deltaTime;
			fpsText.text = "FPS " + fps.ToString("0");
			yield return new WaitForSeconds(0.5f);
        }
    }
}
