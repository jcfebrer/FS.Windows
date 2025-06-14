using System;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using static FSLibrary.Win32APIEnums;

namespace FSFormLibrary
{
    /// <summary>
    /// Librería multimedia
    /// </summary>
    public static class Multimedia
    {
        ///// <summary>
        ///// Clase Pitch
        ///// </summary>
        //public static class Pitch
        //{
        //    static string C = "0|2093";
        //    static string Csharp = "1|2217";
        //    static string Dflat = "1|2217";
        //    static string D = "2|2349";
        //    static string Dsharp = "3|2489";
        //    static string Eflat = "3|2489";
        //    static string E = "4|2637";
        //    static string F = "5|2794";
        //    static string Fsharp = "6|2960";
        //    static string Gflat = "6|2960";
        //    static string G = "7|3136";
        //    static string Gsharp = "8|3322";
        //    static string Aflat = "8|3322";
        //    static string A = "9|3520";
        //    static string Asharp = "10|3729";
        //    static string Bflat = "10|3729";
        //    static string B = "11|3951";
        //    static string Rest = "0|0";
        //}

        private static SoundPlayer keySound = null;
        private static string lastPlaySound = "";

        /// <summary>
        /// Plays the wav.
        /// </summary>
        /// <param name="fileName">The wav file.</param>
        public static void PlayWav(string fileName)
        {
            if (lastPlaySound == fileName)
                keySound.Play();
            else
                keySound = null;

            if (!File.Exists(fileName))
                throw new FileNotFoundException("El archivo de entrada no existe.", fileName);

                if (keySound == null)
            {
                using (keySound = new SoundPlayer(fileName))
                {
                    keySound.Play();
                }
            }
            else
                keySound.Play();

            lastPlaySound = fileName;
        }

        /// <summary>
        /// Plays the wav synchronize.
        /// </summary>
        /// <param name="fileName">The wav file.</param>
        public static void PlayWavSync(string fileName)
        {
            if (lastPlaySound == fileName)
                keySound.Play();
            else
                keySound = null;

            if (!File.Exists(fileName))
                throw new FileNotFoundException("El archivo de entrada no existe.", fileName);

            if (keySound == null)
            {
                using (keySound = new SoundPlayer(fileName))
                {
            keySound.PlaySync();
        }
            }
            else 
                keySound.PlaySync();

            lastPlaySound = fileName;
        }

        /// <summary>
        /// Play beep sound
        /// </summary>
        public static void PlayBeep()
        {
            SystemSounds.Beep.Play();
        }
        /// <summary>
        /// Play asterisk sound
        /// </summary>
        public static void PlayAsterik()
        {
            SystemSounds.Asterisk.Play();
        }
        /// <summary>
        /// Play exclamation sound
        /// </summary>
        public static void PlayExclamation()
        {
            SystemSounds.Exclamation.Play();
        }
        /// <summary>
        /// Play question sound
        /// </summary>
        public static void PlayQuestion()
        {
            SystemSounds.Question.Play();
        }
        /// <summary>
        /// Play hand sound
        /// </summary>
        public static void PlayHand()
        {
            SystemSounds.Hand.Play();
        }

        /// <summary>
        /// Beep de ZX Spectrum
        /// </summary>
        /// <param name="Duration"></param>
        /// <param name="pitch"></param>
        public static void BeepZx(double Duration, int pitch)
        {
            int p = 0;

            switch (pitch % 12)
            {
                case 0: p = 2093; break; //C
                case 1: p = 2217; break; //C#
                case 2: p = 2343; break; //D
                case 3: p = 2489; break; //D#
                case 4: p = 2637; break; //E
                case 5: p = 2794; break; //F
                case 6: p = 2960; break; //G#
                case 7: p = 3136; break; //G
                case 8: p = 3322; break; //F#
                case 9: p = 3520; break; //A
                case 10: p = 3729; break; //A#
                case 11: p = 3951; break; //B
                default: p = 2093; break; //C
            }

            Console.Beep(p, (int)(Duration * 1000));
        }


        /// <summary>
        /// Beeps the specified frequency.
        /// </summary>
        /// <param name="Frequency">The frequency.</param>
        /// <param name="Duration">The duration.</param>
        public static void Beep(int Frequency, int Duration)
        {
            Beep(1, Frequency, Duration);
        }

        /// <summary>
        /// Beeps the specified amplitude.
        /// </summary>
        /// <param name="Amplitude">The amplitude.</param>
        /// <param name="Frequency">The frequency.</param>
        /// <param name="Duration">The duration.</param>
        public static void Beep(int Amplitude, int Frequency, int Duration)
        {
            double A = ((Amplitude * 32768.0) / 1000.0) - 1.0;
            double DeltaFT = (2 * Math.PI * Frequency) / 44100.0;
            int Samples = (44100 * Duration) / 1000;

            Console.WriteLine(Samples);
            int Bytes = Samples * 4;
            int[] Hdr = new int[] { 0x46464952, 0x24 + Bytes, 0x45564157, 0x20746d66, 0x10, 0x20001, 0xac44, 0x2b110, 0x100004, 0x61746164, Bytes };

            using (MemoryStream MS = new MemoryStream(0x24 + Bytes))
            {
                using (BinaryWriter BW = new BinaryWriter(MS))
                {
                    int length = Hdr.Length - 1;
                    for (int I = 0; I <= length; I++)
                    {
                        BW.Write(Hdr[I]);
                    }
                    int sample = Samples - 1;
                    for (int T = 0; T <= sample; T++)
                    {
                        short Sample = (short)Math.Round((double)(A * Math.Sin(DeltaFT * T)));
                        BW.Write(Sample);
                        BW.Write(Sample);
                    }
                    BW.Flush();
                    MS.Seek(0L, SeekOrigin.Begin);
                    using (SoundPlayer SP = new SoundPlayer(MS))
                    {
                        SP.PlaySync();
                    }
                }
            }
        }

