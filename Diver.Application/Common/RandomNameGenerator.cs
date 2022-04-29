using System;
using System.Linq;

namespace Diver.Application.Common
{
    internal static class RandomNameGenerator
    {
        public static string Generate()
        {
            var names = new[]
            {
                "optimistic",
                "pipe",
                "goofy",
                "euler",
                "clever",
                "lewin",
            };

            var postfix = string.Concat(Guid.NewGuid().ToString().Take(5));

            var random = new Random();

            return $"{names[random.Next(names.Length)]}_{names[random.Next(names.Length)]}_{postfix}";
        }
    }
}
