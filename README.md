```
        Extensions to 
        ██╗  ██╗██████╗ ██████╗ ███████╗ ██████╗     
        ╚██╗██╔╝╚════██╗╚════██╗╚════██║██╔═████╗    
         ╚███╔╝  █████╔╝ █████╔╝    ██╔╝██║██╔██║    
         ██╔██╗  ╚═══██╗██╔═══╝    ██╔╝ ████╔╝██║    
        ██╔╝ ██╗██████╔╝███████╗   ██║  ╚██████╔╝    
        ╚═╝  ╚═╝╚═════╝ ╚══════╝   ╚═╝   ╚═════╝     
                             (c) Steven England
```
X3270
===

X3270 is the namespace under which I want to release libraries that expose additional functionalities for the x3270 client by Paul Mattes.

See the [offcial X3270 wiki](https://github.com/stevenengland/X3270/wiki/X3270) for more information.

X3270.Rest
===

X3270.Rest is a wrapper for the standard x3270 HTTP REST interface. Remote controlling the x3270 client via HTTP is very advantageous because the HTTP protocol is very mature and as popular as never before. This imlies a broad support of any kind. In terms of .NET this means that one can easily build really asynchronous 
calls from ones application to the x3270 terminal by using just a few native features for HTTP. This is why I built X3270.Rest that wrapps all the calls to x3270 for you in a straighforward manner.

To find out more have a look at
* the [X3270.Rest wiki pages](https://github.com/stevenengland/X3270/wiki/X3270-REST) or 
* the [X3270.Rest test project](https://github.com/stevenengland/X3270/tree/master/src/StEn.X3270.Rest.Test)

Testing:
---
For most of the testing actions you need to set up a x3270 client and Sim390 emulator. These are not privided within this package.
instead have a look at:

* Sim390 executable: http://www.canpub.com/teammpg/de/sim390/download.htm (Windows executable v1.7)
* Sim390 os file: http://www.canpub.com/teammpg/de/mcgweb/downloads.htm (MUSIC/SP 6.2 Demo system, version "B")
* x3270 terminal client: http://x3270.bgp.nu/download.html (3.5ga10)

The following directory structure would be complient with this project:
```
project_root
|-MockServer
 |-sim390
 |  |-sim390_17.exe
 |-x3270
    |-wc3270.exe
```
These command line arguments will be used for this project:

First Sim90 emulator:
```
sim390_17.exe config.txt
```
Then x3270:
```
wc3270 -httpd 6001 +S "test.wc3270"
```
Credits
===

Thank you Paul Mattes for building x3270 on which this repo is based on.