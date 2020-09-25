using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ImgContainer
{


    class Core
    {
        public static string GetSize(long fileLength)
        {
            double length = fileLength;
            if (length < 1024 * 0.9)
            {
                return String.Format("{0:d} B", length);
            }

            length /= 1024;
            if (length < 1024 * 0.9)
            {
                return String.Format("{0:f2} KB", length);
            }

            length /= 1024;
            if (length < 1024 * 0.9)
            {
                return String.Format("{0:f2} MB", length);
            }

            length /= 1024;
            if (length < 1024 * 0.9)
            {
                return String.Format("{0:f2} GB", length);
            }

            return String.Format("{0:f2} TB", length / 1024);
        }

        public static string GetVolume(Image img)
        {
            return GetSize(img.Width * img.Height / 4 * 3);
        }

        public static bool Check(Image src, FileInfo target)
        {
            int length = (int)target.Length + 4 + System.Text.Encoding.UTF8.GetBytes(target.Name).Length;
            length = length / 3 * 3 + 3; // 3字节对齐

            return src.Width * src.Height / 4 * 3 > length;
        }

        public static byte[] int2bytes(int src) // Big
        {
            byte[] result = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = (byte)(src & 0b11111111);
                src >>= 8;
            }
            return result;
        }

        public static Image Combine(Image src, FileInfo target, string targetFilename)
        {
            Bitmap result = new Bitmap((Image)src.Clone());
            byte[] targetFilenameBytes = System.Text.Encoding.UTF8.GetBytes(targetFilename);
            int fileLength = (int)target.Length;

            int bufferSize = targetFilenameBytes.Length + 4 + fileLength;
            bufferSize = bufferSize / 3 * 3 + 3;
            byte[] writeBuffer = new byte[bufferSize];
            writeBuffer[0] = (byte)targetFilenameBytes.Length;
            Array.Copy(targetFilenameBytes, 0, writeBuffer, 1, targetFilenameBytes.Length);
            Array.Copy(int2bytes(fileLength), 0, writeBuffer, targetFilenameBytes.Length + 1, 4);
            FileStream fs = new FileStream(target.FullName, FileMode.Open ,FileAccess.Read);
            fs.Read(writeBuffer, targetFilenameBytes.Length + 5, fileLength);
            fs.Close();

            int baseX = 0, baseY = 0;
            for(int i = 0; i < writeBuffer.Length; i += 3)
            {
                // Core
                int tmpByte = 0;
                Color tmpColor = result.GetPixel(baseX, baseY);
                tmpByte += tmpColor.A;

                tmpByte <<= 8;
                tmpByte += tmpColor.R & 0b11111100;
                tmpByte += writeBuffer[i] & 0b00000011;
                writeBuffer[i] >>= 2;

                tmpByte <<= 8;
                tmpByte += tmpColor.G & 0b11111100;
                tmpByte += writeBuffer[i] & 0b00000011;
                writeBuffer[i] >>= 2;

                tmpByte <<= 8;
                tmpByte += tmpColor.B & 0b11111100;
                tmpByte += writeBuffer[i] & 0b00000011;
                writeBuffer[i] >>= 2;

                result.SetPixel(baseX, baseY, Color.FromArgb(tmpByte));
                baseX++;
                if (baseX >= result.Width)
                {
                    baseY++;
                    baseX = 0;
                }

                //
                tmpByte = 0;
                tmpColor = result.GetPixel(baseX, baseY);
                tmpByte += tmpColor.A;

                tmpByte <<= 8;
                tmpByte += tmpColor.R & 0b11111100;
                tmpByte += writeBuffer[i] & 0b00000011;

                tmpByte <<= 8;
                tmpByte += tmpColor.G & 0b11111100;
                tmpByte += writeBuffer[i + 1] & 0b00000011;
                writeBuffer[i + 1] >>= 2;

                tmpByte <<= 8;
                tmpByte += tmpColor.B & 0b11111100;
                tmpByte += writeBuffer[i + 1] & 0b00000011;
                writeBuffer[i + 1] >>= 2;

                result.SetPixel(baseX, baseY, Color.FromArgb(tmpByte));
                baseX++;
                if (baseX >= result.Width)
                {
                    baseY++;
                    baseX = 0;
                }

                //
                tmpByte = 0;
                tmpColor = result.GetPixel(baseX, baseY);
                tmpByte += tmpColor.A;

                tmpByte <<= 8;
                tmpByte += tmpColor.R & 0b11111100;
                tmpByte += writeBuffer[i + 1] & 0b00000011;
                writeBuffer[i + 1] >>= 2;

                tmpByte <<= 8;
                tmpByte += tmpColor.G & 0b11111100;
                tmpByte += writeBuffer[i + 1] & 0b00000011;

                tmpByte <<= 8;
                tmpByte += tmpColor.B & 0b11111100;
                tmpByte += writeBuffer[i + 2] & 0b00000011;
                writeBuffer[i + 2] >>= 2;

                result.SetPixel(baseX, baseY, Color.FromArgb(tmpByte));
                baseX++;
                if (baseX >= result.Width)
                {
                    baseY++;
                    baseX = 0;
                }

                //
                tmpByte = 0;
                tmpColor = result.GetPixel(baseX, baseY);
                tmpByte += tmpColor.A;

                tmpByte <<= 8;
                tmpByte += tmpColor.R & 0b11111100;
                tmpByte += writeBuffer[i + 2] & 0b00000011;
                writeBuffer[i + 2] >>= 2;

                tmpByte <<= 8;
                tmpByte += tmpColor.G & 0b11111100;
                tmpByte += writeBuffer[i + 2] & 0b00000011;
                writeBuffer[i + 2] >>= 2;

                tmpByte <<= 8;
                tmpByte += tmpColor.B & 0b11111100;
                tmpByte += writeBuffer[i + 2] & 0b00000011;

                result.SetPixel(baseX, baseY, Color.FromArgb(tmpByte));
                baseX++;
                if (baseX >= result.Width)
                {
                    baseY++;
                    baseX = 0;
                }
            }

            return result;
        }

        private static byte[] GetBytesFromImage(Image img, int offset, int count)
        {
            int pre = offset % 3;
            int post = 3 - (pre + count) % 3;
            if(post == 3)
            {
                post = 0;
            }
            byte[] buffer = new byte[pre + count + post];

            Bitmap src = new Bitmap(img);
            int prePx = (offset - pre) / 3 * 4;
            int baseX = prePx % img.Width;
            int baseY = prePx / img.Width;
            for(int i = 0; i < buffer.Length; i += 3)
            {
                Color tmpColor = src.GetPixel(baseX, baseY);
                baseX++;
                if (baseX >= src.Width)
                {
                    baseY++;
                    baseX = 0;
                }

                int tmpByte = 0;
                tmpByte += tmpColor.R & 0b00000011;
                tmpByte += (tmpColor.G & 0b00000011) << 2;
                tmpByte += (tmpColor.B & 0b00000011) << 4;

                //
                tmpColor = src.GetPixel(baseX, baseY);
                baseX++;
                if (baseX >= src.Width)
                {
                    baseY++;
                    baseX = 0;
                }

                tmpByte += (tmpColor.R & 0b00000011) << 6;
                buffer[i] = (byte)tmpByte;

                tmpByte = 0;
                tmpByte += tmpColor.G & 0b00000011;
                tmpByte += (tmpColor.B & 0b00000011) << 2;

                //
                tmpColor = src.GetPixel(baseX, baseY);
                baseX++;
                if (baseX >= src.Width)
                {
                    baseY++;
                    baseX = 0;
                }

                tmpByte += (tmpColor.R & 0b00000011) << 4;
                tmpByte += (tmpColor.G & 0b00000011) << 6;
                buffer[i + 1] = (byte)tmpByte;

                tmpByte = 0;
                tmpByte += tmpColor.B & 0b00000011;

                //
                tmpColor = src.GetPixel(baseX, baseY);
                baseX++;
                if (baseX >= src.Width)
                {
                    baseY++;
                    baseX = 0;
                }

                tmpByte += (tmpColor.R & 0b00000011) << 2;
                tmpByte += (tmpColor.G & 0b00000011) << 4;
                tmpByte += (tmpColor.B & 0b00000011) << 6;
                buffer[i + 2] = (byte)tmpByte;
            }

            byte[] result = new byte[count];
            Array.Copy(buffer, pre, result, 0, count);
            return result;
        }

        public static string GetFilename(Image src)
        {
            Bitmap srcImage = new Bitmap(src);
            int filenameCount = GetBytesFromImage(src, 0, 1)[0];
            if(filenameCount >= src.Width * src.Height / 4 * 3)
            {
                return null;
            }
            
            try
            {
                return System.Text.Encoding.UTF8.GetString(GetBytesFromImage(src, 1, filenameCount));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static byte[] GetData(Image src)
        {
            Bitmap srcImage = new Bitmap(src);
            int offset = GetBytesFromImage(src, 0, 1)[0] + 1;

            byte[] tmp = GetBytesFromImage(src, offset, 4);
            int dataLength = tmp[3];
            for(int i = 2; i >= 0; i--)
            {
                dataLength <<= 8;
                dataLength += tmp[i];
            }
            offset += 4;

            if(offset + dataLength > src.Width * src.Height / 4 * 3)
            {
                return null;
            }

            return GetBytesFromImage(src, offset, dataLength);
        }
    }
}
