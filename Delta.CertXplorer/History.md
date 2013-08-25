CertXplorer Versions History
============================

**Now CryptExplorer is CertXplorer and is Open-source.** See [License.md](./License.md)

Version 2.3.2 (2013/08/24)
--------------------------
* BUGFIX: tree nodes should NOT be sorted in the ASN.1 viewer

Version 2.3.1 (2013/08/24)
--------------------------
* Delta.CapiNet part of the source code: eases the bugfix process.
* Using Delta.CapiNet 1.0.3 fixed version.

Version 2.3.0 (2013/08/15)
--------------------------
* Removed **CapiNet** from the source tree (now a project of its own and named **Delta.CapiNet**)
* Targetting .NET 4
* Upgraded Log4Net to Version 1.2.11
* Upgraded WeifenLuo's Docking to Version 2.7
* Plugins: 
	* **CryptoHelperPlugin** Version 1.0.1 (Fixed missing docking)
	* **Pluralsight's plugin**: fixed bad window placement + removed min/max buttons + not resizable (in **Pluralsight.Crypto** assembly; hence Version 1.1.1). 

-----------------------------------------------------------------------------------------

History of CertXplorer's ancestor: CryptExplorer
================================================

2.2.3 (2013/06/21)
------------------
* Added (_first_) support for passing files on the command line.

2.2.2
-----
* Fixed the **System.ArgumentOutOfRangeException: Not a valid Win32 FileTime** exception when a file date in a certificate or a revocation list is not valid.

2.2.1
-----
* Fixed plugins version number (they may not be equal to the main app versiojn number
* Replaced Error box with an Exception box when a ASN1 document throws
* Bound log4net to the CapiNet library logging functionality.
* Fixed parsing of unsupported or buggy tags: doesn't stop the document processing any more.
* Added the option to show invalid tagged objects or not.
* Refactoring

2.2.0
-----
* Wrapped **X509Certificate**, **X509Certificate2** & **X509Extension** objects so that they appear in a friendlier way in property grid. 

2.1.0
-----
* Refactoring
* Added a custom About dialog box displaying plugins information.
* This time, the layout save/restore bug is really fixed: the service itself was buggy...  

2.0.3
-----
* Fixed theme inconsistencies
* Fixed exception box when clicking **Decode...** and no object is selected in the Certificate List view.
* Added keyboard shortcuts to menus.

2.0.2
-----
* layout save/restore seems to be fixed... not sure because I don't understand why it now works.

2.0.1
-----
* Allows multiple instances
* New UI theme

2.0.0
-----
* Introduced the plugins object model
* Test plugin
* Pluralsight tool integrated as a plugin.

-----------------------------------------------------------------------------------------
* License: [Ms-RL][msrl]
* History page: [Here][history]
* Credits page: [Here][credits]

  [msrl]: License.md "MS-RL License"
  [history]: History.md "History"
  [credits]: Credits.md "Credits"