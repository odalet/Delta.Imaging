Version 1.1.1 (2013/09/21)
--------------------------
* Added a few properties to the Certificate class + renamed X509Certificate3 property to X509.
* Added X509Extensions.GetCertificates method.
* Refactoring of UI class (extension methods + parameter reordering + added ShowCertificateDialog 
  methods taking a Certificate object as first parameter (note that this is a breaking change in the API).

Version 1.1.0 (2013/09/01)
--------------------------
* Added a PEM Decoder.

Version 1.0.3
-------------------------
* BUGFIX: Fixed handles release code (CertContextHandle and CrlContextHandle)
* BUGFIX: Prevented CertStoreHandle created from X509Store objects to release their handle (it is owned by the X509Store object).

Version 1.0.2
-------------------------
Now the assembly (and root namespace) is named **Delta.CapiNet** and is part of the **Delta** git repo.

Version 1.0.1
-------------------------
Some cleanup part of open-sourcing **CryptExplorer** (and renaming it **CertXplorer**).

Version 1.0.0
-------------------------
At this time **CapiNet** was part of a personal project called **CryptExplorer**.