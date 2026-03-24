using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tests.Data
{
    public class TestDataModel
    {
        public List<string> ValidUsernames { get; set; } = new();
        public List<string> InvalidUsernames { get; set; } = new();
        public List<string> ValidPasswords { get; set; } = new();
        public List<string> InvalidPasswords { get; set; } = new();
    }
}
