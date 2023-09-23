using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
	public enum Fade
	{
		Up,
		Down,
		Stop
	}

	protected Fade fade = Fade.Stop;

	[SerializeField] protected float duration = .4f;
    protected float startValue;
    protected float endValue;
	protected float currentFillAmount;

	[SerializeField] protected Image image;
	[SerializeField] protected Image shadowImage;

	private void Awake()
	{
		image = GetComponent<Image>();
		image.fillAmount = 1f;
		shadowImage.fillAmount = image.fillAmount;
		currentFillAmount = image.fillAmount;
		endValue = currentFillAmount;
	}

	private void Start()
    {
		if (shadowImage == null)
			shadowImage = image;
	}

	private void Update()
    {
        if (fade != Fade.Stop)
			AnimateBar();
    }

	public void SetTargetValue(float target)
	{
		if (target > currentFillAmount)
		{
			shadowImage.fillAmount = endValue = target;

			if (fade == Fade.Down)
				currentFillAmount = image.fillAmount;

			fade = Fade.Up;
		}
		else if (target < currentFillAmount)
		{
			image.fillAmount = endValue = target;

			if (fade == Fade.Up)
				shadowImage.fillAmount = currentFillAmount;

			fade = Fade.Down;
		}
	}

	protected void AnimateBar()
	{
		switch (fade)
		{
			case Fade.Up:
				currentFillAmount = Mathf.Clamp(currentFillAmount + (Time.deltaTime * duration), currentFillAmount, endValue);
				image.fillAmount = currentFillAmount;
				fade = currentFillAmount == endValue ? Fade.Stop : Fade.Up;
				break;
			case Fade.Down:
				currentFillAmount = Mathf.Clamp(currentFillAmount - (Time.deltaTime * duration), endValue, currentFillAmount);
				shadowImage.fillAmount = currentFillAmount;
				fade = currentFillAmount == endValue ? Fade.Stop : Fade.Down;
				break;
		}


	}
}
