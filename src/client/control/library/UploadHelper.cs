using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

using LIB.Configuration;

namespace WebHard.WinCtrl.Library
{
    //=============================================================================================
    //
    //=============================================================================================
    /// <summary></summary>
    public class UploadHelper : System.IDisposable
    {
        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public UploadHelper()
        {
            infoTable = null;

            beginTime = DateTime.Now;
            statusMessage = String.Empty;
            percentProgrss = 0;

            numberOfFiles = 0;
            totalWriteSize = 0;
            totalFileLength = 0;

            fileNumber = 0;
            fileName = String.Empty;
            fileWriteSize = 0;
            fileLength = 0;
        }

        //=========================================================================================
        //
        //=========================================================================================
        public const int AVERAGE_COUNT = 5;
        public const int PREFERRED_TRANSFER_DURATION = 1500;

        //=========================================================================================
        //
        //=========================================================================================
        public int fileNumber;
        public int maxChunkSize;
        public int completeNumber;
        public int percentProgrss;
        public int numberOfFiles;

        public long fileLength;
        public long fileWriteSize;
        public long totalWriteSize;
        public long totalFileLength;

        public string fileName;
        public string fileGuid;
        public string statusMessage;

        public DateTime beginTime;
        public DateTime uploadDay;

        public DataTable infoTable;

        //=========================================================================================
        //
        //=========================================================================================
        private int m_numRetries = 0;
        private int m_maxRetries = 50;

        private string m_localFileHash = String.Empty;
        private string m_remoteFileHash = String.Empty;

        private Thread m_hashThread;

        private MainBox m_mainBox = null;

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        public Thread HashThread
        {
            get
            {
                return this.m_hashThread;
            }
            set
            {
                this.m_hashThread = value;
            }
        }

        /// <summary>
        /// guid, companyid, fileid, ftype, vsize, vtype, rname, title, description, wdate
        /// </summary>
        public DataRow InfoRow
        {
            get
            {
                return infoTable.Rows[1];
            }
        }

        /// <summary></summary>
        public DataSet InfoSet
        {
            get
            {
                DataSet infoset = new DataSet();
                infoset.Tables.Add(infoTable.Copy());

                return infoset;
            }
        }

        /// <summary></summary>
        public string LocalFileHash
        {
            get
            {
                return this.m_localFileHash;
            }
            set
            {
                this.m_localFileHash = value;
            }
        }

        /// <summary></summary>
        public MainBox MainBox
        {
            get
            {
                return this.m_mainBox;
            }
            set
            {
                this.m_mainBox = value;
            }
        }

        /// <summary></summary>
        public int MaxRetries
        {
            get
            {
                return this.m_maxRetries;
            }
            set
            {
                this.m_maxRetries = value;
            }
        }

        /// <summary></summary>
        public int NumRetries
        {
            get
            {
                return this.m_numRetries;
            }
            set
            {
                this.m_numRetries = value;
            }
        }

        /// <summary></summary>
        public string RemoteFileHash
        {
            get
            {
                return this.m_remoteFileHash;
            }
            set
            {
                this.m_remoteFileHash = value;
            }
        }

        //=========================================================================================
        // System.IDisposable ¸â¹ö ±¸Çö
        //=========================================================================================
        /// <summary></summary>
        public void Dispose()
        {
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        /// <returns></returns>
        public bool CheckFileHash()
        {
            bool result = false;

            // start calculating the local hash
            this.HashThread = new Thread(new ThreadStart(this.CheckLocalFileHash));
            this.HashThread.Start();

            // request the server hash
            this.RemoteFileHash = AppMediator.SINGLETON.CheckUploadFileHash(this.fileGuid);

            // wait for the local hash to complete
            this.HashThread.Join();

            if (this.LocalFileHash == this.RemoteFileHash)
                result = true;

            return result;
        }

        /// <summary></summary>
        /// <param name="p_extension"></param>
        /// <returns></returns>
        public string GetMIMEType(string p_extension)
        {
            return RegHelper.SNG.GetMIMEType(p_extension);
        }

        //=========================================================================================
        //
        //=========================================================================================
        /// <summary></summary>
        private void CheckLocalFileHash()
        {
            SHA1CryptoServiceProvider sha1Crypto = new SHA1CryptoServiceProvider();

            byte[] hashBytes;

            using (FileStream stream = new FileStream(this.fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
            {
                hashBytes = sha1Crypto.ComputeHash(stream);
            }

            this.LocalFileHash = BitConverter.ToString(hashBytes);
        }
    }
}