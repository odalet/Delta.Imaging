using System;
using System.Drawing.Imaging;

namespace TestExif.Drawing.Imaging
{
    public class ExifTag : IEquatable<ExifTag>, IEquatable<int>, IEquatable<ExifTagId>
    {
        private int idValue = 0;
        private string idLabel = ExifSR.Unknown;
        private ExifTagId id =  ExifTagId.Unknown;

        internal static ExifTag FromId(int id)
        {
            return new ExifTag(id);
        }

        private ExifTag(int tagId)
        {
            idValue = tagId;
            ParseId();
        }

        public bool IsUnknown
        {
            get { return id == ExifTagId.Unknown; }
        }

        public ExifTagId Id { get { return id; } }

        public string Label { get { return idLabel; } }

        public int IdValue { get { return idValue; } }

        private void ParseId()
        {
            if (Enum.IsDefined(typeof(ExifTagId), idValue))
            {
                id = (ExifTagId)idValue;
                if (Exif.Labels.ContainsKey(id))
                    idLabel = Exif.Labels[id];
                else idLabel = id.ToString();
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0:X4}] - {1}", idValue, idLabel);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is int) return Equals((int)obj);
            if (obj is ExifTagId) return Equals((ExifTagId)obj);
            if (obj is ExifTag) return Equals((ExifTag)obj);

            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return idValue.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ExifTag t1, ExifTag t2)
        {
            if (t1 == null) return t2 == null;
            return t1.Equals(t2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="t1">The t1.</param>
        /// <param name="t2">The t2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ExifTag t1, ExifTag t2)
        {
            if (t1 == null) return t2 != null;
            return !t1.Equals(t2);
        }

        #region IEquatable<ExifTag> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(ExifTag other)
        {
            if (other == null) return false;
            if (other == this) return true;

            return other.idValue == idValue;
        }

        #endregion

        #region IEquatable<int> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(int other)
        {
            return other == idValue;
        }

        #endregion

        #region IEquatable<ExifTagId> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(ExifTagId other)
        {
            return (int)other == idValue;
        }

        #endregion
    }
}
