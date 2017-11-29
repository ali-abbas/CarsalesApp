using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CarsalesDataAccess;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Http.Cors;

namespace CarsalesWebApi.Controllers
{

    [System.Web.Http.RoutePrefix("api/vehicle")]
    [EnableCors(origins: "http://localhost:56052/api/vehicle", headers: "*", methods: "*")]
    public class VehicleController : ApiController
    {
        private CarsalesDataAccess.CarsalesDBEntities objDe = new CarsalesDBEntities();

        [System.Web.Http.HttpGet]
        public HttpResponseMessage findAll()
        {
            var serializeData = JsonConvert.SerializeObject(objDe.vehicles.ToList());
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(serializeData);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id}")]
        public HttpResponseMessage find(int id)
        {
            var serializeData = JsonConvert.SerializeObject(objDe.vehicles.Find(id));
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(serializeData);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }

        [System.Web.Http.HttpPost]
        public void create(vehicle objVeh)
        {
            objDe.vehicles.Add(objVeh);
            objDe.SaveChanges();
        }


        [System.Web.Http.HttpPut]
        public void UPDATE(vehicle objVeh)
        {
            objDe.Entry<vehicle>(objVeh).State = System.Data.Entity.EntityState.Modified;
            objDe.SaveChanges();
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("{id}")]
        public void delete(int id)
        {
            objDe.vehicles.Remove(objDe.vehicles.Find(id));
            objDe.SaveChanges();
        }

        [HttpPost]
        [Route("api/test")]
        public string Tests(vehicle obj)
        {
            return obj.Engine;
        }
        
    }
}