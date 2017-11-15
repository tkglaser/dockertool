using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dockertool
{
    public class Container
    {
        public string Name { get; }

        private DockerClient client;

        public Container(string name)
        {
            this.Name = name;
            this.client = new DockerClientConfiguration(
                new Uri("npipe://./pipe/docker_engine")).CreateClient();
        }

        public bool IsRunning
        {
            get
            {
                Console.WriteLine($"Checking if container [{Name}] is running...");
                var running = Info() != null;
                if (running)
                {
                    Console.WriteLine($"Container [{Name}] is running.");
                }
                else
                {
                    Console.WriteLine($"Container [{Name}] is NOT running.");
                }
                return running;
            }
        }

        public string Ip
        {
            get
            {
                if (!IsRunning)
                {
                    Start();
                }

                var ip = Info().NetworkSettings.Networks["nat"].IPAddress;
                Console.WriteLine($"IP of container [{Name}] is [{ip}]");
                return ip;
            }
        }

        public bool Start()
        {
            Console.WriteLine($"Starting container [{Name}]...");
            var startTask = client.Containers
                .StartContainerAsync(this.Name, new ContainerStartParameters());
            startTask.Wait();
            Console.WriteLine($"Container [{Name}] started.");
            return startTask.Result;
        }

        private ContainerListResponse Info()
        {
            var queryTask = client.Containers.ListContainersAsync(
                new ContainersListParameters());

            queryTask.Wait();

            return queryTask.Result.FirstOrDefault(r => r.Names.Contains("/" + this.Name));
        }
    }
}
