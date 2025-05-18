using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using r6_marketplace.Utils;

namespace r6_marketplace.Classes
{
    /// <summary>
    /// Represents a URI that points to an image. You can use <see cref="DownloadImageAsStream"/> to download the image as a stream
    /// or <see cref="DownloadImageAsFile"/> to save it to a file.
    /// </summary>
    public class ImageUri
    {
        private readonly Uri _value;
        internal ImageUri(string value)
        {
            _value = new Uri(value);
        }
        public static implicit operator string(ImageUri str) => str._value.ToString();
        public static implicit operator Uri(ImageUri str) => str._value;
        public override string ToString() => _value.ToString();
        /// <summary>
        /// Download the image as a stream.
        /// </summary>
        public async Task<Stream> DownloadImageAsStream()
        {
            HttpResponseMessage response = await Web.Get(_value);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync();
        }
        /// <summary>
        /// Download the image and save it to the specified path.
        /// </summary>
        /// <param name="path">The path to save this image to.</param>
        public async Task DownloadImageAsFile(string path)
        {
            using (var stream = await DownloadImageAsStream())
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await stream.CopyToAsync(fileStream);
            }
        }

        // Equality members
        public override bool Equals(object? obj) => Equals(obj as ImageUri);
        public bool Equals(ImageUri? other) => other != null && _value.Equals(other._value);
        public override int GetHashCode() => _value.GetHashCode();
        public static bool operator ==(ImageUri? left, ImageUri? right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }
        public static bool operator !=(ImageUri? left, ImageUri? right) => !(left == right);
    }
}
