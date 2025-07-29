using UnityEngine;

public class UI_Menu_TitleIntro : MonoBehaviour
{
    public Vector3 finalPosition;
    public float duration = 1.2f;
    public AnimationCurve curve;
    public AudioSource audioSource;
    public AudioClip arrivalSound;

    private Vector3 startPosition;
    private float timer = 0f;
    private bool animating = true;

    void Start()
    {
        //para mover el titulo de arriba a bajo cuando ya guardo la posicion final
        finalPosition = transform.localPosition;
        startPosition = finalPosition + new Vector3(0f, 600f, 0f);  //para que venga de arriba
        transform.localPosition = startPosition;
    }

    void Update()
    {
        if (!animating) return;

        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / duration);
        float curveValue = curve.Evaluate(t);

        transform.localPosition = Vector3.LerpUnclamped(startPosition, finalPosition, curveValue);

        if (t >= 1f)
        {
            animating = false;

            //sonido cuando cae...pum talve
            if (audioSource != null && arrivalSound != null)
                audioSource.PlayOneShot(arrivalSound);
        }
    }

}
