using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Exceptions;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Service;
using SystemGroup.General.CourseEnrollment.Common;


namespace SystemGroup.General.CourseEnrollment.Business
{
    public class CourseBusinessValidator : BusinessValidator<Course>
    {
        public override void Validate(Course record, EntityActionType action)
        {
            base.Validate(record, action);

            if (action == EntityActionType.Delete)
            {
                return;
            }

            Dictionary<long, List<long>> graph = [];
            HashSet<long> ids = [];

            var coursePrerequisites = record.Prerequisites;
            foreach (var prerequisite in coursePrerequisites)
            {
                if (prerequisite.CourseRef == prerequisite.PrerequisiteCourseRef)
                {
                    throw this.CreateException("یک درس نمی تواند پیشنیاز خودش باشد.");
                }

                long id1 = prerequisite.CourseRef;
                long id2 = prerequisite.PrerequisiteCourseRef;
                if (!graph.TryGetValue(id1, out List<long> value))
                {
                    value = [];
                    graph[id1] = value;
                }
                value.Add(id2);
                ids.Add(id1);
                ids.Add(id2);
            }

            var prerequisites = ServiceFactory.Create<ICourseBusiness>().FetchDetail<Prerequisite>();
            foreach (var prerequisite in prerequisites)
            {
                long id1 = prerequisite.CourseRef;
                long id2 = prerequisite.PrerequisiteCourseRef;
                if (id1 == record.ID)
                {
                    continue;
                }

                if (!graph.TryGetValue(id1, out List<long> value))
                {
                    value = [];
                    graph[id1] = value;
                }

                value.Add(id2);
            }

            if (DetectCycle(graph, ids))
            {
                throw this.CreateException("میان دروس و پیشنیاز ها نمی تواند چرخه ای وجود داشته باشد.");
            }

        }

        private static bool DetectCycle(IDictionary<long, List<long>> graph, ICollection<long> ids)
        {
            HashSet<long> vis = [];
            HashSet<long> tempVis = [];

            foreach (var id in ids)
            {
                if (DFS(id, graph, vis, tempVis))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool DFS(long i, IDictionary<long, List<long>> graph, ISet<long> vis, ISet<long> tempVis)
        {
            if (vis.Contains(i))
            {
                return false;
            }
            vis.Add(i);
            tempVis.Add(i);

            if (graph.TryGetValue(i, out List<long> value))
            {
                foreach (var adj in value)
                {
                    if (tempVis.Contains(adj) || DFS(adj, graph, vis, tempVis))
                    {
                        return true;
                    }
                }
            }

            tempVis.Remove(i);
            return false;
        }

    }
}
