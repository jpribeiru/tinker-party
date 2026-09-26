using UnityEngine;
using System.IO.Ports;

public class ArduinoConnector : MonoBehaviour
{
    public static ArduinoConnector Instance; // creates an instance variable that stores this gameobject, prevents duplicates forming

    SerialPort serial = new SerialPort("/dev/cu.usbmodem14101", 115200); // write port there
    public static float P1zPos = 0f; // public variables to send out the readings of the potentiometer
    public static float P2zPos = 0f;

    public static bool button1Pressed = false; // public variable to tell if button is being held down
    public static bool button2Pressed = false;

    public static bool button1PressedOnce = false; //public variable totell button is pressed just once
    public static bool button2PressedOnce = false;

    private static bool previousButton1State = false; //private variable used to check if button is pressed once, since when button is held down, lots of readings per second so multiple signals sent at once even when shortly pressed, so this prevents that
    private static bool previousButton2State = false;

    void Awake()
    {
        if (Instance == null) // prevents duplicate, deletes if another instance of this script is found
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // prevents this form deleting when a new scene is loaded
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void refreshPort() // function that allows updates to the port being read
    {
        PortHandler.portLoc = PortHandler.portLoc.Replace("\u200B", "").Trim(); // retreive variable from porthandler

        Debug.Log("Opening port: [" + PortHandler.portLoc + "]");

        if (serial != null && serial.IsOpen) // closes the current port beign listened/strying to listen to
        {
            serial.Close();
        }

        serial = new SerialPort(PortHandler.portLoc, 115200); // sets the new port
        serial.ReadTimeout = 100;
        serial.Open(); // opens port
        serial.DiscardInBuffer(); // prevents buffer from fast readings/lag

        Debug.Log("Refreshed!");
    }

    void Start() // opens port at start
    {
        serial.ReadTimeout = 100;
        serial.Open();
        serial.DiscardInBuffer();
    }

    void Update() // main loop
    {
        button1PressedOnce = false; //sets buttonpressed to false
        button2PressedOnce = false;

        if (serial == null || !serial.IsOpen) // if no serial prot is opened, it keep returning, stops the rest of the code so no error occurs with serial trying to be accessed
            return;

        try
        {
            while (serial.BytesToRead > 0)
            {
                string data = serial.ReadLine().Trim();

                if (data.Length == 0)
                    continue;

                if (data[0] == '1')
                {
                    string valueString = data.Substring(2);

                    if (int.TryParse(valueString, out int value1))
                    {
                        P1zPos = (value1 / 1023f) * 8 - 4f;
                    }
                }
                else if (data[0] == '2')
                {
                    string valueString = data.Substring(2);

                    if (int.TryParse(valueString, out int value2))
                    {
                        P2zPos = (value2 / 1023f) * 8 - 4f;
                    }
                }
                else if (data[0] == '3')
                {
                    string valueString = data.Substring(2);

                    if (int.TryParse(valueString, out int value3))
                    {
                        if (value3 == 1)
                        {
                            button1Pressed = true;

                            if (!previousButton1State)
                            {
                                button1PressedOnce = true;
                                Debug.Log("Button 1 pressed!");
                            }

                            previousButton1State = true;
                        }
                        else
                        {
                            button1Pressed = false;
                            previousButton1State = false;
                        }
                    }
                }
                else if (data[0] == '4')
                {
                    string valueString = data.Substring(2);

                    if (int.TryParse(valueString, out int value4))
                    {
                        if (value4 == 1)
                        {
                            button2Pressed = true;

                            if (!previousButton2State)
                            {
                                button2PressedOnce = true;
                                Debug.Log("Button 2 pressed!");
                            }

                            previousButton2State = true;
                        }
                        else
                        {
                            button2Pressed = false;
                            previousButton2State = false;
                        }
                    }
                }
            }
        }
        catch (System.TimeoutException)
        {
            // normal, ignore
        }
    }

    void OnDisable()
    {
        if (serial != null && serial.IsOpen){
            serial.Close();
        }
    }

    void OnApplicationQuit()
    {
        if (serial != null && serial.IsOpen){
            serial.Close();
        }
    }
}