using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace XatServer
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hola, sóc el servidor!");
			
			Server servidor = new Server("192.168.130.75", 6969);
			
			if (!servidor.Start())
			{
				Console.WriteLine("No puc engegar el servidor!");
			}
			
			if (servidor.WaitForAClient())
			{
				// Escribim tot el que ens envii el client
				while (true)
				{
                    try
                    {
                        Console.WriteLine(servidor.ReadLine() + " diu: " + servidor.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("S'ha desconnectat el client");
                        servidor.WaitForAClient();
                    }
				}
				
				// server.WriteLine("Hi!"); 
			}
		}
	}
	
	public class Server
	{
		private NetworkStream netStream;
		private StreamReader readerStream;
		private StreamWriter writerStream;
		private IPEndPoint server_endpoint;
		private TcpListener listener;
		
		public Server(string ip, int port)
		{
			IPAddress address = IPAddress.Parse(ip);
			server_endpoint = new IPEndPoint(address, port);
		}
		
		public string ReadLine()
		{
			return readerStream.ReadLine();
		}
		
		public void WriteLine(string str)
		{
			writerStream.WriteLine(str);
			writerStream.Flush();
		}
		
		public bool Start()
		{
			try
			{
//				listener = new TcpListener(server_endpoint);
				listener = new TcpListener(9898);

				listener.Start(); //start server
			}
			catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
				return false;
			}
		
			Console.WriteLine("Servidor engegat, escoltant a {0}:{1}", server_endpoint.Address, server_endpoint.Port);
			
			return true;
		}
		
		public bool WaitForAClient()
		{
			// Esperem una connexio d'un client
			Socket serverSocket = listener.AcceptSocket();
			
			try
			{
				if (serverSocket.Connected)
				{
					netStream = new NetworkStream(serverSocket);
					
					writerStream = new StreamWriter(netStream);
					readerStream = new StreamReader(netStream);
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
				return false;
			}

			Console.WriteLine("Un client s'ha connectat!");
					
			return true;
		}
	}
}
