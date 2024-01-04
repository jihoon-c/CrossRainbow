using UnityEngine;

public class TimeScaleButton : MonoBehaviour
{
    private bool isButtonPressed = false;
    private float originalTimeScale = 1f;

    public void OnButtonPressed()
    {
        isButtonPressed = !isButtonPressed;

        if (isButtonPressed)
        {
            originalTimeScale = Time.timeScale;
            Time.timeScale = 5f;
        }
        else
        {
            Time.timeScale = originalTimeScale;
        }
    }
}
