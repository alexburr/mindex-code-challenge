using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class ReportingStructure
    {
        public Employee Employee { get; private set; }
        public int NumberOfReports { get; private set; }

        public ReportingStructure(Employee employee)
        {
            Employee = employee;
            NumberOfReports = FindNumberOfReports(Employee);
        }

        private int FindNumberOfReports(Employee employee)
        {
            int numberOfReports = (employee.DirectReports != null) ? employee.DirectReports.Count : 0;

            if (numberOfReports > 0)
            {
                foreach (Employee reportingEmployee in employee.DirectReports)
                {
                    numberOfReports += FindNumberOfReports(reportingEmployee);
                }
            }

            return numberOfReports;
        }
    }
}
