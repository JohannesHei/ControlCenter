﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BF2Statistics.Gamespy
{
    public class GpspClient : IDisposable
    {
        /// <summary>
        /// Indicates whether this object is disposed
        /// </summary>
        public bool Disposed { get; protected set; }

        /// <summary>
        /// Connection TcpClient Stream
        /// </summary>
        private TcpClientStream Stream;

        /// <summary>
        /// The Tcp Client
        /// </summary>
        private TcpClient Client;

        /// <summary>
        /// Client background thread
        /// </summary>
        private Thread ClientThread;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        public GpspClient(TcpClient client)
        {
            // Set disposed to false!
            this.Disposed = false;

            // Set the client variable
            this.Client = client;

            // Init a new client stream class
            Stream = new TcpClientStream(client);

            // Handle client communications in a background thread
            ClientThread = new Thread(new ThreadStart(Start));
            ClientThread.IsBackground = true;
            ClientThread.Start();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~GpspClient()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose method to be called by the server
        /// </summary>
        public void Dispose()
        {
            if (Client.Client.Connected)
                Client.Close();

            this.Disposed = true;
        }

        /// <summary>
        /// Starts the GPSP.gamespy.com listner for this client
        /// </summary>
        public void Start()
        {
            LoginServer.Log("[GPSP] Client Connected: {0}", Client.Client.RemoteEndPoint);

            while (Client.Client.IsConnected())
            {
                Update();
                Thread.Sleep(200);
            }

            LoginServer.Log("[GPSP] Client Disconnected: {0}", Client.Client.RemoteEndPoint);
            Dispose();
        }

        /// <summary>
        /// Main Listener loop
        /// </summary>
        public void Update()
        {
            if (Stream.HasData())
            {
                // Parse input message
                string message = Stream.Read();
                string[] recv = message.Split('\\');

                switch (recv[1])
                {
                    case "nicks":
                        SendGPSP(recv);
                        break;
                    case "check":
                        SendCheck(recv);
                        break;
                }
            }
        }

        /// <summary>
        /// This method is requested by the client whenever an accounts existance needs validated
        /// </summary>
        /// <param name="recv"></param>
        private void SendGPSP(string[] recv)
        {
            // Try to get user data from database
            Dictionary<string, object> ClientData;
            try
            {
                ClientData = LoginServer.Database.GetUser(GetParameterValue(recv, "email"), GetParameterValue(recv, "pass"));
                if (ClientData == null)
                {
                    Stream.Write("\\nr\\0\\ndone\\\\final\\");
                    return;
                }
            }
            catch 
            {
                Dispose();
                return;
            }

            Stream.Write("\\nr\\1\\nick\\{0}\\uniquenick\\{0}\\ndone\\\\final\\", ClientData["name"]);
        }

        /// <summary>
        /// This is the primary method for fetching an accounts BF2 PID
        /// </summary>
        /// <param name="recv"></param>
        private void SendCheck(string[] recv)
        {
            try {
                Stream.Write("\\cur\\0\\pid\\{0}\\final\\", LoginServer.Database.GetPID(GetParameterValue(recv, "nick")));
            }
            catch
            {
                Dispose();
                return;
            }
        }

        /// <summary>
        /// A simple method of getting the value of the passed parameter key,
        /// from the returned array of data from the client
        /// </summary>
        /// <param name="parts">The array of data from the client</param>
        /// <param name="parameter">The parameter</param>
        /// <returns>The value of the paramenter key</returns>
        private string GetParameterValue(string[] parts, string parameter)
        {
            bool next = false;
            foreach (string part in parts)
            {
                if (next)
                    return part;
                else if (part == parameter)
                    next = true;
            }
            return "";
        }
    }
}
