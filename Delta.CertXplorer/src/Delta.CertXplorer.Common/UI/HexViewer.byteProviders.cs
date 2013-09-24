using System;
using System.IO;

namespace Delta.CertXplorer.UI
{
    partial class HexViewer
    {
        /// <summary>
        /// Defines a byte provider for HexBox control
        /// </summary>
        internal interface IByteProvider
        {
            /// <summary>
            /// Occurs, when the Length property changed.
            /// </summary>
            event EventHandler LengthChanged;

            /// <summary>
            /// Occurs, when bytes are changed.
            /// </summary>
            event EventHandler Changed;

            /// <summary>
            /// Returns the total length of bytes the byte provider is providing.
            /// </summary>
            /// <value>The length.</value>
            long Length { get; }

            /// <summary>
            /// Returns a value if the WriteByte methods is supported by the provider.
            /// </summary>
            /// <value>True, when it´s supported.</value>
            bool SupportsWriteByte { get; }

            /// <summary>
            /// Returns a value if the InsertBytes methods is supported by the provider.
            /// </summary>
            /// <value>True, when it´s supported.</value>
            bool SupportsInsertBytes { get; }

            /// <summary>
            /// Returns a value if the DeleteBytes methods is supported by the provider.
            /// </summary>
            /// <value>True, when it´s supported.</value>
            bool SupportsDeleteBytes { get; }

            /// <summary>
            /// True, when changes are done.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
            /// </value>
            bool HasChanges { get; }

            /// <summary>
            /// Reads a byte from the provider
            /// </summary>
            /// <param name="index">the index of the byte to read</param>
            /// <returns>the byte to read</returns>
            byte ReadByte(long index);

            /// <summary>
            /// Writes a byte into the provider
            /// </summary>
            /// <param name="index">the index of the byte to write</param>
            /// <param name="value">the byte to write</param>
            void WriteByte(long index, byte value);

            /// <summary>
            /// Inserts bytes into the provider
            /// </summary>
            /// <param name="index"></param>
            /// <param name="bs"></param>
            /// <remarks>This method must raise the LengthChanged event.</remarks>
            void InsertBytes(long index, byte[] bs);

            /// <summary>
            /// Deletes bytes from the provider
            /// </summary>
            /// <param name="index">the start index of the bytes to delete</param>
            /// <param name="length">the length of the bytes to delete</param>
            /// <remarks>This method must raise the LengthChanged event.</remarks>
            void DeleteBytes(long index, long length);

            /// <summary>
            /// Applies changes.
            /// </summary>
            void ApplyChanges();
        }

        internal class ByteArrayProvider : IByteProvider
        {
            /// <summary>
            /// Gets or sets the content.
            /// </summary>
            /// <value>The content.</value>
            public byte[] Content { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="ByteArrayProvider"/> class.
            /// </summary>
            /// <param name="content">The content.</param>
            public ByteArrayProvider(byte[] content)
            {
                Content = content;
            }

            #region IByteProvider Members

            /// <summary>
            /// Returns the total length of bytes the byte provider is providing.
            /// </summary>
            /// <value>The length.</value>
            public long Length
            {
                get { return Content.Length; }
            }

            /// <summary>
            /// True, when changes are done.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
            /// </value>
            public bool HasChanges { get { return false; } }

            /// <summary>
            /// Returns a value if the WriteByte methods is supported by the provider.
            /// </summary>
            /// <value>True, when it´s supported.</value>
            public bool SupportsWriteByte { get { return false; } }

            /// <summary>
            /// Returns a value if the InsertBytes methods is supported by the provider.
            /// </summary>
            /// <value>True, when it´s supported.</value>
            public bool SupportsInsertBytes { get { return false; } }

            /// <summary>
            /// Returns a value if the DeleteBytes methods is supported by the provider.
            /// </summary>
            /// <value>True, when it´s supported.</value>
            public bool SupportsDeleteBytes { get { return false; } }

            /// <summary>
            /// Reads a byte from the provider
            /// </summary>
            /// <param name="index">the index of the byte to read</param>
            /// <returns>the byte to read</returns>
            public byte ReadByte(long index) { return Content[index]; }

            /// <summary>
            /// Never used directly.
            /// </summary>
#pragma warning disable 67
            public event EventHandler LengthChanged;
#pragma warning restore 67


            /// <summary>
            /// Never used directly.
            /// </summary>
#pragma warning disable 67
            public event EventHandler Changed;
#pragma warning restore 67

            /// <summary>
            /// Writes a byte into the provider
            /// </summary>
            /// <param name="index">the index of the byte to write</param>
            /// <param name="value">the byte to write</param>
            public void WriteByte(long index, byte value)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Inserts bytes into the provider
            /// </summary>
            /// <param name="index"></param>
            /// <param name="bs"></param>
            /// <remarks>This method must raise the LengthChanged event.</remarks>
            public void InsertBytes(long index, byte[] bs)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Deletes bytes from the provider
            /// </summary>
            /// <param name="index">the start index of the bytes to delete</param>
            /// <param name="length">the length of the bytes to delete</param>
            /// <remarks>This method must raise the LengthChanged event.</remarks>
            public void DeleteBytes(long index, long length)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Applies changes.
            /// </summary>
            public void ApplyChanges()
            {
                throw new NotSupportedException();
            }

            #endregion
        }

