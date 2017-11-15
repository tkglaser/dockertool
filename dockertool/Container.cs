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
                return Info() != null;
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

                return Info().NetworkSettings.Networks["nat"].IPAddress;
            }
        }

        public bool Start()
        {
            var startTask = client.Containers
                .StartContainerAsync(this.Name, new ContainerStartParameters());
            startTask.Wait();
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
