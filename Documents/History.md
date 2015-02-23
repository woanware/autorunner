# History #

**v0.0.9**

- Updated to use .Net 4.5.2
- Corrected registry paths for two items in the config file
- Improved path parsing
- Improved error logging
- Made the application x64 only
- Updated CsvHelper library
- Updated ObjectListView library

**v0.0.8**

- Added extra error handling and reporting to ensure that any strange data does not cause values to be missed
- Removed the VirusTotal integration as I would rather have distinct applications that perform one task well 
- Modified the service/driver analysis so that all service/driver types are output
- Added service display name and service description columns
- Modified the export to include the service display name and service description
- Modified the analysis to attempt to take ownership of the files it is processing  

**v0.0.7**

- Modified the Registry Modified data so that it can be sorted in the list
- Modified the export Date/Times so that they are in an easily sortable format e.g.  2009-06-15T13:45:30

**v0.0.6**

- Added the ability to filter the list's data. Use the right click context menu on the lists column headers to access
- Fixed issue where the SQL CE data provider was not configured with the applications config file. Thanks TimoJ
- Fixed issue where a rogue file path had null bytes at the end of the path, which resulted in the path handling generating an error. Thanks TimoJ
- Modified the export to include the newly added columns e.g. file system and registry timestamps
- Added a Source File column which show which registry file the entry was extracted from, which should be useful when multiple ntuser.dat files are processed

**v0.0.5**

- Fixed a path normalisation issue where the drive might not be correctly replaced with the drive mapping drive
- Added new columns to display the file system created, accessed, modified timestamps
- Added new column to display the registry keys modified timestamp

**v0.0.4**

- Added missing settings.xml file to releases. Thanks RobL
- Added error logging to the VirusTotal.NET library, outputs to the users local AppData directory for the application e.g. 
    C:\Users\ABC\AppData\Local\woanware\virustotalchecker\Errors.txt
- Added new Updated event to the VirusTotal.NET library
- Added parsing of the AppInit_DLL keys
- Added parsing of the BHO keys
- Updated the VirusTotal.NET to v1.0.2 which should fix a number of issues including the VT checking not starting and the improvement of handling resources that don’t exist. Thanks RobL

**v0.0.3**

- Corrected missing field from CSV export (File Version)
- Modified to clear list on each run
- Fixed issue where path replace failed due to case
- Added ability to copy entry information to clipboard
- Added ability to export a sorted and uniqued list of MD5 hashes
- Added double edit to mapping list
- Fixed error that occurred when enumerating drives that are not ready

**v0.0.2**

- Updated to extract ServiceDll

**v0.0.1**

- Initial public release

