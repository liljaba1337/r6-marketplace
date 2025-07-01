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
        public Uri Value { get; private set; }
        /// <summary>
        /// The width of the image. If none is set, this will return -1 and the image will be parsed in the original width.
        /// </summary>
        public int Width
        {
            get
            {
                var query = System.Web.HttpUtility.ParseQueryString(Value.Query);
                if (int.TryParse(query["imwidth"], out int width))
                    return width;
                return -1;
            }
            set
            {
                var uriBuilder = new UriBuilder(Value);
                var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

                query["imwidth"] = value.ToString();
                uriBuilder.Query = query.ToString();

                Value = uriBuilder.Uri;
            }
        }
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
        /// <param name="width">Optional width to resize the image to. If not specified, <see cref="Width"/> will be used</param>
        public async Task<Stream> DownloadImageAsStream(int width = -1)
        {
            HttpResponseMessage response = await Web.Get(new Uri(Value + "?imwidth=" + width));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStreamAsync();
        }
        /// <summary>
        /// Download the image and save it to the specified path.
        /// </summary>
        /// <param name="path">The path to save this image to.</param>
        /// <param name="width">Optional width to resize the image to. If not specified, <see cref="Width"/> will be used</param>
        public async Task DownloadImageAsFile(string path, int width = -1)
        {
            using (var stream = await DownloadImageAsStream(width))
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
