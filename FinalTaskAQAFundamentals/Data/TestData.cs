using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Data
{
    public static class TestData
    {
        public static IEnumerable<string> ValidUsernames => new List<string>
            {
            "standard_user",            
            "problem_user",
            "performance_glitch_user",
            "error_user",
            "visual_user"
            };

        public static IEnumerable<string> InvalidUsernames => new List<string>
            {
            "standardUser",
            "locked_out_user",
            "problem_user_",
            "performance glitch_user",
            "error123user",
            " visual_юзер"
            };

        public static IEnumerable<string> LockedOutUsers => new List<string>
            {
            "locked_out_user"
            };

        public static IEnumerable<string> ValidPasswords => new List<string>
            {
            "secret_sauce"
            };

        public static IEnumerable<string> InvalidPasswords => new List<string>
            {
            "secret_Sauce",
            "secretSauce",
            "secret Sauce",
            "secret123Sauce"
            };
    }
}
