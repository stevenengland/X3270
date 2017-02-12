namespace StEn.X3270.Rest.Types
{
    using System;
    using System.IO;

    using StEn.X3270.Rest.Types.Enums;

    /// <summary>
    /// Represents information for a file to be sent
    /// </summary>
    public struct FileToSend
    {
        /// <summary>
        /// Gets or sets the file name for uploaded file.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the file content for uploaded file.
        /// </summary>
        public Stream Content { get; set; }

        /// <summary>
        /// Gets or sets the file uri.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the file ID
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Gets the type of file to send.
        /// </summary>
        public FileType Type
        {
            get
            {
                if (this.Content != null) return FileType.Stream;
                if (this.FileId != null) return FileType.Id;
                return this.Url != null ? FileType.Url : FileType.Unknown;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileToSend"/> struct. 
        /// </summary>
        /// <param name="filename">
        /// The <see cref="Filename"/>.
        /// </param>
        /// <param name="content">
        /// The <see cref="Content"/>.
        /// </param>
        public FileToSend(string filename, Stream content)
        {
            this.Filename = filename;
            this.Content = content;

            this.Url = null;
            this.FileId = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileToSend"/> struct. 
        /// </summary>
        /// <param name="url">
        /// Url of the File to send
        /// </param>
        public FileToSend(Uri url)
        {
            this.Url = url;

            this.Filename = null;
            this.Content = null;
            this.FileId = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileToSend"/> struct. 
        /// </summary>
        /// <param name="fileId">
        /// The ID of the file to send.
        /// </param>
        public FileToSend(string fileId)
        {
            this.FileId = fileId;

            this.Filename = null;
            this.Content = null;
            this.Url = null;
        }
    }
}
