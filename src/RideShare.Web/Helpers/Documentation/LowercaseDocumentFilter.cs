using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RideShare.Web.Helpers.Documentation
{
    public class LowercaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths;

            //	generate the new keys
            var newPaths = new OpenApiPaths();
            var removeKeys = new List<string>();
            foreach (var path in paths)
            {
                var routes = path.Key.Split('/');
                string newKey = "/" + string.Join('/', routes
                    .Where(x => !string.IsNullOrEmpty(x) && x.Length > 1)
                    .Select(s => char.ToLowerInvariant(s[0]) + s.Remove(0, 1)));


                if (newKey != path.Key)
                {
                    removeKeys.Add(path.Key);
                    newPaths.Add(newKey, path.Value);
                }
            }

            //	add the new keys
            foreach (var path in newPaths)
            {
                swaggerDoc.Paths.Add(path.Key, path.Value);
            }

            //	remove the old keys
            foreach (var key in removeKeys)
            {
                swaggerDoc.Paths.Remove(key);
            }
        }
    }
}
