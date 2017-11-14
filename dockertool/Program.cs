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
            Run().Wait();
        }

        static async Task Run()
        {
            var client = new DockerClientConfiguration(
                new Uri("npipe://./pipe/docker_engine")).CreateClient();

            var success = await client
                .Containers
                .StartContainerAsync("containername", new ContainerStartParameters());

            var response = await client.Containers.ListContainersAsync(
                new ContainersListParameters());
            var ip = response[0].NetworkSettings.Networks["nat"].IPAddress;

            using (StreamWriter w = File.AppendText(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts")))
            {
                w.WriteLine($"{ip} containername");
            }
        }

    }
}
