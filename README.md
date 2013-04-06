autorunner
==========

autorunner is based upon the AutoRuns tool by the Sysinternals/Microsoft gurus. It is designed to perform automated [Authenticode](http://msdn.microsoft.com/en-gb/library/ms537359(v=vs.85).aspx) checking for binaries designed to auto-start on a host.

It will check against all user profiles associated with the host. It's primary purpose is to aid forensic investigations.

The application should be used against a forensic image that has been mounted using what ever method you desire.

## Features ##

- Processes every user profile's path
- Checks the authenticode signature
- Normalises binary path
- Can perform hash checks against virus total

## Third party libraries ##

- [DotNetZip](http://dotnetzip.codeplex.com/) : Used to download the sigcheck tool and unzip
- [MS SQL CE](http://www.microsoft.com/en-us/download/details.aspx?id=30709) : Access to MS SQL CE session database (Used by the VirusTotal.NET fork for database caching of results)
- [sigcheck](http://technet.microsoft.com/en-gb/sysinternals/bb897441.aspx) : Sysinternals tools to perform file signature checks 
- [ProcessPrivileges](http://processprivileges.codeplex.com/): Process Privileges is a set of extension methods, written in C#, for System.Diagnostics.Process. It implements the functionality necessary to query, enable, disable or remove privileges on a process
- [Shellify](http://sourceforge.net/projects/shellify/) : LNK file parsing
- [ObjectListView](http://objectlistview.sourceforge.net/cs/index.html) : Data viewing via lists 
- [CsvHelper](https://github.com/JoshClose/CsvHelper): CSV output
- [VirusTotal.NET](https://github.com/woanware/VirusTotal.NET): Fork from https://github.com/Genbox/VirusTotal.NET
- [Registry](https://github.com/woanware/Registry): Binary Registry parser (woanware)
- [Utility]: Binary Registry parser (woanware)

## Requirements ##

- Microsoft .NET Framework v4.5