        #region other providers

        /// <summary>
        /// Represents a collection of bytes.
        /// </summary>
        internal class ByteCollection : System.Collections.CollectionBase
        {
            /// <summary>
            /// Initializes a new instance of ByteCollection class.
            /// </summary>
            public ByteCollection()
            { }

            /// <summary>
            /// Initializes a new instance of ByteCollection class.
            /// </summary>
            /// <param name="bs">an array of bytes to add to collection</param>
            public ByteCollection(byte[] bs)
            { AddRange(bs); }

            /// <summary>
            /// Gets or sets the value of a byte
            /// </summary>
            public byte this[int index]
            {
                get { return (byte)List[index]; }
                set { List[index] = value; }
            }

            /// <summary>
            /// Adds a byte into the collection.
            /// </summary>
            /// <param name="b">the byte to add</param>
            public void Add(byte b)
            { List.Add(b); }

            /// <summary>
            /// Adds a range of bytes to the collection.
            /// </summary>
            /// <param name="bs">the bytes to add</param>
            public void AddRange(byte[] bs)
            { InnerList.AddRange(bs); }

            /// <summary>
            /// Removes a byte from the collection.
            /// </summary>
            /// <param name="b">the byte to remove</param>
            public void Remove(byte b)
            { List.Remove(b); }

            /// <summary>
            /// Removes a range of bytes from the collection.
            /// </summary>
            /// <param name="index">the index of the start byte</param>
            /// <param name="count">the count of the bytes to remove</param>
            public void RemoveRange(int index, int count)
            { InnerList.RemoveRange(index, count); }

            /// <summary>
            /// Inserts a range of bytes to the collection.
            /// </summary>
            /// <param name="index">the index of start byte</param>
            /// <param name="bs">an array of bytes to insert</param>
            public void InsertRange(int index, byte[] bs)
            { InnerList.InsertRange(index, bs); }

            /// <summary>
            /// Gets all bytes in the array
            /// </summary>
            /// <returns>an array of bytes.</returns>
            public byte[] GetBytes()
            {
                byte[] bytes = new byte[Count];
                InnerList.CopyTo(0, bytes, 0, bytes.Length);
                return bytes;
            }

            /// <summary>
            /// Inserts a byte to the collection.
            /// </summary>
            /// <param name="index">the index</param>
            /// <param name="b">a byte to insert</param>
            public void Insert(int index, byte b)
            {
                InnerList.Insert(index, b);
            }

            /// <summary>
            /// Returns the index of the given byte.
            /// </summary>
            public int IndexOf(byte b)
            {
                return InnerList.IndexOf(b);
            }

            /// <summary>
            /// Returns true, if the byte exists in the collection.
            /// </summary>
            public bool Contains(bool b)
            {
                return InnerList.Contains(b);
            }

            /// <summary>
            /// Copies the content of the collection into the given array.
            /// </summary>
            public void CopyTo(byte[] bs, int index)
            {
                InnerList.CopyTo(bs, index);
            }

            /// <summary>
            /// Copies the content of the collection into an array.
            /// </summary>
            /// <returns>the array containing all bytes.</returns>
            public byte[] ToArray()
            {
                byte[] data = new byte[this.Count];
                this.CopyTo(data, 0);
                return data;
            }
        }

