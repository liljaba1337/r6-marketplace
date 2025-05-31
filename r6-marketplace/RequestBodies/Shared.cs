using System.Reflection;
using System.Text.Json;

namespace r6_marketplace.RequestBodies
{
    internal static class RequestQueries
    {
        internal static readonly Dictionary<string, string> Queries;

        static RequestQueries()
        {
            var assembly = typeof(RequestQueries).Assembly;
            
            Queries = assembly
                .GetManifestResourceNames()
                .Where(name => name.Contains(".Resources.") && name.EndsWith(".txt"))
                .ToDictionary(
                    name => ExtractName(name),
                    name => LoadResource(assembly, name).Replace("\\n", String.Empty)
                );
        }

        private static string LoadResource(Assembly assembly, string resourceName)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException($"Resource {resourceName} not found.");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private static string ExtractName(string fullResourceName) =>
            fullResourceName.Split('.')[^2];

        internal static string Get(string name) =>
            Queries.TryGetValue(name, out var body)
                ? body
                : throw new ArgumentException($"Request body '{name}' not found.");
    }
}

namespace r6_marketplace.RequestBodies.Shared
{
    internal abstract class RequestRoot<TVariables>
    {
        public abstract string operationName { get; }
        public TVariables variables { get; }
        public abstract string query { get; }

        protected RequestRoot(TVariables variables)
        {
            this.variables = variables;
        }

        internal List<RequestRoot<TVariables>> AsList() => new() { this };
        internal string AsJson() => System.Text.Json.JsonSerializer.Serialize(AsList());
    }

    internal abstract class BaseVariables
    {
        public string spaceId { get; } = "0d2ae42d-4c27-4cb7-af6c-2099062302bb";
    }
}