autorunner
==========

autorunner is based upon the AutoRuns tool by the Sysinternals/Microsoft gurus. It is designed to perform automated [Authenticode](http://msdn.microsoft.com/en-gb/library/ms537359(v=vs.85).aspx) checking for binaries designed to auto-start on a host. Its primary purpose is to aid forensic investigations.

I didn't want to write this application, it was one that I had to...software persistence is a key factor when identifying malware and I wanted AutoRuns to work. Ideally I would have just run the command line version of it and parsed the output and performed other checks on the data extracted, however, this is flawed for a number of reasons. 

The key one being I can never get it to run on more than one user profile, so if I am working on a host with two user accounts, then I can extract the data from the first profile, but when run it against the second profile then it fails. Or I can reboot, remount the image, then it will load the second profile and not the first?!

The second issue that is in off-line mode it needs the user to supply the path to the user profile, which is fine for one profile, but we will work with hosts that have numerous profiles.



So autorunner is designed to work around all of these issues. It will check against all user profiles associated with the host. It will parse out LNK files to the actual binary (one level down). It allows the user to specify multiple drive mappings, so that if the forensic image contains multiple partitions you can map the original drives to mounted drives on the forensic workstation.

The application should be used against a forensic image that has been mounted using whatever method you desire. 

## Notes

- Ensure that the image is mounted writeable, so that the writes are cached and the original image is not affected. This is so the application can take ownership of the files where necessary to ensure that everything is accessible to retrieve the file attributes**
- Make sure you run the application with elavated privileges e.g. Run As Administrator**

## Features ##

- Processes every user profile's path in one go
- Checks the authenticode signature via the Windows security catalog
- Parses LNK files
- Normalises binary path

## Third party libraries ##

- [CsvHelper](https://github.com/JoshClose/CsvHelper): CSV output
- [DotNetZip](http://dotnetzip.codeplex.com/) : Used to download the sigcheck tool and unzip
- [sigcheck](http://technet.microsoft.com/en-gb/sysinternals/bb897441.aspx) : Sysinternals tools to perform file signature checks 
- [ObjectListView](http://objectlistview.sourceforge.net/cs/index.html) : Data viewing via lists 
- [ProcessPrivileges](http://processprivileges.codeplex.com/): Process Privileges is a set of extension methods, written in C#, for System.Diagnostics.Process. It implements the functionality necessary to query, enable, disable or remove privileges on a process
- [Shellify](http://sourceforge.net/projects/shellify/) : LNK file parsing
- [Registry](https://github.com/woanware/Registry): Binary Registry parser (woanware)
- [Utility](http://www.woanware.co.uk): Helper functions (woanware)

## Requirements ##

- Microsoft .NET Framework v4.7.2
- Windows (x64)