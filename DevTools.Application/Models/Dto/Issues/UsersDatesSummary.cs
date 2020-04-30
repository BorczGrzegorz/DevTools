using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Application.Models.Dto.Issues
{
    public class UsersDatesSummary : Dictionary<string, DatesIssueSummaryDto>
    {
        public UsersDatesSummary(IDictionary<string, DatesIssueSummaryDto> dictionary) : base(dictionary)
        {

        }
    }
}
