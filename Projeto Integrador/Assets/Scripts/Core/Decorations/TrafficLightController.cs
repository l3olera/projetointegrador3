using System.Collections;
using UnityEngine;
public enum TrafficLightState { Red, Yellow, Green }

public class TrafficLightController : MonoBehaviour
{
    public GameObject redLight, yellowLight, greenLight;
    public float redTime = 5f, yellowTime = 2f, greenTime = 5f;
    public TrafficLightState currentState = TrafficLightState.Green;

    void Start()
    {
        StartCoroutine(StateMachine());
    }

    IEnumerator StateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case TrafficLightState.Red:
                    SetLights(true, false, false);
                    yield return new WaitForSeconds(redTime);
                    currentState = TrafficLightState.Green;
                    break;

                case TrafficLightState.Green:
                    SetLights(false, false, true);
                    yield return new WaitForSeconds(greenTime);
                    currentState = TrafficLightState.Yellow;
                    break;

                case TrafficLightState.Yellow:
                    SetLights(false, true, false);
                    yield return new WaitForSeconds(yellowTime);
                    currentState = TrafficLightState.Red;
                    break;
            }
        }
    }

    void SetLights(bool red, bool yellow, bool green)
    {
        redLight.SetActive(red);
        yellowLight.SetActive(yellow);
        greenLight.SetActive(green);
    }
}
