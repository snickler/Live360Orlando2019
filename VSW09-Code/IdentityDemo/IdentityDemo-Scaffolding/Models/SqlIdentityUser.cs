using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IdentityDemo_Scaffolding.Models
{
    public class SqlIdentityUser :IdentityUser<Guid>
    {
        private static readonly Func<AspNetProfile, string, string> _getProfileProperty =
(profile, property) =>
{
if (profile == null)
{
    return null;
}

var match = Regex.Matches(profile.PropertyNames, @"([A-Z]\w+):(\w+):(\d+):(\d+):").Cast<Match>().FirstOrDefault(x => x.Groups[1].Value == property);
if (match == null)
{
    return null;
}

return profile.PropertyValuesString.Substring(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
};

        public AspNetProfile Profile { get; set; }
        public override string PhoneNumber { get => _getProfileProperty(Profile, "PhoneNumber"); }
        public Guid ApplicationId { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}
