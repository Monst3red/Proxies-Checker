using System;
using System.IO;
using System.Threading.Tasks;
using Leaf.xNet;

namespace Powerfull_Proxies_Checker
{
    class Program
    {
        private static string type = "";
        static void Main(string[] args)
        {
            Console.Title = "Powerfull Proxies Checker | Made by Monsτεгεd#1337";
            Menu();
            Console.Write("Proxies choice (http, socks4, socks5): ");
            string proxie = Console.ReadLine();
            Console.Clear();

            Menu();
            Console.Write("How many thread do u want? ");
            string threads = Console.ReadLine();
            type += proxie;
            Console.Clear();
            
            Menu();
            Parallel.ForEach(File.ReadAllLines("Proxies.txt"), new ParallelOptions() { 
                MaxDegreeOfParallelism = Convert.ToInt32(threads) }, proxy =>
            {
                try
                {
                    using (var request = new HttpRequest())
                    {
                        request.ConnectTimeout = 5 * 1000;

                        switch (type)
                        {
                            case "http":
                                request.Proxy = ProxyClient.Parse(ProxyType.HTTP, proxy);
                                break;
                            case "socks4":
                                request.Proxy = ProxyClient.Parse(ProxyType.Socks4, proxy);
                                break;
                            case "socks5":
                                request.Proxy = ProxyClient.Parse(ProxyType.Socks5, proxy);
                                break;

                            default:
                                request.Proxy = null;
                                break;
                        }

                        request.Proxy.ConnectTimeout = 2 * 1000;

                        request.Get("http://azenv.net/");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[GOOD] {proxy}");

                        if (!File.Exists("Hits.txt"))
                            File.Create("Hitx.txt");

                        File.AppendAllText("Hits.txt", proxy + Environment.NewLine);
                    }
                }
                catch { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"[DEAD] {proxy}"); }
            });
        }

        private static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("    ____                  _              ________              __            ");
            Console.WriteLine("   / __ \\_________  _  __(_)__  _____   / ____/ /_  ___  _____/ /_____  _____");
            Console.WriteLine("  / /_/ / ___/ __ \\| |/_/ / _ \\/ ___/  / /   / __ \\/ _ \\/ ___/ //_/ _ \\/ ___/");
            Console.WriteLine(" / ____/ /  / /_/ />  </ /  __(__  )  / /___/ / / /  __/ /__/ ,< /  __/ /    ");
            Console.WriteLine("/_/   /_/   \\____/_/|_/_/\\___/____/   \\____/_/ /_/\\___/\\___/_/|_|\\___/_/     ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
