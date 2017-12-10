# dockertool
This tool gets the IP address of a running container and then adds it to the Windows HOSTS file. This is a workaround for the [Windows NAT interface bug](https://blog.sixeyed.com/published-ports-on-windows-containers-dont-do-loopback/).

If the container is not running, the tool will attempt to start it. The tool will ask for elevated User Permissions because it needs to edit the HOSTS file which is protected.

Usage:

`dockertool dns mycontainer`
