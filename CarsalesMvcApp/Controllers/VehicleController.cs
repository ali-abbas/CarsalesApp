using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsalesDataAccess;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Helpers;

namespace CarsalesMvcApp.Controllers
{
    public class VehicleController : Controller
    {
        CarsalesDBEntities entities = new CarsalesDBEntities();
        string BaseUrl = "http://localhost:56052";

        public async Task<ActionResult> Index()
        {
            List<vehicle> objVeh = new List<vehicle>();

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                //define request data format
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Sending request to find web api Rest service resouces findAll
                HttpResponseMessage Res = await client.GetAsync("api/vehicle");

                //Checking the response is successfull or not which is sent using httpClinet

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the respoinse details recieved from web api
                    var VehicleResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response from web api and storing into Vehicle list

                    objVeh = JsonConvert.DeserializeObject<List<vehicle>>(VehicleResponse);


                }

            }

            return View(objVeh);
        }


        public async Task<ActionResult> Edit(int Id,int Type)
        {
            vehicle objVeh = new vehicle();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                //define request data format
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Sending request to find web api Rest service resouces findAll
                HttpResponseMessage Res = await client.GetAsync("api/vehicle/"+Id);

                //Checking the response is successfull or not which is sent using httpClinet

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the respoinse details recieved from web api
                    var VehicleResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response from web api and storing into Vehicle list

                    objVeh = JsonConvert.DeserializeObject<vehicle>(VehicleResponse);

                }

            }

            return View(objVeh);
        }
        public async Task<ActionResult> Update(int Id, int Type)
        {
            vehicle objVeh = new vehicle();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();

                //define request data format
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Sending request to find web api Rest service resouces findAll
                HttpResponseMessage Res = await client.GetAsync("api/vehicle/" + Id);

                //Checking the response is successfull or not which is sent using httpClinet

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the respoinse details recieved from web api
                    var VehicleResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response from web api and storing into Vehicle list

                    objVeh = JsonConvert.DeserializeObject<vehicle>(VehicleResponse);


                }

            }

            return View(objVeh);
        }

        [HttpPost]
        public bool Update1(vehicle objVeh)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    var result = client.PostAsync("/api/vehicle", objVeh, new JsonMediaTypeFormatter()).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return true;
                        //Console.writeLine("Performance instance successfully sent to the API");
                    }
                    else
                    {
                        return false;
                    }

                }
                
            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}