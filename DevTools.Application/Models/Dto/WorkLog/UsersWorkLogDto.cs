using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.Application.Models.Dto.WorkLog
{
    public class UsersWorkLogDto : Dictionary<string, IEnumerable<WorkLogDto>>
    {
        public UsersWorkLogDto(IDictionary<string, IEnumerable<WorkLogDto>> dictionary) : base(dictionary)
        {

        }    
    }
}
