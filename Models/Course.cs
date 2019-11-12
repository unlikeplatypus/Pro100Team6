using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOM
{
    class Course : CanvasItem
    {
        [JsonProperty(PropertyName = "course_code")]
        public string CourseCode { get; set; }

        [JsonProperty(PropertyName = "enrollment_term_id")]
        public int EnrollmentTermId { get; set; }

        public Assignment[] Assignments { get; set; }

        public void ParseAssignments(User user, String domain)
        {
                string assignmentURL = $"https://{domain}/api/v1/courses/{Id}/assignments?access_token={user.Token};per_page=100";
                var restClient2 = new RestClient(assignmentURL);
                var request2 = new RestRequest(Method.GET);
                var response2 = restClient2.Execute<List<Assignment>>(request2);
                var upcomingAssignments = response2.Data.Where(duedate => duedate.Due_at >= DateTime.Now).OrderBy(date => date.Due_at);
                Assignments = upcomingAssignments.ToArray();
        }
    }
}
