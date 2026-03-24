using Core.Config;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Data
{
    /// <summary>
    /// Loads test data from "testdata.json"
    /// and exposes it through the Data property.
    /// </summary>
    public static class TestDataLoader
    {
        public static TestDataModel Data { get; } = Load();

        private static TestDataModel Load()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Data", "testdata.json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"testdata.json file not found: {path}");
            }

            var json = File.ReadAllText(path);

            var data = JsonSerializer.Deserialize<TestDataModel>(json);

            ArgumentNullException.ThrowIfNull(data);

            return data;
        }
    }
}
