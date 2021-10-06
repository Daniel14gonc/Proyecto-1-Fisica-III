using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    public InputField ampX;
    public InputField ampY;

    private const string m1 = "Voltajes de Placas X";
    private const string m2 = "Voltajes de Placas Y";
    private const string m3 = "Amplitud X";
    private const string m4 = "Amplitud Y";

    public Toggle toggle;

    private bool active;

    public void Start()
    {
        toggle.isOn = false;
        updateCanvas();
        toggle.onValueChanged.AddListener(delegate
        {
            updateCanvas();
        });

    }

    private void Update()
    {
        
    }

    public void updateCanvas()
    {
        if (!toggle.isOn)
        {
            ElectronMovement.isSin = false;
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            ampX.placeholder.GetComponent<Text>().text = m1;
            ampY.placeholder.GetComponent<Text>().text = m2;

        }
        else
        {
            ElectronMovement.isSin = true;
            panel1.SetActive(true);
            panel2.SetActive(true);
            panel3.SetActive(true);
            ampX.placeholder.GetComponent<Text>().text = m3;
            ampY.placeholder.GetComponent<Text>().text = m4;
        }
    }
}
