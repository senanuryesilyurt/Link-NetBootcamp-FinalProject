using Data.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EntityFramework
{
    public class ReportDal : IReportDal
    {
        public void Get()
        {
            using( PostgreSqlContext context = new PostgreSqlContext())
            {
                
            }
        }
    }
}
