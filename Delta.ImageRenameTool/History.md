Version 1.2.3.1 (2016/07/31)
--------------------------
* Refactoring, C#6 and VS2015

Version 1.2.3 (2015/08/09)
--------------------------
* New Name and Result columns are now read-only (no more customization of the generated name is possible).
* BUGFIX: do not crash any more when a loaded file does not exist anymore.
* Modified the 'Description Guess' Algorithm so that it is more accurate.

Version 1.2.2.1 (2015/08/06)
--------------------------
* Fixed warnings emitted by Visual Studio 2015

Version 1.2.2 (2013/08/16)
--------------------------
* BUGFIX: When no EXIF data, the file creation date is used instead of the exif date (which is MinDate)
* We now try to guess what the description can be by parsing the file name.
* Added a 'Clear Descriptions' button.
* Modified About box (removed default image and copyright label).

Version 1.2.1 (2013/08/15)
--------------------------
* Renamed the project into **Delta.ImageRenameTool**; now part of the **Delta** git repo.

Version 1.2.0 (2013/07/29)
--------------------------
* Uses the EXIF Orientation tag to apply rotations when previewing images
* Moved to VS 2012 Update 3
* Targetted .NET 3.5 Client Profile instead of the full framework
* Used WPF for the new preview control (easing the rotation process).
* BUGFIX: use EXIF "Date Time" tag as a last resort, only if "DTDigitized" and "DTOrig" do not exist.
  It seems that "DateTime" gets modified (by Windows 8 ???) when an image is copied.

Version 1.1.1 (2011/07/17) 
--------------------------
* Fixed the "Date" column sort bug. Now sorts on the Photo date and not on the file creation date.

Version 1.1.0 (2011/07/17)
--------------------------
* Now guesses the description part of already renamed files
* Added Exif decoding
* The _Photo date_ is now based on Exif data.

Version 1.0.2 (2010/01/02)
--------------------------
* Full version displayed in title bar.
* Added _FolderBrowserDialogEx_ in replacement of the standard .NET folder browser.
* Added an icon.
                     
Version 1.0.1 (2010/01/02)
--------------------------
* Fixed the _nudFirstIndex.MaximumValue_ to _decimal.MaxValue_ instead of 100 (default value).

Version 1.0.0 
-------------
* First version

-----------------------------------------------------------------------------------------
* License: [Ms-RL][msrl]
* History page: [Here][history]
* Credits page: [Here][credits]

  [msrl]: License.md "MS-RL License"
  [history]: History.md "History"
  [credits]: Credits.md "Credits"