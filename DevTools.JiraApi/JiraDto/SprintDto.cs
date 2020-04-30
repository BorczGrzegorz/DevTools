using DevTools.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevTools.JiraApi.JiraDto
{
    public class SprintDto
    {
        public int Id { get; set; }
        public SprintState State { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
