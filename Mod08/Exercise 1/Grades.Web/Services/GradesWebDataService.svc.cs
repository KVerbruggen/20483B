//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.Data.Services.Providers;
using System.Data.Services.Client;
using Grades.DataModel;
using Microsoft.Data;

namespace Grades.Web.Services
{
    public class GradesWebDataService : DataService<SchoolGradesDBEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            
            config.SetEntitySetAccessRule("Grades", EntitySetRights.All);
            config.SetEntitySetAccessRule("Teachers", EntitySetRights.All);
            config.SetEntitySetAccessRule("Students", EntitySetRights.All);
            config.SetEntitySetAccessRule("Subjects", EntitySetRights.All);
            config.SetEntitySetAccessRule("Users", EntitySetRights.All);
            config.SetServiceOperationAccessRule("StudentsInClass", ServiceOperationRights.AllRead);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        [WebGet]
        public IEnumerable<Student> StudentsInClass(string className)
        {
            return (from student in this.CurrentDataSource.Students
                    where student.Teacher.Class == className
                    select student);
        }

    }
}
