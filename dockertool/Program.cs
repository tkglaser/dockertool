using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dockertool
{
    class Program
    {
        static void Main(string[] args)
        { 
            if (args.Length != 1)
            {
                Console.WriteLine("Pleas pass the name of the container as argument");
            }
            else
            {
                Run(args[0]);
            }
        }

        static void Run(string name)
        {
            var container = new Container(name);
            var ip = container.Ip;

            var hostsFile = new HostsFile();
            hostsFile.SetIp(name, ip);
            hostsFile.Save();
        }
    }
}