        /// <summary>
        /// Extrae un fichero wav del fichero wav original desde la posición indicada en ms y la duración indicada.
        /// </summary>
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <param name="startMilliseconds"></param>
        /// <param name="durationMilliseconds"></param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void SplitWav(string inputFilePath, string outputFilePath, int startMilliseconds, int durationMilliseconds)
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException("El archivo de entrada no existe.", inputFilePath);

            if(Path.GetExtension(inputFilePath).ToLower() != ".wav")
                throw new Exception("Solo se permiten ficheros con extensión WAV. Fichero: " + inputFilePath);

            using (var inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
            using (var binaryReader = new BinaryReader(inputStream))
            using (var outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
            using (var binaryWriter = new BinaryWriter(outputStream))
            {
                // Leer el encabezado WAV
                byte[] header = binaryReader.ReadBytes(44);

                // Extraer información clave del encabezado
                int sampleRate = BitConverter.ToInt32(header, 24); // SampleRate
                short numChannels = BitConverter.ToInt16(header, 22); // NumChannels
                short bitsPerSample = BitConverter.ToInt16(header, 34); // BitsPerSample
                int byteRate = BitConverter.ToInt32(header, 28); // ByteRate
                short blockAlign = BitConverter.ToInt16(header, 32); // BlockAlign

                // Calcular posiciones en bytes
                int bytesPerMillisecond = byteRate / 1000;
                long startPosition = 44 + startMilliseconds * bytesPerMillisecond;
                long durationBytes = durationMilliseconds * bytesPerMillisecond;

                // Asegurar que las posiciones estén dentro del rango
                if (startPosition >= inputStream.Length)
                    throw new ArgumentOutOfRangeException(nameof(startMilliseconds), "El tiempo de inicio está fuera del rango del archivo.");
                if (startPosition + durationBytes > inputStream.Length)
                    durationBytes = inputStream.Length - startPosition;

                // Ajustar el tamaño de Subchunk2 y ChunkSize en el encabezado
                int subchunk2Size = (int)durationBytes;
                int chunkSize = 36 + subchunk2Size;

                Array.Copy(BitConverter.GetBytes(chunkSize), 0, header, 4, 4); // ChunkSize
                Array.Copy(BitConverter.GetBytes(subchunk2Size), 0, header, 40, 4); // Subchunk2Size

                // Escribir el encabezado modificado al archivo de salida
                binaryWriter.Write(header);

                // Mover al inicio de los datos
                inputStream.Seek(startPosition, SeekOrigin.Begin);

                // Leer y escribir los datos de audio
                byte[] buffer = new byte[1024];
                long bytesToRead = durationBytes;

                while (bytesToRead > 0)
                {
                    int bytesRead = inputStream.Read(buffer, 0, (int)Math.Min(buffer.Length, bytesToRead));
                    if (bytesRead == 0) break;

                    binaryWriter.Write(buffer, 0, bytesRead);
                    bytesToRead -= bytesRead;
                }
            }
    }

    /// <summary>
        /// Genera un fichero WAV con el Rate, duración y frecuencia indicada.
    /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sampleRate"></param>
        /// <param name="durationMs"></param>
        /// <param name="frequency"></param>
        public static void GenerateWav(string filePath, int sampleRate, int durationMs, int frequency)
        {
            int samples = sampleRate * durationMs / 1000;
            short amplitude = 3000; // Amplitud del sonido
            byte[] wavData = new byte[44 + samples * 2];

            // Cabecera WAV
            using (var stream = new MemoryStream(wavData))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(new[] { 'R', 'I', 'F', 'F' });
                writer.Write(36 + samples * 2); // ChunkSize
                writer.Write(new[] { 'W', 'A', 'V', 'E' });
                writer.Write(new[] { 'f', 'm', 't', ' ' });
                writer.Write(16); // Subchunk1Size
                writer.Write((short)1); // AudioFormat (PCM)
                writer.Write((short)1); // NumChannels (Mono)
                writer.Write(sampleRate); // SampleRate
                writer.Write(sampleRate * 2); // ByteRate
                writer.Write((short)2); // BlockAlign
                writer.Write((short)16); // BitsPerSample
                writer.Write(new[] { 'd', 'a', 't', 'a' });
                writer.Write(samples * 2); // Subchunk2Size

                // Datos de audio
                for (int i = 0; i < samples; i++)
                {
                    double t = (double)i / sampleRate;
                    short sample = (short)(amplitude * Math.Sin(2 * Math.PI * frequency * t));
                    writer.Write(sample);
                }
            }

            File.WriteAllBytes(filePath, wavData);
    }

        /// <summary>
        /// Plays the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="sync">Modo sincronizado true/false</param>
        public static void PlaySound(string fileName, bool sync = false)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("El archivo de entrada no existe.", fileName);

            SoundFlags syncSound;
            if(sync)
                syncSound = SoundFlags.SND_SYNC;
            else
                syncSound = SoundFlags.SND_ASYNC;

            FSLibrary.Win32API.PlaySound(fileName, IntPtr.Zero,
                   (int)(SoundFlags.SND_FILENAME | syncSound));
    }
}
}