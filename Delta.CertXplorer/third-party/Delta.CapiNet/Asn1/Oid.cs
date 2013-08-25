using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Delta.CapiNet.Asn1
{
    // This code is probably not mine... Though I can't remember where I grabbed it from.
    internal static class Oid
    {
        private static Dictionary<string, string> oidRepository = new Dictionary<string,string>();
        
        /// <summary>
        /// Initializes the <see cref="Oid"/> class.
        /// </summary>
        static Oid()
        {
            oidRepository.Add("0.9.2342.19200300.100.1.25", "domainComponent");
            oidRepository.Add("0.4.0.127.0.7.2.2.1.1", "PublicKey-DH");
            oidRepository.Add("0.4.0.127.0.7.2.2.1.2", "PublicKey-ECDH");
            oidRepository.Add("0.4.0.127.0.7.2.2.3.1", "ChipAuthentication-DH");
            oidRepository.Add("0.4.0.127.0.7.2.2.3.1.1", "ChipAuthentication-DH-3DES-CBC-CBC");
            oidRepository.Add("0.4.0.127.0.7.2.2.3.2", "ChipAuthentication-ECDH");
            oidRepository.Add("0.4.0.127.0.7.2.2.3.2.1", "ChipAuthentication-ECDH-3DES-CBC-CBC");
            oidRepository.Add("0.4.0.127.0.7.2.2.2.1", "TerminalAuthentication-RSA");
            oidRepository.Add("0.4.0.127.0.7.2.2.2.1.1", "TerminalAuthentication-RSA-V15-SHA1");
            oidRepository.Add("0.4.0.127.0.7.2.2.2.1.2", "TerminalAuthentication-RSA-V15-SHA256");
            oidRepository.Add("0.4.0.127.0.7.2.2.2.1.3", "TerminalAuthentication-RSA-PSS-SHA1");
            oidRepository.Add("0.4.0.127.0.7.2.2.2.1.4", "TerminalAuthentication-RSA-PSS-SHA256");
            oidRepository.Add("0.4.0.127.0.7.2.2.2.2", "TerminalAuthentication-ECDSA");
            oidRepository.Add("0.4.0.127.0.7.2.2.2.2.1", "TerminalAuthentication-ECDSA-SHA1");
            oidRepository.Add("0.4.0.127.0.7.2.2.2.2.2", "TerminalAuthentication-ECDSA-SHA224");
            oidRepository.Add("0.4.0.127.0.7.2.2.2.2.3", "TerminalAuthentication-ECDSA-SHA256");
            oidRepository.Add("0.4.0.127.0.7.3.1.2.1", "EAC-ePassport");
            oidRepository.Add("1.2.36.68980861.1.1.10", "Signet pilot");
            oidRepository.Add("1.2.36.68980861.1.1.11", "Signet intraNet");
            oidRepository.Add("1.2.36.68980861.1.1.2", "Signet personal");
            oidRepository.Add("1.2.36.68980861.1.1.20", "Signet securityPolicy");
            oidRepository.Add("1.2.36.68980861.1.1.3", "Signet business");
            oidRepository.Add("1.2.36.68980861.1.1.4", "Signet legal");
            oidRepository.Add("1.2.36.75878867.1.100.1.1", "Certificates Australia policyIdentifier");
            oidRepository.Add("1.2.752.34.1", "seis-cp");
            oidRepository.Add("1.2.752.34.1.1", "SEIS certificatePolicy-s10");
            oidRepository.Add("1.2.752.34.2", "SEIS pe");
            oidRepository.Add("1.2.752.34.3", "SEIS at");
            oidRepository.Add("1.2.752.34.3.1", "SEIS at-personalIdentifier");
            oidRepository.Add("1.2.840.10040.2.1", "holdinstruction-none");
            oidRepository.Add("1.2.840.10040.2.2", "holdinstruction-callissuer");
            oidRepository.Add("1.2.840.10040.2.3", "holdinstruction-reject");
            oidRepository.Add("1.2.840.10040.4.1", "dsa");
            oidRepository.Add("1.2.840.10040.4.3", "dsaWithSha1");
            oidRepository.Add("1.2.840.10045.1", "fieldType");
            oidRepository.Add("1.2.840.10045.1.1", "prime-field");
            oidRepository.Add("1.2.840.10045.1.2", "characteristic-two-field");
            oidRepository.Add("1.2.840.10045.1.2.1", "ecPublicKey");
            oidRepository.Add("1.2.840.10045.1.2.3", "characteristic-two-basis");
            oidRepository.Add("1.2.840.10045.1.2.3.1", "onBasis");
            oidRepository.Add("1.2.840.10045.1.2.3.2", "tpBasis");
            oidRepository.Add("1.2.840.10045.1.2.3.3", "ppBasis");
            oidRepository.Add("1.2.840.10045.2", "publicKeyType");
            oidRepository.Add("1.2.840.10045.2.1", "ecPublicKey");
            oidRepository.Add("1.2.840.10046.2.1", "dhPublicNumber");
            oidRepository.Add("1.2.840.113533.7", "nsn");
            oidRepository.Add("1.2.840.113533.7.65", "nsn-ce");
            oidRepository.Add("1.2.840.113533.7.65.0", "entrustVersInfo");
            oidRepository.Add("1.2.840.113533.7.66", "nsn-alg");
            oidRepository.Add("1.2.840.113533.7.66.10", "cast5CBC");
            oidRepository.Add("1.2.840.113533.7.66.11", "cast5MAC");
            oidRepository.Add("1.2.840.113533.7.66.12", "pbeWithMD5AndCAST5-CBC");
            oidRepository.Add("1.2.840.113533.7.66.13", "passwordBasedMac");
            oidRepository.Add("1.2.840.113533.7.66.3", "cast3CBC");
            oidRepository.Add("1.2.840.113533.7.67", "nsn-oc");
            oidRepository.Add("1.2.840.113533.7.67.0", "entrustUser");
            oidRepository.Add("1.2.840.113533.7.68", "nsn-at");
            oidRepository.Add("1.2.840.113533.7.68.0", "entrustCAInfo");
            oidRepository.Add("1.2.840.113533.7.68.10", "attributeCertificate");
            oidRepository.Add("1.2.840.113549.1.1", "pkcs-1");
            oidRepository.Add("1.2.840.113549.1.1.1", "rsaEncryption");
            oidRepository.Add("1.2.840.113549.1.1.2", "md2withRSAEncryption");
            oidRepository.Add("1.2.840.113549.1.1.3", "md4withRSAEncryption");
            oidRepository.Add("1.2.840.113549.1.1.4", "md5withRSAEncryption");
            oidRepository.Add("1.2.840.113549.1.1.5", "sha1withRSAEncryption");
            oidRepository.Add("1.2.840.113549.1.1.11", "sha256withRSAEncryption");
            oidRepository.Add("1.2.840.113549.1.1.12", "sha384withRSAEncryption");
            oidRepository.Add("1.2.840.113549.1.1.13", "sha512withRSAEncryption");
            oidRepository.Add("1.2.840.113549.1.1.14", "sha224withRSAEncryption");
            oidRepository.Add("1.2.840.113549.1.1.6", "rsaOAEPEncryptionSET");
            oidRepository.Add("1.2.840.113549.1.12", "pkcs-12");
            oidRepository.Add("1.2.840.113549.1.12.1", "pkcs-12-PbeIds");
            oidRepository.Add("1.2.840.113549.1.12.1.1", "pbeWithSHAAnd128BitRC4");
            oidRepository.Add("1.2.840.113549.1.12.1.2", "pbeWithSHAAnd40BitRC4");
            oidRepository.Add("1.2.840.113549.1.12.1.3", "pbeWithSHAAnd3-KeyTripleDES-CBC");
            oidRepository.Add("1.2.840.113549.1.12.1.4", "pbeWithSHAAnd2-KeyTripleDES-CBC");
            oidRepository.Add("1.2.840.113549.1.12.1.5", "pbeWithSHAAnd128BitRC2-CBC");
            oidRepository.Add("1.2.840.113549.1.12.1.6", "pbeWithSHAAnd40BitRC2-CBC");
            oidRepository.Add("1.2.840.113549.1.12.10", "pkcs-12Version1");
            oidRepository.Add("1.2.840.113549.1.12.10.1", "pkcs-12BadIds");
            oidRepository.Add("1.2.840.113549.1.12.10.1.1", "pkcs-12-keyBag");
            oidRepository.Add("1.2.840.113549.1.12.10.1.2", "pkcs-12-pkcs-8ShroudedKeyBag");
            oidRepository.Add("1.2.840.113549.1.12.10.1.3", "pkcs-12-certBag");
            oidRepository.Add("1.2.840.113549.1.12.10.1.4", "pkcs-12-crlBag");
            oidRepository.Add("1.2.840.113549.1.12.10.1.5", "pkcs-12-secretBag");
            oidRepository.Add("1.2.840.113549.1.12.10.1.6", "pkcs-12-safeContentsBag");
            oidRepository.Add("1.2.840.113549.1.12.2", "pkcs-12-ESPVKID");
            oidRepository.Add("1.2.840.113549.1.12.2.1", "pkcs-12-PKCS8KeyShrouding");
            oidRepository.Add("1.2.840.113549.1.12.3", "pkcs-12-BagIds");
            oidRepository.Add("1.2.840.113549.1.12.3.1", "pkcs-12-keyBagId");
            oidRepository.Add("1.2.840.113549.1.12.3.2", "pkcs-12-certAndCRLBagId");
            oidRepository.Add("1.2.840.113549.1.12.3.3", "pkcs-12-secretBagId");
            oidRepository.Add("1.2.840.113549.1.12.3.4", "pkcs-12-safeContentsId");
            oidRepository.Add("1.2.840.113549.1.12.3.5", "pkcs-12-pkcs-8ShroudedKeyBagId");
            oidRepository.Add("1.2.840.113549.1.12.4", "pkcs-12-CertBagID");
            oidRepository.Add("1.2.840.113549.1.12.4.1", "pkcs-12-X509CertCRLBagID");
            oidRepository.Add("1.2.840.113549.1.12.4.2", "pkcs-12-SDSICertBagID");
            oidRepository.Add("1.2.840.113549.1.12.5", "pkcs-12-OID");
            oidRepository.Add("1.2.840.113549.1.12.5.1", "pkcs-12-PBEID");
            oidRepository.Add("1.2.840.113549.1.12.5.1.1", "pkcs-12-PBEWithSha1And128BitRC4");
            oidRepository.Add("1.2.840.113549.1.12.5.1.2", "pkcs-12-PBEWithSha1And40BitRC4");
            oidRepository.Add("1.2.840.113549.1.12.5.1.3", "pkcs-12-PBEWithSha1AndTripleDESCBC");
            oidRepository.Add("1.2.840.113549.1.12.5.1.4", "pkcs-12-PBEWithSha1And128BitRC2CBC");
            oidRepository.Add("1.2.840.113549.1.12.5.1.5", "pkcs-12-PBEWithSha1And40BitRC2CBC");
            oidRepository.Add("1.2.840.113549.1.12.5.1.6", "pkcs-12-PBEWithSha1AndRC4");
            oidRepository.Add("1.2.840.113549.1.12.5.1.7", "pkcs-12-PBEWithSha1AndRC2CBC");
            oidRepository.Add("1.2.840.113549.1.12.5.2", "pkcs-12-EnvelopingID");
            oidRepository.Add("1.2.840.113549.1.12.5.2.1", "pkcs-12-RSAEncryptionWith128BitRC4");
            oidRepository.Add("1.2.840.113549.1.12.5.2.2", "pkcs-12-RSAEncryptionWith40BitRC4");
            oidRepository.Add("1.2.840.113549.1.12.5.2.3", "pkcs-12-RSAEncryptionWithTripleDES");
            oidRepository.Add("1.2.840.113549.1.12.5.3", "pkcs-12-SignatureID");
            oidRepository.Add("1.2.840.113549.1.12.5.3.1", "pkcs-12-RSASignatureWithSHA1Digest");
            oidRepository.Add("1.2.840.113549.1.3", "pkcs-3");
            oidRepository.Add("1.2.840.113549.1.3.1", "dhKeyAgreement");
            oidRepository.Add("1.2.840.113549.1.5", "pkcs-5");
            oidRepository.Add("1.2.840.113549.1.5.1", "pbeWithMD2AndDES-CBC");
            oidRepository.Add("1.2.840.113549.1.5.10", "pbeWithSHAAndDES-CBC");
            oidRepository.Add("1.2.840.113549.1.5.3", "pbeWithMD5AndDES-CBC");
            oidRepository.Add("1.2.840.113549.1.5.4", "pbeWithMD2AndRC2-CBC");
            oidRepository.Add("1.2.840.113549.1.5.6", "pbeWithMD5AndRC2-CBC");
            oidRepository.Add("1.2.840.113549.1.5.9", "pbeWithMD5AndXOR");
            oidRepository.Add("1.2.840.113549.1.7", "pkcs-7");
            oidRepository.Add("1.2.840.113549.1.7.1", "data");
            oidRepository.Add("1.2.840.113549.1.7.2", "signedData");
            oidRepository.Add("1.2.840.113549.1.7.3", "envelopedData");
            oidRepository.Add("1.2.840.113549.1.7.4", "signedAndEnvelopedData");
            oidRepository.Add("1.2.840.113549.1.7.5", "digestData");
            oidRepository.Add("1.2.840.113549.1.7.6", "encryptedData");
            oidRepository.Add("1.2.840.113549.1.7.7", "dataWithAttributes");
            oidRepository.Add("1.2.840.113549.1.7.8", "encryptedPrivateKeyInfo");
            oidRepository.Add("1.2.840.113549.1.9", "pkcs-9");
            oidRepository.Add("1.2.840.113549.1.9.1", "emailAddress");
            oidRepository.Add("1.2.840.113549.1.9.10", "issuerAndSerialNumber");
            oidRepository.Add("1.2.840.113549.1.9.11", "passwordCheck");
            oidRepository.Add("1.2.840.113549.1.9.12", "publicKey");
            oidRepository.Add("1.2.840.113549.1.9.13", "signingDescription");
            oidRepository.Add("1.2.840.113549.1.9.14", "extensionReq");
            oidRepository.Add("1.2.840.113549.1.9.15", "sMIMECapabilities");
            oidRepository.Add("1.2.840.113549.1.9.15.1", "preferSignedData");
            oidRepository.Add("1.2.840.113549.1.9.15.2", "canNotDecryptAny");
            oidRepository.Add("1.2.840.113549.1.9.15.3", "receiptRequest");
            oidRepository.Add("1.2.840.113549.1.9.15.4", "receipt");
            oidRepository.Add("1.2.840.113549.1.9.15.5", "contentHints");
            oidRepository.Add("1.2.840.113549.1.9.15.6", "mlExpansionHistory");
            oidRepository.Add("1.2.840.113549.1.9.16", "id-sMIME");
            oidRepository.Add("1.2.840.113549.1.9.16.0", "id-mod");
            oidRepository.Add("1.2.840.113549.1.9.16.0.1", "id-mod-cms");
            oidRepository.Add("1.2.840.113549.1.9.16.0.2", "id-mod-ess");
            oidRepository.Add("1.2.840.113549.1.9.16.1", "id-ct");
            oidRepository.Add("1.2.840.113549.1.9.16.1.1", "id-ct-receipt");
            oidRepository.Add("1.2.840.113549.1.9.16.2", "id-aa");
            oidRepository.Add("1.2.840.113549.1.9.16.2.1", "id-aa-receiptRequest");
            oidRepository.Add("1.2.840.113549.1.9.16.2.2", "id-aa-securityLabel");
            oidRepository.Add("1.2.840.113549.1.9.16.2.3", "id-aa-mlExpandHistory");
            oidRepository.Add("1.2.840.113549.1.9.16.2.4", "id-aa-contentHint");
            oidRepository.Add("1.2.840.113549.1.9.2", "unstructuredName");
            oidRepository.Add("1.2.840.113549.1.9.20", "friendlyName");
            oidRepository.Add("1.2.840.113549.1.9.21", "localKeyID");
            oidRepository.Add("1.2.840.113549.1.9.22", "certTypes");
            oidRepository.Add("1.2.840.113549.1.9.22.1", "x509Certificate");
            oidRepository.Add("1.2.840.113549.1.9.22.2", "sdsiCertificate");
            oidRepository.Add("1.2.840.113549.1.9.23", "crlTypes");
            oidRepository.Add("1.2.840.113549.1.9.23.1", "x509Crl");
            oidRepository.Add("1.2.840.113549.1.9.3", "contentType");
            oidRepository.Add("1.2.840.113549.1.9.4", "messageDigest");
            oidRepository.Add("1.2.840.113549.1.9.5", "signingTime");
            oidRepository.Add("1.2.840.113549.1.9.6", "countersignature");
            oidRepository.Add("1.2.840.113549.1.9.7", "challengePassword");
            oidRepository.Add("1.2.840.113549.1.9.8", "unstructuredAddress");
            oidRepository.Add("1.2.840.113549.1.9.9", "extendedCertificateAttributes");
            oidRepository.Add("1.2.840.113549.2", "digestAlgorithm");
            oidRepository.Add("1.2.840.113549.2.2", "md2");
            oidRepository.Add("1.2.840.113549.2.4", "md4");
            oidRepository.Add("1.2.840.113549.2.5", "md5");
            oidRepository.Add("1.2.840.113549.3", "encryptionAlgorithm");
            oidRepository.Add("1.2.840.113549.3.10", "desCDMF");
            oidRepository.Add("1.2.840.113549.3.2", "rc2CBC");
            oidRepository.Add("1.2.840.113549.3.3", "rc2ECB");
            oidRepository.Add("1.2.840.113549.3.4", "rc4");
            oidRepository.Add("1.2.840.113549.3.5", "rc4WithMAC");
            oidRepository.Add("1.2.840.113549.3.6", "DESX-CBC");
            oidRepository.Add("1.2.840.113549.3.7", "DES-EDE3-CBC");
            oidRepository.Add("1.2.840.113549.3.8", "RC5CBC");
            oidRepository.Add("1.2.840.113549.3.9", "RC5-CBCPad");
            oidRepository.Add("1.2.840.113556.4.3", "microsoftExcel");
            oidRepository.Add("1.2.840.113556.4.4", "titledWithOID");
            oidRepository.Add("1.2.840.113556.4.5", "microsoftPowerPoint");
            oidRepository.Add("1.3.14.2.26.5", "sha");
            oidRepository.Add("1.3.14.3.2.1.1", "rsa");
            oidRepository.Add("1.3.14.3.2.10", "desMAC");
            oidRepository.Add("1.3.14.3.2.11", "rsaSignature");
            oidRepository.Add("1.3.14.3.2.12", "dsa");
            oidRepository.Add("1.3.14.3.2.13", "dsaWithSHA");
            oidRepository.Add("1.3.14.3.2.14", "mdc2WithRSASignature");
            oidRepository.Add("1.3.14.3.2.15", "shaWithRSASignature");
            oidRepository.Add("1.3.14.3.2.16", "dhWithCommonModulus");
            oidRepository.Add("1.3.14.3.2.17", "desEDE");
            oidRepository.Add("1.3.14.3.2.18", "sha");
            oidRepository.Add("1.3.14.3.2.19", "mdc-2");
            oidRepository.Add("1.3.14.3.2.2", "md4WitRSA");
            oidRepository.Add("1.3.14.3.2.2.1", "sqmod-N");
            oidRepository.Add("1.3.14.3.2.20", "dsaCommon");
            oidRepository.Add("1.3.14.3.2.21", "dsaCommonWithSHA");
            oidRepository.Add("1.3.14.3.2.22", "rsaKeyTransport");
            oidRepository.Add("1.3.14.3.2.23", "keyed-hash-seal");
            oidRepository.Add("1.3.14.3.2.24", "md2WithRSASignature");
            oidRepository.Add("1.3.14.3.2.25", "md5WithRSASignature");
            oidRepository.Add("1.3.14.3.2.26", "sha1");
            oidRepository.Add("1.3.14.3.2.27", "dsaWithSHA1");
            oidRepository.Add("1.3.14.3.2.28", "dsaWithCommonSHA1");
            oidRepository.Add("1.3.14.3.2.29", "sha-1WithRSAEncryption");
            oidRepository.Add("1.3.14.3.2.3", "md5WithRSA");
            oidRepository.Add("1.3.14.3.2.3.1", "sqmod-NwithRSA");
            oidRepository.Add("1.3.14.3.2.4", "md4WithRSAEncryption");
            oidRepository.Add("1.3.14.3.2.6", "desECB");
            oidRepository.Add("1.3.14.3.2.7", "desCBC");
            oidRepository.Add("1.3.14.3.2.8", "desOFB");
            oidRepository.Add("1.3.14.3.2.9", "desCFB");
            oidRepository.Add("1.3.14.3.3.1", "simple-strong-auth-mechanism");
            oidRepository.Add("1.3.14.7.2.1.1", "ElGamal");
            oidRepository.Add("1.3.14.7.2.3.1", "md2WithRSA");
            oidRepository.Add("1.3.14.7.2.3.2", "md2WithElGamal");
            oidRepository.Add("1.3.36.3", "algorithm");
            oidRepository.Add("1.3.36.3.1", "encryptionAlgorithm");
            oidRepository.Add("1.3.36.3.1.1", "des");
            oidRepository.Add("1.3.36.3.1.1.1.1", "desECBPad");
            oidRepository.Add("1.3.36.3.1.1.1.1.1", "desECBPadISO");
            oidRepository.Add("1.3.36.3.1.1.2.1", "desCBCPad");
            oidRepository.Add("1.3.36.3.1.1.2.1.1", "desCBCPadISO");
            oidRepository.Add("1.3.36.3.1.2", "idea");
            oidRepository.Add("1.3.36.3.1.2.1", "ideaECB");
            oidRepository.Add("1.3.36.3.1.2.1.1", "ideaECBPad");
            oidRepository.Add("1.3.36.3.1.2.1.1.1", "ideaECBPadISO");
            oidRepository.Add("1.3.36.3.1.2.2", "ideaCBC");
            oidRepository.Add("1.3.36.3.1.2.2.1", "ideaCBCPad");
            oidRepository.Add("1.3.36.3.1.2.2.1.1", "ideaCBCPadISO");
            oidRepository.Add("1.3.36.3.1.2.3", "ideaOFB");
            oidRepository.Add("1.3.36.3.1.2.4", "ideaCFB");
            oidRepository.Add("1.3.36.3.1.3", "des-3");
            oidRepository.Add("1.3.36.3.1.3.1.1", "des-3ECBPad");
            oidRepository.Add("1.3.36.3.1.3.1.1.1", "des-3ECBPadISO");
            oidRepository.Add("1.3.36.3.1.3.2.1", "des-3CBCPad");
            oidRepository.Add("1.3.36.3.1.3.2.1.1", "des-3CBCPadISO");
            oidRepository.Add("1.3.36.3.2", "hashAlgorithm");
            oidRepository.Add("1.3.36.3.2.1", "ripemd160");
            oidRepository.Add("1.3.36.3.2.2", "ripemd128");
            oidRepository.Add("1.3.36.3.2.3", "ripemd256");
            oidRepository.Add("1.3.36.3.2.4", "mdc2singleLength");
            oidRepository.Add("1.3.36.3.2.5", "mdc2doubleLength");
            oidRepository.Add("1.3.36.3.3", "signatureAlgorithm");
            oidRepository.Add("1.3.36.3.3.1", "rsa");
            oidRepository.Add("1.3.36.3.3.1.1", "rsaMitSHA-1");
            oidRepository.Add("1.3.36.3.3.1.2", "rsaMitRIPEMD160");
            oidRepository.Add("1.3.36.3.3.2", "ellipticCurve");
            oidRepository.Add("1.3.36.3.4", "signatureScheme");
            oidRepository.Add("1.3.36.3.4.1", "iso9796-1");
            oidRepository.Add("1.3.36.3.4.2.1", "iso9796-2");
            oidRepository.Add("1.3.36.3.4.2.2", "iso9796-2rsa");
            oidRepository.Add("1.3.36.4", "attribute");
            oidRepository.Add("1.3.36.5", "policy");
            oidRepository.Add("1.3.36.6", "api");
            oidRepository.Add("1.3.36.6.1", "manufacturerSpecific");
            oidRepository.Add("1.3.36.6.2", "functionalitySpecific");
            oidRepository.Add("1.3.36.7", "api");
            oidRepository.Add("1.3.36.7.1", "keyAgreement");
            oidRepository.Add("1.3.36.7.2", "keyTransport");
            oidRepository.Add("1.3.6.1.4.1.2428.10.1.1", "UNINETT policyIdentifier");
            oidRepository.Add("1.3.6.1.4.1.2712.10", "ICE-TEL policyIdentifier");
            oidRepository.Add("1.3.6.1.4.1.3029.32.1", "cryptlibEnvelope");
            oidRepository.Add("1.3.6.1.4.1.3029.32.2", "cryptlibPrivateKey");
            oidRepository.Add("1.3.6.1.4.1.311.10.1", "certTrustList");
            oidRepository.Add("1.3.6.1.4.1.311.10.2", "nextUpdateLocation");
            oidRepository.Add("1.3.6.1.4.1.311.10.3.1", "certTrustListSigning");
            oidRepository.Add("1.3.6.1.4.1.311.10.3.2", "timeStampSigning");
            oidRepository.Add("1.3.6.1.4.1.311.10.3.3", "serverGatedCrypto");
            oidRepository.Add("1.3.6.1.4.1.311.10.3.4", "encryptedFileSystem");
            oidRepository.Add("1.3.6.1.4.1.311.10.4.1", "yesnoTrustAttr");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.10", "spcAgencyInfo");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.11", "spcStatementType");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.12", "spcSpOpusInfo");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.14", "certExtensions");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.15", "spcPelmageData");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.20", "spcLink");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.21", "individualCodeSigning");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.22", "commercialCodeSigning");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.25", "spcLink");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.26", "spcMinimalCriteriaInfo");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.27", "spcFinancialCriteriaInfo");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.28", "spcLink");
            oidRepository.Add("1.3.6.1.4.1.311.2.1.4", "spcIndirectDataContext");
            oidRepository.Add("1.3.6.1.5.5.7", "pkix");
            oidRepository.Add("1.3.6.1.5.5.7.1", "privateExtension");
            oidRepository.Add("1.3.6.1.5.5.7.1.1", "authorityInfoAccess");
            oidRepository.Add("1.3.6.1.5.5.7.2", "policyQualifierIds");
            oidRepository.Add("1.3.6.1.5.5.7.2.1", "cps");
            oidRepository.Add("1.3.6.1.5.5.7.2.2", "unotice");
            oidRepository.Add("1.3.6.1.5.5.7.3", "keyPurpose");
            oidRepository.Add("1.3.6.1.5.5.7.3.1", "serverAuth");
            oidRepository.Add("1.3.6.1.5.5.7.3.2", "clientAuth");
            oidRepository.Add("1.3.6.1.5.5.7.3.3", "codeSigning");
            oidRepository.Add("1.3.6.1.5.5.7.3.4", "emailProtection");
            oidRepository.Add("1.3.6.1.5.5.7.3.5", "ipsecEndSystem");
            oidRepository.Add("1.3.6.1.5.5.7.3.6", "ipsecTunnel");
            oidRepository.Add("1.3.6.1.5.5.7.3.7", "ipsecUser");
            oidRepository.Add("1.3.6.1.5.5.7.3.8", "timeStamping");
            oidRepository.Add("1.3.6.1.5.5.7.4", "cmpInformationTypes");
            oidRepository.Add("1.3.6.1.5.5.7.4.1", "caProtEncCert");
            oidRepository.Add("1.3.6.1.5.5.7.4.2", "signKeyPairTypes");
            oidRepository.Add("1.3.6.1.5.5.7.4.3", "encKeyPairTypes");
            oidRepository.Add("1.3.6.1.5.5.7.4.4", "preferredSymmAlg");
            oidRepository.Add("1.3.6.1.5.5.7.4.5", "caKeyUpdateInfo");
            oidRepository.Add("1.3.6.1.5.5.7.4.6", "currentCRL");
            oidRepository.Add("1.3.6.1.5.5.7.48.1", "ocsp");
            oidRepository.Add("1.3.6.1.5.5.7.48.2", "caIssuers");
            oidRepository.Add("1.3.6.1.5.5.8.1.1", "HMAC-MD5");
            oidRepository.Add("1.3.6.1.5.5.8.1.2", "HMAC-SHA");
            oidRepository.Add("2.16.840.1.101.2.1.1.1", "sdnsSignatureAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.10", "mosaicKeyManagementAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.11", "sdnsKMandSigAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.12", "mosaicKMandSigAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.13", "SuiteASignatureAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.14", "SuiteAConfidentialityAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.15", "SuiteAIntegrityAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.16", "SuiteATokenProtectionAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.17", "SuiteAKeyManagementAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.18", "SuiteAKMandSigAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.19", "mosaicUpdatedSigAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.2", "mosaicSignatureAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.20", "mosaicKMandUpdSigAlgorithms");
            oidRepository.Add("2.16.840.1.101.2.1.1.21", "mosaicUpdatedIntegAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.22", "mosaicKeyEncryptionAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.3", "sdnsConfidentialityAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.4", "mosaicConfidentialityAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.5", "sdnsIntegrityAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.6", "mosaicIntegrityAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.7", "sdnsTokenProtectionAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.8", "mosaicTokenProtectionAlgorithm");
            oidRepository.Add("2.16.840.1.101.2.1.1.9", "sdnsKeyManagementAlgorithm");
            oidRepository.Add("2.16.840.1.113730.1", "cert-extension");
            oidRepository.Add("2.16.840.1.113730.1.1", "netscape-cert-type");
            oidRepository.Add("2.16.840.1.113730.1.10", "EntityLogo");
            oidRepository.Add("2.16.840.1.113730.1.11", "UserPicture");
            oidRepository.Add("2.16.840.1.113730.1.12", "netscape-ssl-server-name");
            oidRepository.Add("2.16.840.1.113730.1.13", "netscape-comment");
            oidRepository.Add("2.16.840.1.113730.1.2", "netscape-base-url");
            oidRepository.Add("2.16.840.1.113730.1.3", "netscape-revocation-url");
            oidRepository.Add("2.16.840.1.113730.1.4", "netscape-ca-revocation-url");
            oidRepository.Add("2.16.840.1.113730.1.7", "netscape-cert-renewal-url");
            oidRepository.Add("2.16.840.1.113730.1.8", "netscape-ca-policy-url");
            oidRepository.Add("2.16.840.1.113730.1.9", "HomePage-url");
            oidRepository.Add("2.16.840.1.113730.2", "data-type");
            oidRepository.Add("2.16.840.1.113730.2.1", "GIF");
            oidRepository.Add("2.16.840.1.113730.2.2", "JPEG");
            oidRepository.Add("2.16.840.1.113730.2.3", "URL");
            oidRepository.Add("2.16.840.1.113730.2.4", "HTML");
            oidRepository.Add("2.16.840.1.113730.2.5", "netscape-cert-sequence");
            oidRepository.Add("2.16.840.1.113730.2.6", "netscape-cert-url");
            oidRepository.Add("2.16.840.1.113730.3", "directory");
            oidRepository.Add("2.16.840.1.113730.4.1", "serverGatedCrypto");
            oidRepository.Add("2.16.840.1.113733.1.6.3", "Unknown Verisign extension");
            oidRepository.Add("2.16.840.1.113733.1.6.6", "Unknown Verisign extension");
            oidRepository.Add("2.16.840.1.113733.1.7.1.1", "Verisign certificatePolicy");
            oidRepository.Add("2.16.840.1.113733.1.7.1.1.1", "Unknown Verisign policy qualifier");
            oidRepository.Add("2.16.840.1.113733.1.7.1.1.2", "Unknown Verisign policy qualifier");
            oidRepository.Add("2.23.133", "TCPA");
            oidRepository.Add("2.23.133.1", "tcpa_specVersion");
            oidRepository.Add("2.23.133.2", "tcpa_attribute");
            oidRepository.Add("2.23.133.2.1", "tcpa_at_tpmManufacturer");
            oidRepository.Add("2.23.133.2.10", "tcpa_at_securityQualities");
            oidRepository.Add("2.23.133.2.11", "tcpa_at_tpmProtectionProfile");
            oidRepository.Add("2.23.133.2.12", "tcpa_at_tpmSecurityTarget");
            oidRepository.Add("2.23.133.2.13", "tcpa_at_foundationProtectionProfile");
            oidRepository.Add("2.23.133.2.14", "tcpa_at_foundationSecurityTarget");
            oidRepository.Add("2.23.133.2.15", "tcpa_at_tpmIdLabel");
            oidRepository.Add("2.23.133.2.2", "tcpa_at_tpmModel");
            oidRepository.Add("2.23.133.2.3", "tcpa_at_tpmVersion");
            oidRepository.Add("2.23.133.2.4", "tcpa_at_platformManufacturer");
            oidRepository.Add("2.23.133.2.5", "tcpa_at_platformModel");
            oidRepository.Add("2.23.133.2.6", "tcpa_at_platformVersion");
            oidRepository.Add("2.23.133.2.7", "tcpa_at_componentManufacturer");
            oidRepository.Add("2.23.133.2.8", "tcpa_at_componentModel");
            oidRepository.Add("2.23.133.2.9", "tcpa_at_componentVersion");
            oidRepository.Add("2.23.133.3", "tcpa_protocol");
            oidRepository.Add("2.23.133.3.1", "tcpa_prtt_tpmIdProtocol");
            oidRepository.Add("2.23.136.1.1.1", "SOD");
            oidRepository.Add("2.23.42.0", "contentType");
            oidRepository.Add("2.23.42.0.0", "PANData");
            oidRepository.Add("2.23.42.0.1", "PANToken");
            oidRepository.Add("2.23.42.0.2", "PANOnly");
            oidRepository.Add("2.23.42.1", "msgExt");
            oidRepository.Add("2.23.42.10", "national");
            oidRepository.Add("2.23.42.10.192", "Japan");
            oidRepository.Add("2.23.42.2", "field");
            oidRepository.Add("2.23.42.2.0", "fullName");
            oidRepository.Add("2.23.42.2.1", "givenName");
            oidRepository.Add("2.23.42.2.10", "amount");
            oidRepository.Add("2.23.42.2.2", "familyName");
            oidRepository.Add("2.23.42.2.3", "birthFamilyName");
            oidRepository.Add("2.23.42.2.4", "placeName");
            oidRepository.Add("2.23.42.2.5", "identificationNumber");
            oidRepository.Add("2.23.42.2.6", "month");
            oidRepository.Add("2.23.42.2.7", "date");
            oidRepository.Add("2.23.42.2.7.11", "accountNumber");
            oidRepository.Add("2.23.42.2.7.12", "passPhrase");
            oidRepository.Add("2.23.42.2.8", "address");
            oidRepository.Add("2.23.42.2.9", "telephone");
            oidRepository.Add("2.23.42.3", "attribute");
            oidRepository.Add("2.23.42.3.0", "cert");
            oidRepository.Add("2.23.42.3.0.0", "rootKeyThumb");
            oidRepository.Add("2.23.42.3.0.1", "additionalPolicy");
            oidRepository.Add("2.23.42.4", "algorithm");
            oidRepository.Add("2.23.42.5", "policy");
            oidRepository.Add("2.23.42.5.0", "root");
            oidRepository.Add("2.23.42.6", "module");
            oidRepository.Add("2.23.42.7", "certExt");
            oidRepository.Add("2.23.42.7.0", "hashedRootKey");
            oidRepository.Add("2.23.42.7.1", "certificateType");
            oidRepository.Add("2.23.42.7.2", "merchantData");
            oidRepository.Add("2.23.42.7.3", "cardCertRequired");
            oidRepository.Add("2.23.42.7.4", "tunneling");
            oidRepository.Add("2.23.42.7.5", "setExtensions");
            oidRepository.Add("2.23.42.7.6", "setQualifier");
            oidRepository.Add("2.23.42.8", "brand");
            oidRepository.Add("2.23.42.8.1", "IATA-ATA");
            oidRepository.Add("2.23.42.8.30", "Diners");
            oidRepository.Add("2.23.42.8.34", "AmericanExpress");
            oidRepository.Add("2.23.42.8.4", "VISA");
            oidRepository.Add("2.23.42.8.5", "MasterCard");
            oidRepository.Add("2.23.42.8.6011", "Novus");
            oidRepository.Add("2.23.42.9", "vendor");
            oidRepository.Add("2.23.42.9.0", "GlobeSet");
            oidRepository.Add("2.23.42.9.1", "IBM");
            oidRepository.Add("2.23.42.9.10", "Griffin");
            oidRepository.Add("2.23.42.9.11", "Certicom");
            oidRepository.Add("2.23.42.9.12", "OSS");
            oidRepository.Add("2.23.42.9.13", "TenthMountain");
            oidRepository.Add("2.23.42.9.14", "Antares");
            oidRepository.Add("2.23.42.9.15", "ECC");
            oidRepository.Add("2.23.42.9.16", "Maithean");
            oidRepository.Add("2.23.42.9.17", "Netscape");
            oidRepository.Add("2.23.42.9.18", "Verisign");
            oidRepository.Add("2.23.42.9.19", "BlueMoney");
            oidRepository.Add("2.23.42.9.2", "CyberCash");
            oidRepository.Add("2.23.42.9.20", "Lacerte");
            oidRepository.Add("2.23.42.9.21", "Fujitsu");
            oidRepository.Add("2.23.42.9.22", "eLab");
            oidRepository.Add("2.23.42.9.23", "Entrust");
            oidRepository.Add("2.23.42.9.24", "VIAnet");
            oidRepository.Add("2.23.42.9.25", "III");
            oidRepository.Add("2.23.42.9.26", "OpenMarket");
            oidRepository.Add("2.23.42.9.27", "Lexem");
            oidRepository.Add("2.23.42.9.28", "Intertrader");
            oidRepository.Add("2.23.42.9.29", "Persimmon");
            oidRepository.Add("2.23.42.9.3", "Terisa");
            oidRepository.Add("2.23.42.9.30", "NABLE");
            oidRepository.Add("2.23.42.9.31", "espace-net");
            oidRepository.Add("2.23.42.9.32", "Hitachi");
            oidRepository.Add("2.23.42.9.33", "Microsoft");
            oidRepository.Add("2.23.42.9.34", "NEC");
            oidRepository.Add("2.23.42.9.35", "Mitsubishi");
            oidRepository.Add("2.23.42.9.36", "NCR");
            oidRepository.Add("2.23.42.9.37", "e-COMM");
            oidRepository.Add("2.23.42.9.38", "Gemplus");
            oidRepository.Add("2.23.42.9.4", "RSADSI");
            oidRepository.Add("2.23.42.9.5", "VeriFone");
            oidRepository.Add("2.23.42.9.6", "TrinTech");
            oidRepository.Add("2.23.42.9.7", "BankGate");
            oidRepository.Add("2.23.42.9.8", "GTE");
            oidRepository.Add("2.23.42.9.9", "CompuSource");
            oidRepository.Add("2.5.29.1", "authorityKeyIdentifier");
            oidRepository.Add("2.5.29.10", "basicConstraints");
            oidRepository.Add("2.5.29.11", "nameConstraints");
            oidRepository.Add("2.5.29.12", "policyConstraints");
            oidRepository.Add("2.5.29.13", "basicConstraints");
            oidRepository.Add("2.5.29.14", "subjectKeyIdentifier");
            oidRepository.Add("2.5.29.15", "keyUsage");
            oidRepository.Add("2.5.29.16", "privateKeyUsagePeriod");
            oidRepository.Add("2.5.29.17", "subjectAltName");
            oidRepository.Add("2.5.29.18", "issuerAltName");
            oidRepository.Add("2.5.29.19", "basicConstraints");
            oidRepository.Add("2.5.29.2", "keyAttributes");
            oidRepository.Add("2.5.29.20", "cRLNumber");
            oidRepository.Add("2.5.29.21", "cRLReason");
            oidRepository.Add("2.5.29.22", "expirationDate");
            oidRepository.Add("2.5.29.23", "instructionCode");
            oidRepository.Add("2.5.29.24", "invalidityDate");
            oidRepository.Add("2.5.29.25", "cRLDistributionPoints");
            oidRepository.Add("2.5.29.26", "issuingDistributionPoint");
            oidRepository.Add("2.5.29.27", "deltaCRLIndicator");
            oidRepository.Add("2.5.29.28", "issuingDistributionPoint");
            oidRepository.Add("2.5.29.29", "certificateIssuer");
            oidRepository.Add("2.5.29.3", "certificatePolicies");
            oidRepository.Add("2.5.29.30", "nameConstraints");
            oidRepository.Add("2.5.29.31", "cRLDistributionPoints");
            oidRepository.Add("2.5.29.32", "certificatePolicies");
            oidRepository.Add("2.5.29.33", "policyMappings");
            oidRepository.Add("2.5.29.34", "policyConstraints");
            oidRepository.Add("2.5.29.35", "authorityKeyIdentifier");
            oidRepository.Add("2.5.29.36", "policyConstraints");
            oidRepository.Add("2.5.29.37", "extKeyUsage");
            oidRepository.Add("2.5.29.4", "keyUsageRestriction");
            oidRepository.Add("2.5.29.5", "policyMapping");
            oidRepository.Add("2.5.29.6", "subtreesConstraint");
            oidRepository.Add("2.5.29.7", "subjectAltName");
            oidRepository.Add("2.5.29.8", "issuerAltName");
            oidRepository.Add("2.5.29.9", "subjectDirectoryAttributes");
            oidRepository.Add("2.5.4.0", "objectClass");
            oidRepository.Add("2.5.4.1", "aliasObjectName");
            oidRepository.Add("2.5.4.10", "organizationName");
            oidRepository.Add("2.5.4.11", "organizationalUnitName");
            oidRepository.Add("2.5.4.12", "title");
            oidRepository.Add("2.5.4.13", "description");
            oidRepository.Add("2.5.4.14", "searchGuide");
            oidRepository.Add("2.5.4.15", "businessCategory");
            oidRepository.Add("2.5.4.16", "postalAddress");
            oidRepository.Add("2.5.4.17", "postalCode");
            oidRepository.Add("2.5.4.18", "postOfficeBox");
            oidRepository.Add("2.5.4.19", "physicalDeliveryOfficeName");
            oidRepository.Add("2.5.4.2", "knowledgeInformation");
            oidRepository.Add("2.5.4.20", "telephoneNumber");
            oidRepository.Add("2.5.4.21", "telexNumber");
            oidRepository.Add("2.5.4.22", "teletexTerminalIdentifier");
            oidRepository.Add("2.5.4.23", "facsimileTelephoneNumber");
            oidRepository.Add("2.5.4.24", "x121Address");
            oidRepository.Add("2.5.4.25", "internationalISDNNumber");
            oidRepository.Add("2.5.4.26", "registeredAddress");
            oidRepository.Add("2.5.4.27", "destinationIndicator");
            oidRepository.Add("2.5.4.28", "preferredDeliveryMehtod");
            oidRepository.Add("2.5.4.29", "presentationAddress");
            oidRepository.Add("2.5.4.3", "commonName");
            oidRepository.Add("2.5.4.30", "supportedApplicationContext");
            oidRepository.Add("2.5.4.31", "member");
            oidRepository.Add("2.5.4.32", "owner");
            oidRepository.Add("2.5.4.33", "roleOccupant");
            oidRepository.Add("2.5.4.34", "seeAlso");
            oidRepository.Add("2.5.4.35", "userPassword");
            oidRepository.Add("2.5.4.36", "userCertificate");
            oidRepository.Add("2.5.4.37", "caCertificate");
            oidRepository.Add("2.5.4.38", "authorityRevocationList");
            oidRepository.Add("2.5.4.39", "certificateRevocationList");
            oidRepository.Add("2.5.4.4", "surname");
            oidRepository.Add("2.5.4.40", "crossCertificatePair");
            oidRepository.Add("2.5.4.41", "givenName");
            oidRepository.Add("2.5.4.42", "givenName");
            oidRepository.Add("2.5.4.5", "serialNumber");
            oidRepository.Add("2.5.4.52", "supportedAlgorithms");
            oidRepository.Add("2.5.4.53", "deltaRevocationList");
            oidRepository.Add("2.5.4.58", "crossCertificatePair");
            oidRepository.Add("2.5.4.6", "countryName");
            oidRepository.Add("2.5.4.7", "localityName");
            oidRepository.Add("2.5.4.8", "stateOrProvinceName");
            oidRepository.Add("2.5.4.9", "streetAddress");
            oidRepository.Add("2.5.8", "X.500-Algorithms");
            oidRepository.Add("2.5.8.1", "X.500-Alg-Encryption");
            oidRepository.Add("2.5.8.1.1", "rsa");
            oidRepository.Add("1.3.27.1.1", "id-icao-mrtd-security");
            oidRepository.Add("1.3.27.1.1.1", "LDSSecurityObject");
            oidRepository.Add("2.16.840.1.101.3.4.2.1", "sha256");
            oidRepository.Add("2.16.840.1.101.3.4.2.2", "sha384");
            oidRepository.Add("2.16.840.1.101.3.4.2.3", "sha512");
        }

        /// <summary>
        /// Retrieve OID name by OID code.
        /// </summary>
        /// <param name="oidKey">source OID code.</param>
        /// <returns>OID name.</returns>
        public static string GetOidName(string oidKey)
        {
            if (oidRepository.ContainsKey(oidKey))
                return oidRepository[oidKey];
            else return string.Empty;
        }

        /// <summary>
        /// Gets the oid key.
        /// </summary>
        /// <param name="oidName">Name of the oid.</param>
        /// <returns></returns>
        public static string GetOidKey(string oidName)
        {
            return oidRepository.Values.SingleOrDefault(v => v.Equals(oidName)) ?? string.Empty;
        }

        /// <summary>
        /// Decode OID byte array to OID string.
        /// </summary>
        /// <param name="data">source byte array.</param>
        /// <returns>result OID string.</returns>
        public static string Decode(byte[] data)
        {
            string result = string.Empty;
            using (var ms = new MemoryStream(data))
            {
                ms.Position = 0;
                result = Decode(ms);
                ms.Close();
            }
            return result;
        }

        /// <summary>
        /// Decode OID <see cref="Stream"/> and return OID string.
        /// </summary>
        /// <param name="stream">source stream.</param>
        /// <returns>result OID string.</returns>
        public static string Decode(Stream stream)
        {
            var result = string.Empty;
            
            var b = (byte)stream.ReadByte();
            result += Convert.ToString(b / 40);
            result += "." + Convert.ToString(b % 40);

            var v = 0UL;
            while (stream.Position < stream.Length)
            {
                try
                {
                    DecodeValue(stream, ref v);
                    result += "." + v.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to decode OID value: " + ex.Message, ex);
                }
            }

            return result;
        }

        /// <summary>
        /// Decode a single OID value.
        /// </summary>
        /// <param name="stream">source stream.</param>
        /// <param name="v">output value</param>
        /// <returns>OID value bytes.</returns>
        private static int DecodeValue(Stream stream, ref ulong value)
        {
            byte b = 0;
            int i = 0;
            value = 0UL;
            while (true)
            {
                b = (byte)stream.ReadByte();
                i++;
                value <<= 7;
                value += (ulong)(b & 0x7f);
                if ((b & 0x80) == 0) return i;
            }
        }
    }
}
