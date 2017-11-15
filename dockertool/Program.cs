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
            Run();
        }

        static void Run()
        {
            var containerName = "pauldb";
            var container = new Container(containerName);
            var ip = container.Ip;

            using (StreamWriter w = File.AppendText(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts")))
            {
                w.WriteLine($"{ip} ${containerName}");
            }
        }

    }
}
