using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DHSTask.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DHSTask.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly string BaseUrl = "https://localhost:44312/api/students";
        HttpClient httpClient;
        public StudentController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(BaseUrl + "/all");

            if (responseMessage.IsSuccessStatusCode)
            {
                var response = responseMessage.Content.ReadAsStringAsync().Result;
                var students = JsonConvert.DeserializeObject<List<StudentModel>>(response);
                return View(students);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentModel student)
        {
            if (student.Fname == string.Empty || student.Lname == string.Empty)
            {
                return BadRequest("Invalid Request");
            }

            string url = BaseUrl + "/Add";
            StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await httpClient.PostAsync(url, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = responseMessage.Content.ReadAsStringAsync().Result;
                var StudentResult = JsonConvert.DeserializeObject<StudentModel>(response);
                return Json(StudentResult);
            }
            else
            {
                return Json("API Error!");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(StudentModel student)
        {
            if (student.Fname == string.Empty || student.Lname == string.Empty)
            {
                return BadRequest("Invalid Request");
            }
            string url = BaseUrl + "/Edit";
            StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await httpClient.PutAsync(url, content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var response = responseMessage.Content.ReadAsStringAsync().Result;

                var studentResult = JsonConvert.DeserializeObject<StudentModel>(response);

                return Json(studentResult);
            }
            else
            {
                return Json("API Error!");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var url = BaseUrl + $"/Delete/?id={id}";
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var response = responseMessage.Content.ReadAsStringAsync().Result;
                return Ok(response);
            }
            else
            {
                return Json("API Error!");
            }
        }
    }
}