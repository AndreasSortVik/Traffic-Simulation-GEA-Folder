using UnityEngine;
using UnityEngine.Splines;

public class AICarMovement : MonoBehaviour
{
    private SplineAnimate _splineAnimate;

    private void Awake()
    {
        _splineAnimate = GetComponent<SplineAnimate>();
    }

    public void PauseAnimation()
    {
        _splineAnimate.Pause();
    }

    public void PlayAnimation()
    {
        _splineAnimate.Play();
    }
}
