using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EManager : MonoBehaviour
{
    public GameObject start;
    public GameObject electron;

    public Dropdown functionx;
    public Dropdown functiony;


    public InputField frecuencyx;
    public InputField frecuencyy;
    public InputField xphase;
    public InputField yphase;
    public InputField EVoltaje;
    public InputField amplitudex;
    public InputField apmlitudey; 
    public InputField latency;


    private int cont = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Vector3 startPos = start.transform.position;
        Instantiate(electron, startPos, Quaternion.identity);
        */

    }

    // Update is called once per frame
    void Update()
    {
        amplitudex.placeholder.GetComponent<Text>().text = "Voltaje (amplitud) x +/-" + Math.Round(ElectronMovement.maxVoltageX(), 2) + "V";
        apmlitudey.placeholder.GetComponent<Text>().text = "Voltaje (amplitud) y +/-" + Math.Round(ElectronMovement.maxVoltageY(), 2) + "V";

        if (cont%25==0)
        {
            Vector3 startPos = start.transform.position;
            Instantiate(electron, startPos, Quaternion.identity);
        }

        cont++;
        
        
    }

    public void updateInformation()
    {
        try
        {
            ElectronMovement.frecuencyx = (string.IsNullOrEmpty(frecuencyx.text)) ? ElectronMovement.frecuencyx : (float)Convert.ToDouble(frecuencyx.text);
            ElectronMovement.frecuencyy = (string.IsNullOrEmpty(frecuencyy.text)) ? ElectronMovement.frecuencyy : (float)Convert.ToDouble(frecuencyy.text);
            ElectronMovement.xphase = (string.IsNullOrEmpty(xphase.text)) ? ElectronMovement.xphase : (float)Convert.ToDouble(xphase.text);
            ElectronMovement.yphase = (string.IsNullOrEmpty(yphase.text)) ? ElectronMovement.yphase : (float)Convert.ToDouble(yphase.text);
            ElectronMovement.latency = (string.IsNullOrEmpty(latency.text)) ? ElectronMovement.yphase : (float)Convert.ToDouble(latency.text);


            if (!string.IsNullOrEmpty(EVoltaje.text))
            {
                float voltage = (float)Convert.ToDouble(EVoltaje.text);
                if (voltage < 80e3 && voltage >= 1000)
                    ElectronMovement.EVoltaje = voltage;
            }

            if (!string.IsNullOrEmpty(amplitudex.text))
            {
                float ampx = (float)Convert.ToDouble(amplitudex.text);
                if (!(Mathf.Abs(ampx) > ElectronMovement.maxVoltageX()))
                    ElectronMovement.amplitudex = ampx;
            }

            if (!string.IsNullOrEmpty(apmlitudey.text))
            {
                float ampy = (float)Convert.ToDouble(apmlitudey.text);
                if (!(Mathf.Abs(ampy) > ElectronMovement.maxVoltageX()))
                    ElectronMovement.apmlitudey = ampy;
            }

            frecuencyx.text = "";
            frecuencyy.text = "";
            xphase.text = "";
            yphase.text = "";
            EVoltaje.text = "";
            amplitudex.text = "";
            apmlitudey.text = "";


            if (functionx.value.Equals("Seno"))
            {
                ElectronMovement.senx = true;
            }
            else
            {
                ElectronMovement.senx = false;
            }

            if (functiony.value.Equals("Seno"))
            {
                ElectronMovement.seny = true;
            }
            else
            {
                ElectronMovement.seny = false;
            }
        }
        catch (Exception e)
        {
            print(e.Message);
        }
    }
}