        /// <summary>
        /// Byte provider for a small amount of data.
        /// </summary>
        internal class DynamicProvider : IByteProvider
        {
            /// <summary>
            /// Contains information about changes.
            /// </summary>
            private bool dirty = false;

            /// <summary>
            /// Contains a byte collection.
            /// </summary>
            private ByteCollection bytes = null;

            /// <summary>
            /// Initializes a new instance of the DynamicByteProvider class.
            /// </summary>
            /// <param name="data"></param>
            public DynamicProvider(byte[] data)
                : this(new ByteCollection(data)) { }

            /// <summary>
            /// Initializes a new instance of the DynamicByteProvider class.
            /// </summary>
            /// <param name="byteCollection"></param>
            public DynamicProvider(ByteCollection byteCollection)
            {
                bytes = byteCollection;
            }

            /// <summary>
            /// Raises the Changed event.
            /// </summary>
            private void OnChanged(EventArgs e)
            {
                dirty = true;

                if (Changed != null) Changed(this, e);
            }

            /// <summary>
            /// Raises the LengthChanged event.
            /// </summary>
            private void OnLengthChanged(EventArgs e)
            {
                if (LengthChanged != null) LengthChanged(this, e);
            }

            /// <summary>
            /// Gets the byte collection.
            /// </summary>
            public ByteCollection Bytes
            {
                get { return bytes; }
            }

            #region IByteProvider Members
            /// <summary>
            /// True, when changes are done.
            /// </summary>
            public bool HasChanges
            {
                get { return dirty; }
            }

            /// <summary>
            /// Applies changes.
            /// </summary>
            public void ApplyChanges()
            {
                dirty = false;
            }

            /// <summary>
            /// Occurs, when the write buffer contains new changes.
            /// </summary>
            public event EventHandler Changed;

            /// <summary>
            /// Occurs, when InsertBytes or DeleteBytes method is called.
            /// </summary>
            public event EventHandler LengthChanged;

            /// <summary>
            /// Reads a byte from the byte collection.
            /// </summary>
            /// <param name="index">the index of the byte to read</param>
            /// <returns>the byte</returns>
            public byte ReadByte(long index)
            {
                return bytes[(int)index];
            }

            /// <summary>
            /// Write a byte into the byte collection.
            /// </summary>
            /// <param name="index">the index of the byte to write.</param>
            /// <param name="value">the byte</param>
            public void WriteByte(long index, byte value)
            {
                bytes[(int)index] = value;
                OnChanged(EventArgs.Empty);
            }

            /// <summary>
            /// Deletes bytes from the byte collection.
            /// </summary>
            /// <param name="index">the start index of the bytes to delete.</param>
            /// <param name="length">the length of bytes to delete.</param>
            public void DeleteBytes(long index, long length)
            {
                bytes.RemoveRange(
                    (int)Math.Max(0, index),
                    (int)Math.Min((int)Length, length));

                OnLengthChanged(EventArgs.Empty);
                OnChanged(EventArgs.Empty);
            }

            /// <summary>
            /// Inserts byte into the byte collection.
            /// </summary>
            /// <param name="index">the start index of the bytes in the byte collection</param>
            /// <param name="bs">the byte array to insert</param>
            public void InsertBytes(long index, byte[] bs)
            {
                bytes.InsertRange((int)index, bs);

                OnLengthChanged(EventArgs.Empty);
                OnChanged(EventArgs.Empty);
            }

            /// <summary>
            /// Gets the length of the bytes in the byte collection.
            /// </summary>
            public long Length
            {
                get { return bytes.Count; }
            }

            /// <summary>
            /// Returns true
            /// </summary>
            public bool SupportsWriteByte
            {
                get { return true; }
            }

            /// <summary>
            /// Returns true
            /// </summary>
            public bool SupportsInsertBytes
            {
                get { return true; }
            }

            /// <summary>
            /// Returns true
            /// </summary>
            public bool SupportsDeleteBytes
            {
                get { return true; }
            }

            #endregion
        }

        /// <summary>
        /// Byte provider for (big) files.
        /// </summary>
        internal class FileProvider : IByteProvider, IDisposable
        {
            #region WriteCollection class

            /// <summary>
            /// Represents the write buffer class
            /// </summary>
            private class WriteCollection : System.Collections.DictionaryBase
            {
                /// <summary>
                /// Gets or sets a byte in the collection
                /// </summary>
                public byte this[long index]
                {
                    get { return (byte)Dictionary[index]; }
                    set { Dictionary[index] = value; }
                }

