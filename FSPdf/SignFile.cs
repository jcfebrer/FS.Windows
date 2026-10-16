#if NET461_OR_GREATER || NETCOREAPP

using FSException;
using FSLibrary;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace FSPdf
{
    public class SignFile
    {
        /// <summary>
        /// Firma un documento
        /// </summary>
        /// <param name="sourcePdf">Documento origen</param>
        /// <param name="targetPdf">Documento destino</param>
        /// <param name="certificate">Certificado a utilizar</param>
        /// <param name="reason">Razón de la firma</param>
        /// <param name="location">Ubicación</param>
        /// <param name="addVisibleSign">Establece si hay que agregar la firma visible al documento</param>
        /// <param name="imageUrl">Fichero gráfico para mostrar en la firma</param>
        public static bool SignHashed(string sourcePdf, string targetPdf, X509Certificate2 certificate, string reason, string location, bool addVisibleSign, string imageUrl)
        {
            if (!File.Exists(sourcePdf))
                throw new Exception("Fichero pdf de origen, no existe.");
            if (certificate == null)
                throw new Exception("Certificado no especificado.");
            if (!certificate.HasPrivateKey)
                throw new Exception("Certificado sin clave privada");

            // Verificar si el origen y destino son la misma ruta
            bool isSameFile = string.Equals(Path.GetFullPath(sourcePdf), Path.GetFullPath(targetPdf), StringComparison.OrdinalIgnoreCase);
            string tempPdfPath = isSameFile ? Path.GetTempFileName() : null;
            string actualOutputPath = isSameFile ? tempPdfPath : targetPdf;

            try
            {
                using (FileStream fsSource = new FileStream(sourcePdf, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (PdfReader reader = new PdfReader(fsSource))
                using (FileStream fs = new FileStream(actualOutputPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfStamper stamper = PdfStamper.CreateSignature(reader, fs, '\0');

                    PdfSignature dic = new PdfSignature(PdfName.ADOBE_PPKLITE, PdfName.ADBE_PKCS7_DETACHED);
                    dic.Reason = reason;
                    dic.Location = location;
                    dic.Date = new PdfDate(DateTime.Now);
                    dic.Name = certificate.FriendlyName ?? certificate.Subject;

                    PdfSignatureAppearance signatureAppearance = stamper.SignatureAppearance;
                    signatureAppearance.Reason = reason;
                    signatureAppearance.LocationCaption = location;
                    signatureAppearance.CryptoDictionary = dic;

                    if (addVisibleSign)
                    {
                        signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;

                        int page = 1;
                        int posx = 100;
                        int posy = 100;
                        float width = 300;
                        float height = 300;

                        iTextSharp.text.Image imageSignature = null;
                        if (!String.IsNullOrEmpty(imageUrl))
                        {
                            imageSignature = iTextSharp.text.Image.GetInstance(imageUrl);

                            if (imageSignature != null)
                            {
                                imageSignature.Transparency = new int[] { 5 };
                                signatureAppearance.SignatureGraphic = imageSignature;
                            }
                        }

                        signatureAppearance.SetVisibleSignature(
                            new iTextSharp.text.Rectangle(posx, posy, width, height),
                            page,
                            "Signature");
                    }

#if NETFRAMEWORK
                    var privateKey = certificate.PrivateKey;
#else
                    var privateKey = certificate.GetRSAPrivateKey();
#endif

                    var keyPair = Org.BouncyCastle.Security.DotNetUtilities.GetKeyPair(privateKey).Private;
                    Org.BouncyCastle.X509.X509Certificate bcCert = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(certificate);
                    var chain = new List<Org.BouncyCastle.X509.X509Certificate> { bcCert };
                    IExternalSignature signature = new PrivateKeySignature(keyPair, "SHA-256");

                    MakeSignature.SignDetached(signatureAppearance, signature, chain, null, null, null, 0, CryptoStandard.CMS);

                    stamper.Close();
                }

                // Si origen y destino son el mismo archivo, sobrescribimos el original con el temporal
                if (isSameFile)
                {
                    File.Copy(tempPdfPath, targetPdf, true);
                    File.Delete(tempPdfPath);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Limpieza del archivo temporal en caso de fallo
                if (isSameFile && File.Exists(tempPdfPath))
                {
                    try { File.Delete(tempPdfPath); } catch { }
                }

                throw new Exception("Error al firmar el documento: " + ex.ToString());
            }
        }

        public static bool SignHashed2(string rutaOrigen, string rutaDestino, X509Certificate2 certificadoNet)
        {
            // 1. Cargar el documento con PDFsharp
            PdfSharp.Pdf.PdfDocument document = PdfSharp.Pdf.IO.PdfReader.Open(rutaOrigen, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Modify);

            // 2. Preparar el diccionario de la firma (el "espacio" en el PDF)
            PdfSharp.Pdf.PdfDictionary sandbox = new PdfSharp.Pdf.PdfDictionary(document);
            sandbox.Elements.SetName(PdfSharp.Pdf.Annotations.PdfAnnotation.Keys.Type, "/Sig");
            sandbox.Elements.SetName("/Filter", "/Adobe.PPKLITE");
            sandbox.Elements.SetName("/SubFilter", "/adbe.pkcs7.detached");
            sandbox.Elements.SetDateTime(PdfSharp.Pdf.Annotations.PdfAnnotation.Keys.M, DateTime.Now);
            sandbox.Elements.SetString("/Name", certificadoNet.Subject);

            // Añadir el diccionario al documento
            document.Internals.AddObject(sandbox);
            document.Internals.Catalog.Elements.SetReference("/Perms", sandbox);

            // 3. Convertir certificado .NET a BouncyCastle para la criptografía
            byte[] certRawData = certificadoNet.Export(X509ContentType.Pfx, "0000");

            Pkcs12Store store = new Pkcs12StoreBuilder().Build();
            using (var ms = new MemoryStream(certRawData))
            {
                store.Load(ms, "0000".ToCharArray());
            }

            string alias = null;
            foreach (string a in store.Aliases) { if (store.IsKeyEntry(a)) { alias = a; break; } }

            ICipherParameters privateKey = store.GetKey(alias).Key;
            X509CertificateEntry[] chain = store.GetCertificateChain(alias);
            Org.BouncyCastle.X509.X509Certificate bcCert = chain[0].Certificate;

            // 4. Guardar el documento (PDFsharp genera la estructura)
            document.Save(rutaDestino);

            // NOTA: Para firmas avanzadas, PDFsharp suele requerir escribir 
            // los bytes de la firma manualmente en el stream del archivo guardado.
            Console.WriteLine("Documento procesado. Nota: PDFsharp requiere post-procesamiento manual para firmas PKCS7.");
            return true;
        }
    }
}

#endif