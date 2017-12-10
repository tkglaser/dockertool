# dockertool
This tool gets the IP address of a running container and then adds it to the Windows HOSTS file. This is a workaround for the [Windows NAT interface bug](https://blog.sixeyed.com/published-ports-on-windows-containers-dont-do-loopback/).

**Note:** This tool works with native Windows Docker Containers only, not MobiLinux. Your Docker must be in "Windows Containers" mode. You need Windows 10 Anniversary Edition or higher for this.

If the container is not running, the tool will attempt to start it. The tool will ask for elevated User Permissions because it needs to edit the HOSTS file which is protected.

Usage:

`dockertool dns mycontainer`

The HOSTS entry will match the container name, so after the command above, you should be able to ping your container using its name.

`ping mycontainer`