                /// <summary>
                /// Adds a byte into the collection
                /// </summary>
                /// <param name="index">the index of the byte</param>
                /// <param name="value">the value of the byte</param>
                public void Add(long index, byte value)
                {
                    Dictionary.Add(index, value);
                }

                /// <summary>
                /// Determines if a byte with the given index exists.
                /// </summary>
                /// <param name="index">the index of the byte</param>
                /// <returns>true, if the is in the collection</returns>
                public bool Contains(long index)
                {
                    return Dictionary.Contains(index);
                }
            }

            #endregion

            /// <summary>
            /// Occurs, when the write buffer contains new changes.
            /// </summary>
            public event EventHandler Changed;

            /// <summary>
            /// Contains all changes
            /// </summary>
            private WriteCollection bytes = new WriteCollection();

            /// <summary>
            /// Contains the file name.
            /// </summary>
            private string fileName = string.Empty;

            /// <summary>
            /// Contains the file stream.
            /// </summary>
            private FileStream fileStream;

            /// <summary>
            /// Initializes a new instance of the FileByteProvider class.
            /// </summary>
            /// <param name="fileName"></param>
            public FileProvider(string file)
            {
                fileName = file;
                fileStream = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            }

            /// <summary>
            /// Raises the Changed event.
            /// </summary>
            /// <remarks>Never used.</remarks>
            private void OnChanged(EventArgs e)
            {
                if (Changed != null) Changed(this, e);
            }

            /// <summary>
            /// Gets the name of the file the byte provider is using.
            /// </summary>
            public string FileName
            {
                get { return fileName; }
            }

            /// <summary>
            /// Returns a value if there are some changes.
            /// </summary>
            /// <returns>true, if there are some changes</returns>
            public bool HasChanges
            {
                get { return bytes.Count > 0; }
            }

            /// <summary>
            /// Updates the file with all changes the write buffer contains.
            /// </summary>
            public void ApplyChanges()
            {
                if (!HasChanges) return;

                var enumerator = bytes.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    long index = (long)enumerator.Key;
                    byte value = (byte)enumerator.Value;
                    if (fileStream.Position != index)
                        fileStream.Position = index;
                    fileStream.Write(new byte[] { value }, 0, 1);
                }
                bytes.Clear();
            }

            /// <summary>
            /// Clears the write buffer and reject all changes made.
            /// </summary>
            public void RejectChanges() { bytes.Clear(); }

            #region IByteProvider Members

            /// <summary>
            /// Never used directly.
            /// </summary>
#pragma warning disable 67
            public event EventHandler LengthChanged;
#pragma warning restore 67

            /// <summary>
            /// Reads a byte from the file.
            /// </summary>
            /// <param name="index">the index of the byte to read</param>
            /// <returns>the byte</returns>
            public byte ReadByte(long index)
            {
                if (bytes.Contains(index))
                    return bytes[index];

                if (fileStream.Position != index)
                    fileStream.Position = index;

                byte res = (byte)fileStream.ReadByte();
                return res;
            }

            /// <summary>
            /// Gets the length of the file.
            /// </summary>
            public long Length
            {
                get { return fileStream.Length; }
            }

            /// <summary>
            /// Writes a byte into write buffer
            /// </summary>
            public void WriteByte(long index, byte value)
            {
                if (bytes.Contains(index))
                    bytes[index] = value;
                else bytes.Add(index, value);

                OnChanged(EventArgs.Empty);
            }

            /// <summary>
            /// Not supported
            /// </summary>
            public void DeleteBytes(long index, long length)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Not supported
            /// </summary>
            public void InsertBytes(long index, byte[] bs)
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Returns true
            /// </summary>
            public bool SupportsWriteByte
            {
                get { return true; }
            }

            /// <summary>
            /// Returns false
            /// </summary>
            public bool SupportsInsertBytes
            {
                get { return false; }
            }

            /// <summary>
            /// Returns false
            /// </summary>
            public bool SupportsDeleteBytes
            {
                get { return false; }
            }

            #endregion

            #region IDisposable Members

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                if (fileStream != null)
                {
                    fileName = null;

                    fileStream.Close();
                    fileStream = null;
                }
            }

            #endregion
        }

        #endregion
    }
}
