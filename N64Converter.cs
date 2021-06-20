using System;
using System.Collections.Generic;
using System.IO;

namespace RomConverter
{
    public class N64 : IRomConverter
    {
        enum RomType
        {
            n64, z64, v64, Unknown
        }

        public N64()
        {
        }

        private RomType GetRomFormat(string romHeader)
        {
            Console.WriteLine($"GetRomFormat: {romHeader}");
            switch (romHeader)
            {
                case "40-12-37-80":
                    return RomType.n64;
                case "80-37-12-40":
                    return RomType.z64;
                case "37-80-40-12":
                    return RomType.v64;
                default:
                    return RomType.Unknown;
            }
        }

        private byte[] ReadFullBytes(string inputFilePath)
        {
            return File.ReadAllBytes(inputFilePath);
        }

        private byte[] ReadFirstBytes(string inputFilePath, short bytesCount)
        {
            byte[] buffer = new byte[bytesCount];
            try
            {
                using (FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                {
                    var bytes_read = fs.Read(buffer, 0, buffer.Length);
                    fs.Close();

                    if (bytes_read != buffer.Length)
                    {
                        return null;
                    }
                    return buffer;
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        private bool WriteBytesToPath(string outputPathFile, byte[] bytes)
        {
            bool result = true;
            try
            {
                File.WriteAllBytes(outputPathFile, bytes);
                /*using (FileStream fs = new FileStream(outputPathFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }*/
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// For V64 to Z64
        /// </summary>
        /// <param name=""></param>
        private byte[] SwapWord(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i += 2)
                SwapWord(bytes, i, i + 1);

            return bytes;
        }

        private byte[] SwapWord(byte[] bytes, int a, int b)
        {
            byte temp = bytes[a];
            bytes[a] = bytes[b];
            bytes[b] = temp;
            //(bytes[a], bytes[b]) = (bytes[b], bytes[a]);

            return bytes;
        }

        /// <summary>
        /// For N64 to Z64
        /// </summary>
        /// <param name=""></param>
        private byte[] SwapWordD(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i += 4)
            {
                SwapWord(bytes, i, i + 3);
                SwapWord(bytes, i + 1, i + 2);
            }

            return bytes;
        }

        public bool Convert(string inputPath, string outputFilePath)
        {
            //check path
            //check rom format
            try
            {
                byte[] fullBytes = ReadFullBytes(inputPath);
                byte[] bytes = ReadFirstBytes(inputPath, 4);
                Console.WriteLine($"FULL: {fullBytes[0]}:{fullBytes[1]}:{fullBytes[2]}:{fullBytes[3]}");
                Console.WriteLine($"NOFU: {bytes[0]}:{bytes[1]}:{bytes[2]}:{bytes[3]}");
                if (bytes == null)
                {
                    //unable to read file for some reason or file is shoter than 4 bytes
                    return false;
                }
                RomType romType = GetRomFormat(BitConverter.ToString(bytes));
                switch (romType)
                {
                    case RomType.Unknown:
                    default:
                        return false;
                    case RomType.z64:
                        //just copy file rom
                        break;
                    case RomType.n64:
                        fullBytes = SwapWordD(fullBytes);
                        break;
                    case RomType.v64:
                        fullBytes = SwapWord(fullBytes);
                        break;
                }
                //return WriteBytesToPath(Path.Combine(outputPath, $"{Path.GetFileNameWithoutExtension(inputPath)}_temp.z64"), bytes);
                return WriteBytesToPath(outputFilePath, fullBytes);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
