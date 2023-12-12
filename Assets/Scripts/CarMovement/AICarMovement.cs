using UnityEngine;
using UnityEngine.Splines;

public class AICarMovement : MonoBehaviour
{
    private SplineAnimate _splineAnimate;

    private void Awake()
    {
        _splineAnimate = GetComponent<SplineAnimate>();
    }

    // If player or AI car stays in trigger box, this AI car will stop
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("AI"))
            return;

        PauseAnimation();
    }

    // If player or AI car exits trigger box, this AI car will start
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("AI"))
            return;

        PlayAnimation();
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
