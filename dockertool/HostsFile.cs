using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dockertool
{
    public class HostsFile
    {
        public string Filename => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers/etc/hosts");

        private List<string> Content;

        private Dictionary<string, string> ContainerIps = new Dictionary<string, string>();

        private readonly string Separator = "##### DOCKERTOOL #####";

        public HostsFile()
        {
            using (var stream = File.OpenText(Filename))
            {
                Content = stream.ReadToEnd().Split(
                    new string[] { Environment.NewLine }, 
                    StringSplitOptions.None).ToList();
            }
            ParseEntries();
        }

        public void SetIp(string name, string ip)
        {
            ContainerIps[name] = ip;
        }

        public void Save()
        {
            Console.WriteLine($"Saving to HOSTS...");
            var idx = Content.IndexOf(Separator);
            Content = Content.GetRange(0, idx);
            AddHeader();
            foreach(var entry in ContainerIps)
            {
                Content.Add($"{entry.Value} {entry.Key}");
            }
            Content.Add("");
            File.WriteAllText(Filename, string.Join(Environment.NewLine, Content));
            Console.WriteLine($"Saved.");
        }

        private void AddHeader()
        {
            Content.Add(Separator);
            Content.Add("# The lines below are managed by DockerTool. All your edits may be overridden!");
        }

        private void ParseEntries()
        {
            var idx = Content.IndexOf(Separator);
            if (idx == -1)
            {
                AddHeader();
                idx = Content.IndexOf(Separator);
            };

            var ourLines = Content.GetRange(idx, Content.Count - idx);
            foreach(var line in ourLines)
            {
                if (line.StartsWith("#") || string.IsNullOrEmpty(line))
                {
                    continue;
                }
                var tokens = line.Split(' ').Select(s => s.Trim()).ToArray();
                ContainerIps[tokens[1]] = tokens[0];
            }
        }
    }
}
