using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    [SerializeField] private Transform pfConfetti;
    [SerializeField] private Color[] colorArray;

    private List<Confetti> confettiList;
    private float spawnTimer;
    private const float SPAWN_TIMER_MAX = 0.033f;

    private void Awake()
    {
        Debug.Log("CONFETTI MANAGER: Awake llamado");
        confettiList = new List<Confetti>();

        // Forzar confeti inicial
        SpawnConfetti();
    }

    // ✅ ESTO DEBE ESTAR ACTIVO
    private void Update()
    {
        foreach (Confetti confetti in new List<Confetti>(confettiList))
        {
            if (confetti.Update())
            {
                confettiList.Remove(confetti);
            }
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer += SPAWN_TIMER_MAX;
            int spawnAmount = Random.Range(1, 4);
            for (int i = 0; i < spawnAmount; i++)
            {
                SpawnConfetti();
            }
        }
    }

    private void SpawnConfetti()
    {
        float width = GetComponent<RectTransform>().rect.width;
        float height = GetComponent<RectTransform>().rect.height;
        Vector2 anchoredPosition = new Vector2(Random.Range(-width / 2f, width / 2f), height / 2f);

        Color color = colorArray.Length > 0 ? colorArray[Random.Range(0, colorArray.Length)] : Color.white;

        Confetti confetti = new Confetti(pfConfetti, transform, anchoredPosition, color, -height / 2f);
        confettiList.Add(confetti);
    }

    private class Confetti
    {
        private Transform transform;
        private RectTransform rectTransform;
        private Vector2 anchoredPosition;
        private Vector3 euler;
        private float eulerSpeed;
        private Vector2 moveAmount;
        private float minimumY;

        public Confetti(Transform prefab, Transform container, Vector2 anchoredPosition, Color color, float minimumY)
        {
            this.anchoredPosition = anchoredPosition;
            this.minimumY = minimumY;

            transform = GameObject.Instantiate(prefab, container);
            rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta *= Random.Range(0.8f, 1.2f);

            euler = new Vector3(0, 0, Random.Range(0, 360f));
            eulerSpeed = Random.Range(100f, 200f);
            eulerSpeed *= Random.Range(0, 2) == 0 ? 1f : -1f;
            transform.localEulerAngles = euler;

            moveAmount = new Vector2(0, Random.Range(-50f, -150f));

            Image image = transform.GetComponent<Image>();
            if (image == null)
            {
                Debug.LogError("CONFETTI: No se encontró componente Image en el prefab!");
            }
            else
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>("Confetti");
                if (sprites.Length > 0)
                {
                    Sprite selectedSprite = sprites[Random.Range(0, sprites.Length)];
                    image.sprite = selectedSprite;
                    Debug.Log("CONFETTI: Sprite asignado => " + selectedSprite.name);
                }
                else
                {
                    Debug.LogError("CONFETTI: No se encontraron sprites en Resources/Confetti");
                }

                image.color = color;
                //image.color = Color.white;
            }
        }

        public bool Update()
        {
            anchoredPosition += moveAmount * Time.deltaTime;
            rectTransform.anchoredPosition = anchoredPosition;

            euler.z += eulerSpeed * Time.deltaTime;
            transform.localEulerAngles = euler;

            if (anchoredPosition.y < minimumY)
            {
                GameObject.Destroy(transform.gameObject);
                return true;
            }

            return false;
        }
    }
}