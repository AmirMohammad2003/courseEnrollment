using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Security;
using SystemGroup.General.CourseEnrollment.Common;

namespace SystemGroup.General.CourseEnrollment.Common.Security
{
    public class LogicalResources : ILogicalResourceDeclarator
    {
        public void DeclareLogicalResourceTree(IList<LogicalResourceTreeNode> list)
        {
            var resource = new LogicalResourceCategory("CourseEnrollment", "LogicalResources_CourseEnrollment",
                                new CompositeLogicalResource("Enrollment", "LogicalResources_Enrollment", typeof(Enrollment),
                                    new LogicalResource("New", "LogicalResources_System_Security_UserInfo_New"),
                                    new LogicalResource("Edit", "LogicalResources_General_Security_User_Edit"),
                                    new LogicalResource("Approval", "LogicalResources_EnrollmentApproval")
                                ),
                                new LogicalResourceCategory("Moderator", "LogicalResources_Moderator",
                                new CompositeLogicalResource("Semester", "LogicalResources_Semester", typeof(Semester),
                                    new LogicalResource("New", "LogicalResources_System_Security_UserInfo_New"),
                                    new LogicalResource("Edit", "LogicalResources_General_Security_User_Edit"),
                                    new LogicalResource("Delete", "LogicalResources_General_Security_User_Delete")
                                ),
                                new CompositeLogicalResource("Course", "LogicalResources_Course", typeof(Course),
                                    new LogicalResource("New", "LogicalResources_System_Security_UserInfo_New"),
                                    new LogicalResource("Edit", "LogicalResources_General_Security_User_Edit"),
                                    new LogicalResource("Delete", "LogicalResources_General_Security_User_Delete")
                                ),
                                new CompositeLogicalResource("PartyMajor", "LogicalResources_PartyMajor", typeof(PartyMajor),
                                    new LogicalResource("New", "LogicalResources_System_Security_UserInfo_New"),
                                    new LogicalResource("Edit", "LogicalResources_General_Security_User_Edit"),
                                    new LogicalResource("Delete", "LogicalResources_General_Security_User_Delete")
                                ),
                                new CompositeLogicalResource("SemesterCoursePlan", "LogicalResources_SemesterCoursePlan", typeof(SemesterCoursePlan),
                                    new LogicalResource("New", "LogicalResources_System_Security_UserInfo_New"),
                                    new LogicalResource("Edit", "LogicalResources_General_Security_User_Edit"),
                                    new LogicalResource("Delete", "LogicalResources_General_Security_User_Delete")
                                ),
                                new CompositeLogicalResource("Major", "LogicalResources_Major", typeof(Major),
                                    new LogicalResource("New", "LogicalResources_System_Security_UserInfo_New"),
                                    new LogicalResource("Edit", "LogicalResources_General_Security_User_Edit"),
                                    new LogicalResource("Delete", "LogicalResources_General_Security_User_Delete")
                                ))
                            );

            list.Add(resource);
        }
    }
}
