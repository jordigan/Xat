using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace XatClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Client client = new Client("192.168.130.75", 6969);
			
			if (client.ConnectToServer())
			{
                bool desconectar = false;
				while (true && desconectar == false)
				{
                    try
                    {
                        string cadena = Console.ReadLine();
                        if (cadena == "disconnect")
                            desconectar = true;
                        client.WriteLine("jordigc.1988@gmail.com");
                        client.WriteLine(cadena);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("S'ha desconnectat el servidor");
                        Console.ReadKey(true);
                    }
					
				}
			}
		}
	}
	
	public class Client
	{
		private NetworkStream netStream;
		private StreamReader readerStream;
		private StreamWriter writerStream;
		private IPEndPoint server_endpoint;
		private TcpClient tcpClient;
		
		public Client(string ip, int port)
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
		
		public bool ConnectToServer()
		{
			try
			{
				//tcpClient = new TcpClient(server_endpoint);
				tcpClient = new TcpClient("192.168.130.81", 9898);
				
				netStream = tcpClient.GetStream();
				readerStream = new StreamReader(netStream);
				writerStream = new StreamWriter(netStream);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
				Console.WriteLine(e.Message);
				return false;
			}

			Console.WriteLine("M'he connectat amb el servidor");

			return true;		
		}
	}
}
