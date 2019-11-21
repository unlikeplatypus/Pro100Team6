using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOM
{
    public class User
    {
        public string Token { get; set; }
        public Course[] Courses { get; set; }

        public User(string token)
        {
            Token = token;
            ParseCourses();
        }

        public void ParseCourses()
        {
            string url = $"https://lms.neumont.edu/api/v1/courses.json?access_token={Token};per_page=100";
            var restClient = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = restClient.Execute<List<Course>>(request);
            var currentClassList = response.Data.Where(thingy => thingy.EnrollmentTermId == 13591);
            Courses = currentClassList.ToArray();
            foreach (Course c in Courses)
            {
                c.ParseAssignments(this);
            }
        }
    }
}
