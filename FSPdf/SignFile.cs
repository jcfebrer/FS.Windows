using FSException;
using FSLibrary;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Crypto;
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
                throw new ExceptionUtil("Fichero pdf de origen, no existe.");
            if (certificate == null)
                throw new ExceptionUtil("Certificado no especificado.");
            if (!certificate.HasPrivateKey)
                throw new ExceptionUtil("Certificado sin clave privada");

            try
            {
                using (FileStream fsSource = new FileStream(sourcePdf, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (PdfReader reader = new PdfReader(fsSource))
                    {
                        using (FileStream fs = new FileStream(targetPdf, FileMode.Create, FileAccess.Write, FileShare.None))
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

                                int page = 1; //reader.NumberOfPages
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
                                        //width = imageSignature.Width;
                                        //height = imageSignature.Height;
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

                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al firmar el documento: " + ex.ToString());
            }
        }
    }
}