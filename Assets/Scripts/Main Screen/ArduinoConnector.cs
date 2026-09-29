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
            while (serial.BytesToRead > 0) //if there is something present
            {
                string data = serial.ReadLine().Trim(); //cuts off whitespace and new charaacters

                if (data.Length == 0) //if nothing is present, continue to next loop
                    continue;

                // these next if statements check what is being sent [check the uno script for more details]. here are the following mappings:
                // 1 = potentiometer 1, 2 = potentiometer 2, 3 = button 1, 4 = button 2

                if (data[0] == '1')
                {
                    string valueString = data.Substring(2);

                    if (int.TryParse(valueString, out int value1))
                    {
                        P1zPos = (value1 / 1023f) * 8 - 4f; // turns the range of 0-1023 into -4 to 4, which is the range of the z axis in the game
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
                                button1PressedOnce = true; // when button is held, it will keep sending 1 signals, ti prevent the game recognising the game being pressed multiple times really quickly, this little statement checks that the button has been hled down once and only stores a pressedDownOnce variable to tryue, only becoming false when button is released. basically, no matter how long i hold, the variable will allow other scripts to see that ive pressed it just once
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
            // if port times out, ignores and continues, so game can contine
        }
    }

    void OnDisable() //closes serial port when game is closed, so can be opened if restarted, how kind
    {
        if (serial != null && serial.IsOpen){
            serial.Close();
        }
    }

    void OnApplicationQuit() // same thing, if the game is quitted, closes the port so it can be opened again if restarted
    {
        if (serial != null && serial.IsOpen){
            serial.Close();
        }
    }
}