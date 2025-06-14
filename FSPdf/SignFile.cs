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
        public static void SignHashed(string sourcePdf, string targetPdf, X509Certificate2 certificate, string reason, string location, bool addVisibleSign, string imageUrl)
        {
            if (!certificate.HasPrivateKey)
                throw new ExceptionUtil("Certificado sin clave privada");

            PdfReader reader = new PdfReader(sourcePdf);
            PdfStamper stamper = PdfStamper.CreateSignature(reader, new FileStream(targetPdf, FileMode.Create), '\0');

            PdfSignatureAppearance signatureAppearance = stamper.SignatureAppearance;
            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;
            signatureAppearance.Reason = reason;
            signatureAppearance.LocationCaption = location;


            if (addVisibleSign)
            {
                int page = 1; //reader.NumberOfPages
                int posx = 100;
                int posy = 100;
                float width = 300;
                float height = 300;

                Image imageSignature = null;
                if (!String.IsNullOrEmpty(imageUrl))
                {
                    imageSignature = Image.GetInstance(imageUrl);

                    if (imageSignature != null)
                    {
                        //width = imageSignature.Width;
                        //height = imageSignature.Height;
                        imageSignature.Transparency = new int[] { 5 };
                        signatureAppearance.SignatureGraphic = imageSignature;
                    }
                }

                signatureAppearance.SetVisibleSignature(
                new Rectangle(posx, posy, width, height),
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
    }
}
