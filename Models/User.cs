using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOM
{
    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; private set; }

        public Course[] Courses { get; set; }

        public User(String token, string domain)
        {
            Token = token;
            ParseCourses(domain);
        }

        public void ParseCourses(String domain)
        {
            string url = $"https://{domain}/api/v1/courses.json?access_token={Token};per_page=100";
            var restClient = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = restClient.Execute<List<Course>>(request);
            var currentClassList = response.Data.Where(thingy => thingy.EnrollmentTermId == 13591);
            Courses = currentClassList.ToArray();
            foreach (Course c in Courses)
            {
                c.ParseAssignments(this, domain);
            }
        }
    }
}