using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace CKGenUpdater
{
    public class ZipHelp
    {
        /// <summary>
        /// Zip a file
        /// </summary>
        /// <param name="SrcFile">source file path</param>
        /// <param name="DstFile">zipped file path</param>
        /// <param name="BufferSize">buffer to use</param>
        public static void Zip(string SrcFile, string DstFile, int BufferSize = 1024)
        {
            FileStream fileStreamIn = new FileStream(SrcFile, FileMode.Open, FileAccess.Read);
            FileStream fileStreamOut = new FileStream(DstFile, FileMode.Create, FileAccess.Write);
            ZipOutputStream zipOutStream = new ZipOutputStream(fileStreamOut);

            byte[] buffer = new byte[BufferSize];

            ZipEntry entry = new ZipEntry(Path.GetFileName(SrcFile));
            zipOutStream.PutNextEntry(entry);

            int size;
            do
            {
                size = fileStreamIn.Read(buffer, 0, buffer.Length);
                zipOutStream.Write(buffer, 0, size);
            } while (size > 0);

            zipOutStream.Close();
            fileStreamOut.Close();
            fileStreamIn.Close();
        }

        /// <summary>
        /// UnZip a file
        /// </summary>
        /// <param name="SrcFile">source file path</param>
        /// <param name="DstFile">unzipped file path</param>
        /// <param name="BufferSize">buffer to use</param>
        public static void UnZip(string SrcFile, string DstFile)
        {
            FastZip fastZip = new FastZip();
            fastZip.ExtractZip(SrcFile, DstFile, "");//@"ICSharpCode.SharpZipLib.dll"
        }
    }
}
