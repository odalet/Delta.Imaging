using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Goheer.EXIF;

namespace Delta.ImageRenameTool
{
    internal class FileRenameInfo
    {
        private string guessedDescription = null;
        private string inputDescription = null;
        private DateTime cacheExifDateTime = DateTime.MinValue;
        private bool exifDateTimeRetrieved = false;
        private int indexPaddingCount = Properties.Settings.Default.IndexPaddingCount;
        private string file = string.Empty;
        private FileInfo info = null;
        private EXIFextractor exif = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileRenameInfo"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public FileRenameInfo(string fileName) :
            this(fileName, Properties.Settings.Default.IndexPaddingCount)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileRenameInfo"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="paddingCount">The padding count.</param>
        public FileRenameInfo(string fileName, int paddingCount)
        {
            file = fileName;
            try
            {
                exif = new EXIFextractor(fileName, "\r\n", string.Empty);
            }
            catch (Exception ex)
            {
                // no exif data?
                var debugException = ex;
            }

            info = new FileInfo(file);
            indexPaddingCount = paddingCount;
            Selected = true;
        }

        /// <summary>
        /// Gets the name of the original file.
        /// </summary>
        /// <value>The name of the original file.</value>
        public string OriginalFileName
        {
            get { return Path.GetFileName(file); }
        }

        /// <summary>
        /// Gets the file update time (not the photo datetime).
        /// </summary>
        /// <value>The update time.</value>
        public DateTime UpdateTime
        {
            get { return info.LastWriteTime; }
        }

        /// <summary>
        /// Gets the file creation time.
        /// </summary>
        /// <value>The creation time.</value>
        public DateTime FileCreationTime
        {
            get { return info.CreationTime; }
        }

        /// <summary>
        /// Gets the photo creation time.
        /// </summary>
        /// <value>The photo time.</value>
        public DateTime PhotoTime
        {
            get
            {
                var exifDateTime = GetExifDateTime();
                if (exifDateTime == DateTime.MinValue || exifDateTime == DateTime.MaxValue) // Let's default to the file time
                    return FileCreationTime;
                else return exifDateTime;
            }
        }

        public EXIFextractor Exif
        {
            get { return exif; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FileRenameInfo"/> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get
            {
                return inputDescription == null ?
                    GuessDescription(file) : inputDescription;
            }
            set { inputDescription = value; }
        }

        /// <summary>
        /// Gets or sets the new name of the file.
        /// </summary>
        /// <value>The new name of the file.</value>
        public string NewFileName { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public string Result { get; set; }

        private string Pattern
        {
            get
            {
                var padding = new string('0', indexPaddingCount);
                return "{0:" + padding + "} - {1} - {2}{3}";
            }
        }

        public string GetFileName() { return file; }

        public void CreateNewName(int index)
        {
            var guessedDescription = GuessDescription(file);
            var description = string.IsNullOrEmpty(Description) ?
                guessedDescription : Description;

            NewFileName = string.Format(Pattern,
                index,
                PhotoTime.ToString("yyyyMMdd"),
                description,
                Path.GetExtension(file));
            Result = string.Empty;
        }

        public void Rename()
        {
            try
            {
                var newFile = Path.Combine(Path.GetDirectoryName(file), NewFileName);
                File.Move(file, newFile);

                file = newFile;
                NewFileName = string.Empty;
                Result = "OK";
            }
            catch (Exception ex)
            {
                Result = string.Format("KO: {0}", ex.Message);
            }
        }

        private DateTime GetExifDateTime()
        {
            if (exifDateTimeRetrieved) return cacheExifDateTime;

            exifDateTimeRetrieved = true;
            cacheExifDateTime = DateTime.MinValue;
            if (exif == null) return cacheExifDateTime;

            try
            {
                var prop = exif.Cast<Pair>().FirstOrDefault(t => (string)t.First == "DTDigitized");
                if (prop == null)
                    prop = exif.Cast<Pair>().FirstOrDefault(t => (string)t.First == "DTOrig");
                if (prop == null) // Use Date Time as a last resort because it gest modified when image is copied (at least in Win8)
                    prop = exif.Cast<Pair>().FirstOrDefault(t => (string)t.First == "Date Time");
                if (prop != null)
                {
                    var value = (string)prop.Second;
                    cacheExifDateTime = DateTime.ParseExact(value, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                var debugException = ex;
            }

            return cacheExifDateTime;
        }

        private string GuessDescription(string filename)
        {
            if (guessedDescription != null) return guessedDescription;

            // If the filename has this form:
            // {0} - {1} - {2}.{3}
            // with 0 = index, 1 = photo date, 2 = description, 3 = extension
            // Then we should be able to retrieve the description.

            var name = Path.GetFileNameWithoutExtension(file);
            if (string.IsNullOrEmpty(name)) return string.Empty;

            // Now name should be {0} - {1} - {2}; let's split on dashes
            var parts = name.Split('-');

            // Because {2} may contain dashes, we don't retrieve the last part, 
            // but from the third one up to the end (if there are at least 3 parts)
            if (parts.Length >= 3)
                guessedDescription = string.Join("-", parts.Skip(2).ToArray()).Trim();
            else guessedDescription = parts[parts.Length - 1].Trim(); // Revert to grabbing the last part

            return guessedDescription;
        }
    }
}
