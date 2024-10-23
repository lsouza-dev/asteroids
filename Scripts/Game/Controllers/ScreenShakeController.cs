using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    [SerializeField] public Transform camTransform;
    [SerializeField] public float shakeDuration = .2f;
    [SerializeField] public float shakeAmount = 0.2f;
    [SerializeField] public float decreaseFactor = 1.0f;
    public bool shakeActive = false;

    Vector3 originalPos;

    private void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (shakeActive)
        {
            ScreenShake();

        }

    }


    private void ScreenShake()
    {

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            camTransform.localPosition = originalPos;

            shakeDuration = .1f;
            shakeAmount = .1f;
            decreaseFactor = 1f;

            shakeActive = false;
        }
    }

}
