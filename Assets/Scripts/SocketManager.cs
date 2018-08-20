using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Threading;
using System.Text;

public class SocketManager : EventManager {

    private Thread SocketThread;
    private Socket listener;
    private static string localhost = "127.0.0.1";
    private static int port = 9838;
    private volatile bool keepReading = false;

    public override void Setup()
    {
        Application.runInBackground = true;
        SocketThread = new Thread(StartSocket);
        SocketThread.IsBackground = true;
        SocketThread.Start();
    }
    
    private void StartSocket()
    {
        byte[] bytes = new byte[1024];

        /** Example of sending data: 48#{"_type":"project_opened","framework":"vuforia"}
         *  headerData indicates the length of the bodyData, so it will be: 48
         *  bodyData will be: {"_type":"project_opened","framework":"vuforia"}
         *  expectingData is used in case of message framing.
        **/
        string data = "", bodyData;
        bool expectingData = false;
        int headerData, separatorIndex;
        IPAddress[] ipArray = Dns.GetHostAddresses(localhost);
        IPEndPoint localEndPoint = new IPEndPoint(ipArray[0], port);
        listener = new Socket(ipArray[0].AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            listener.Connect(localEndPoint);
            Debug.Log("<color=green>SOCKET CONNECTED</color>");
            Send(new ViewerEvent.ViewerReady(ViewerEvent.VIEWER_READY));
            keepReading = true;

            while (keepReading)
            {
                int bytesRec = listener.Receive(bytes);
                if (bytesRec <= 0)
                {
                    keepReading = false;
                    Debug.Log("<color=yellow>SOCKET DISCONECTED</color>");
                    Stop();
                    break;
                }
                
                if (expectingData)
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                else
                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                while (true)
                {
                    separatorIndex = data.IndexOf('#');
                    headerData = Int32.Parse(data.Substring(0, separatorIndex));
                    
                    bodyData = data.Substring(separatorIndex + 1);

                    if (bodyData.Length == headerData)
                    {
                        Receive(bodyData);
                        expectingData = false;
                        break;
                    }
                    else if(bodyData.Length < headerData)
                    {
                        expectingData = true;
                        break;
                    }
                    else
                    {
                        bodyData = data.Substring(separatorIndex + 1, headerData);
                        data = data.Substring((separatorIndex + 1) + headerData);
                        Receive(bodyData);
                    }

                }

                //if (data.IndexOf("<EOF>") > -1)
                //{
                //    break;
                //}

                Thread.Sleep(1);
            }
        }
        catch (SocketException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public override void Send(ViewerEvent viewerEvent)
    {
        string response = JsonUtility.ToJson(viewerEvent);
        byte[] msg = Encoding.ASCII.GetBytes(response.Length + "#" + response);
        Debug.Log("> SENDING > " + JsonUtility.ToJson(viewerEvent));
        int bytesSent = listener.Send(msg);
    }

    public override void Stop()
    {
        if (SocketThread != null)
        {
            SocketThread.Abort();
        }

        try
        {
            keepReading = false;
            listener.Shutdown(SocketShutdown.Both);
            listener.Close();
            Debug.Log("<color=yellow>SOCKET DISCONNECTED</color>");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}
