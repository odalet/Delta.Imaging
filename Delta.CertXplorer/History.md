CertXplorer Versions History
============================

Version 2.4.8 (2013/xx/xx)
--------------------------
* UI Fix in CryptoHelperPlugin (added a splitter)
* Changed default base64 text to "Lorem Ipsum" in CryptoHelperPlugin
* Removed the error box when the user cancels mmc.exe UAC box.
* Issue #12: Added Copy/Print/Save links (and contextual menu) to the details tab of the exception box.
* Issue #11: Fixed "Open Certificate" command  
* Issue #10: Fixed "Open File" command  
* Issue #8: First support of Certificate Trust Lists

Version 2.4.7 (2013/09/29)
--------------------------
* Updated configuration system to store the application culture
* Added MessageBox apis to CertXplorer.Extensibility
* New plugin: CertToolsPlugin

Version 2.4.6 (2013/09/29)
--------------------------
* Updated Clickonce certificate

Version 2.4.5 (2013/09/25)
--------------------------
* Minor warning fixes (+ updated the publisher to Delta Apps)
* Moved the location of certx.layout.xml and certx.log4net.config settings files to user's directory so that 
  they are recovered after a clickonce update.
* Modified PluralSightSelfCertPlugin (1.1.3) and Pluralsight.Crypto.dll (1.1.2) so that it saves its settings in the user's directory alongside the
  application other configuration files.

Version 2.4.4 (2013/09/21)
--------------------------
* Delta.CapiNet: Version 1.1.1
* Crl Wrapper used in property grids
* BUGFIX: Certificates viewer was cleared when a new tab opened (after double-clicking a certificate for example).

Version 2.4.3 (2013/08/30)
--------------------------
* Fixed ClickOnce deployment settings

Version 2.4.2 (2013/08/30)
--------------------------
* Minor changes in ExceptionBox and some extension methods
* Fixed Version.

Version 2.4.1 (2013/08/30)
--------------------------
* Moved (and improved) PEM decoder to Delta.CapiNet (now version 1.1.0).

Version 2.4.0 (2013/08/30)
--------------------------
* Added the notion of Data Handler Plugins
* Made PEM support a data handler plugin.
* Truncated very long Octet and Bit strings in the ASN1 Treeview

Version 2.3.3 (2013/08/29)
--------------------------
* Refactoring of commands/verbs system
* Refactoring of documents model
* Added support of PEM files

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

**Now CryptExplorer is CertXplorer and is Open-source.** See [License.md](./License.md)

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
* Fixed plugins version number (they may not be equal to the main app version number
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