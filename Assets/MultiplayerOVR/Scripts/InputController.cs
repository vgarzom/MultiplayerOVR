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

        text.text = "Iniciando...";
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.Any))
        {
            Debug.Log("A button was pressed!");
            if (currentStep == Step.Control_1)
            {
                Debug.Log("First control step set");
                boundaryReporter.boundaryMaxLimit = playerPosition.localPosition;
                currentStep = Step.Setting_1;
                text.text = "Fijando punto de control 1... ";
                StartCoroutine(NextStep());
            }

            if (currentStep == Step.Control_2)
            {
                Debug.Log("Second control step set");
                boundaryReporter.boundaryMinLimit = playerPosition.localPosition;
                currentStep = Step.Setting_2;
                text.text = "Fijando punto de control 2... ";
                StartCoroutine(NextStep());
            }

            if (currentStep == Step.Central)
            {
                Debug.Log("Central point step set");
                currentStep = Step.Connect;
                text.text = "Gracias... Conectando";
                networkController.startConnection();
                //StartCoroutine(NextStep());
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
                text.text = "Ubícate en el primer punto de control y presiona cualquier botón.";
                break;
            case Step.Setting_1:
                currentStep = Step.Control_2;
                text.text = "Ubícate en el segundo punto de control y presiona cualquier botón.";
                break;
            case Step.Setting_2:
                currentStep = Step.Central;
                text.text = "Ubicate en el punto central y presiona cualquier botón";
                break;
            case Step.Connect:
                currentStep = Step.Connected;
                break;
            default:
                break;
        }
    }
}
