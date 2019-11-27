using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    enum Step {Started, Control_1, Setting_1, Control_2, Setting_2, Central, Connect, Connected};

    public NetworkController networkController;
    public OVRBoundaryReporter boundaryReporter;
    public Transform playerPosition;

    public Text text;

    Step currentStep = Step.Started;
    // Start is called before the first frame update
    void Start()
    {
        currentStep = Step.Started;
        StartCoroutine(NextStep());
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStep) {
            case Step.Started:
                text.text = "Iniciando...";
                break;
            case Step.Control_1:
                text.text = "Ubícate en el primer punto de control y presiona cualquier botón.";
                break;

            case Step.Setting_1:
                text.text = "Fijando punto de control 1... ";
                break;

            case Step.Control_2:
                text.text = "Ubícate en el segundo punto de control y presiona cualquier botón.";
                break;

            case Step.Setting_2:
                text.text = "Fijando punto de control 2... ";
                break;
            case Step.Central:
                text.text = "Ubicate en el punto central y presiona cualquier botón";
                break;
            case Step.Connect:
                text.text = "Gracias... Conectando";
                break;
            case Step.Connected:
                text.text = "Conexión completa";
                break;
            default:
                break;
        }

        if (OVRInput.GetDown(OVRInput.Button.Any))
        {
            Debug.Log("A button was pressed!");
            if (currentStep == Step.Control_1)
            {
                Debug.Log("First control step set");
                boundaryReporter.boundaryMaxLimit = playerPosition.localPosition;
                currentStep = Step.Setting_1;
                StartCoroutine(NextStep());
            }

            if (currentStep == Step.Control_2)
            {
                Debug.Log("Second control step set");
                boundaryReporter.boundaryMinLimit = playerPosition.localPosition;
                currentStep = Step.Setting_2;
                StartCoroutine(NextStep());
            }

            if (currentStep == Step.Central)
            {
                Debug.Log("Central point step set");
                currentStep = Step.Connect;
                networkController.startConnection();
                StartCoroutine(NextStep());
            }
        }
        
    }

    IEnumerator NextStep()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        switch (currentStep)
        {
            case Step.Started:
                currentStep = Step.Control_1;
                break;
            case Step.Setting_1:
                currentStep = Step.Control_2;
                break;
            case Step.Setting_2:
                currentStep = Step.Central;
                break;
            case Step.Connect:
                currentStep = Step.Connected;
                break;
            default:
                break;
        }
    }
}
