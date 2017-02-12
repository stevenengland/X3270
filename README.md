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

X3270.Rest
===

X3270.Rest is a wrapper for the standard x3270 REST interface.

More content coming soon...

Testing:
---
For testing purposes you need to set up a x3270 client and Sim390 emulator. These are not privided within this package.
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