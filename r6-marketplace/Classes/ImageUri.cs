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
        /// <summary>
        /// The URI of the image.
        /// </summary>
        public readonly Uri Value;
        internal ImageUri(string value)
        {
            Value = new Uri(value);
        }
        public static implicit operator string(ImageUri str) => str.Value.ToString();
        public static implicit operator Uri(ImageUri str) => str.Value;
        public override string ToString() => Value.ToString();
        /// <summary>
        /// Download the image as a stream.
        /// </summary>
        public async Task<Stream> DownloadImageAsStream()
        {
            HttpResponseMessage response = await Web.Get(Value);
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
        public bool Equals(ImageUri? other) => other != null && Value.Equals(other.Value);
        public override int GetHashCode() => Value.GetHashCode();
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
