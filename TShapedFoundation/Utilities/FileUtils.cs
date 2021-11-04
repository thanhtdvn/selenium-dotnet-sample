using Newtonsoft.Json;
using System.IO;

namespace TShapedFoundation.Utilities
{
    public class FileUtils
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>T.</returns>
        public T GetData<T>(string filename)
        {
            var datasetJson = File.ReadAllText(filename);
            var dataset = JsonConvert.DeserializeObject<T>(datasetJson);
            return dataset;
        }
    }
}
