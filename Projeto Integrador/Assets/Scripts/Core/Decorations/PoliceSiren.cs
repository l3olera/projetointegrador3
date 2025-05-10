using UnityEngine;

public class PoliceSiren : MonoBehaviour
{
    public GameObject redLight;
    public GameObject blueLight;
    public float flashSpeed = 0.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= flashSpeed)
        {
            redLight.SetActive(!redLight.activeSelf);
            blueLight.SetActive(!blueLight.activeSelf);
            timer = 0;
        }
    }
}
