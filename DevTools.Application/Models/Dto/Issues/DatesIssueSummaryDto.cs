using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Application.Models.Dto.Issues
{
    public class DatesIssueSummaryDto : Dictionary<DateTime, List<IssueSummaryDto>>
    {
        public DatesIssueSummaryDto(IDictionary<DateTime, List<IssueSummaryDto>> dictionary) : base(dictionary)
        {

        }
    }
}
