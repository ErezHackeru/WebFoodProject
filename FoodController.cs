using ClassLibraryEFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication_Food.Controllers
{
    public class FoodController : ApiController
    {
        FoodDAO foodDAO = new FoodDAO();
        
        static FoodController()
        {
            
        }

        // GET api/values
        public HttpResponseMessage Get()
        {
            if (foodDAO.GetAll().Count == 0)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, foodDAO.GetAll());
            return msg;            
        }

        
        // GET api/values/5
        [HttpGet]
        public HttpResponseMessage Get([FromUri] int id)
        {
            Food result = foodDAO.GetById(id);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, result);
            return msg;             
        }

        //...//api/food/ByName/{name}
        [Route("api/food/ByName/{name}")]
        [HttpGet]
        public HttpResponseMessage GetBySenderName([FromUri] string name)
        {
            List<Food> result = foodDAO.GetByName(name);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, result);
            return msg;
        }

        //...//api/food/ByName/{calories}
        [Route("api/food/ByMaxCal/{calories}")]
        [HttpGet]
        public HttpResponseMessage GetBySenderCalories([FromUri] int calories)
        {
            List<Food> result = foodDAO.GetByAboveCalory(calories);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, result);
            return msg;
        }

        //...//api/food/search
        // add ? sender = d & text = o
        [Route("api/food/search")]
        [HttpGet]
        public HttpResponseMessage SearchFoodsByCriteria(string name = "", int min_cal = 0, int max_cal = int.MaxValue, int min_grade = 0)
        {
            List<Food> result = foodDAO.SearchFoodsByCriteria(name, max_cal, min_cal, min_grade);
            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NoContent);
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, result);
            return msg;
        }

        /*
        // GET api/values/5
        [Route("api/messages/bysender/{sender}")]
        [HttpGet]
        public IEnumerable<Message> GetBySenderName([FromUri] string sender)
        {
            IEnumerable<Message> result = messages.Where(m => m.Sender.ToUpper() == sender.ToUpper());
            return result;
        }

        // GET api/ -- Targil Kita
        [Route("api/messages/bytext/{text}")]
        [HttpGet]
        public IEnumerable<Message> GetByTextInText([FromUri] string text)
        {
            IEnumerable<Message> result = messages.Where(m => m.Text.ToUpper().Contains(text.ToUpper()));
            return result;
        }
        */



        [HttpPost]
        // POST api/values
        public void Post([FromBody]Food food)
        {   
            foodDAO.AddFood(food);
            Request.CreateResponse(HttpStatusCode.Accepted, food);
        }

        [HttpPut]
        // PUT api/values/5
        public void Put(int id, [FromBody]Food food)
        {
            Food result = foodDAO.GetById(id);
            if (result != null)
            {
                foodDAO.UpdateFood(food);
            }
        }

        [HttpDelete]
        // DELETE api/values/5
        public void Delete(int id)
        {
            Food result = foodDAO.GetById(id);
            if (result != null)
                foodDAO.RemoveFood(id);

        }

        /*
        [Route("api/messages/search")]
        // GET api/values
        public List<Message> GetByFilter(string sender = null)
        {
            List<Message> result = messages.Where(m => sender == null || m.Sender.ToUpper() == sender.ToUpper()).ToList();
            return result;
        }


        // query string
        // ...api/messages/search ? sender = d & text = o
        // ...api/messages/search ? text = o & sender = d 
        [Route("api/messages/search")]
        [HttpGet]
        public IEnumerable<Message> GetByFilter(string sender = "", string text = "")
        {
            if (sender == "" && text != "")
                return messages.Where(m => m.Text.ToUpper().Contains(text.ToUpper()));
            if (sender != "" && text == "")
                return messages.Where(m => m.Sender.ToUpper().Contains(sender.ToUpper())); ;
            return messages.Where(m => m.Sender.ToUpper().Contains(sender.ToUpper()) && m.Text.ToUpper().Contains(text.ToUpper()));
        }

        [Route("api/messages/biggerThanId")]
        [HttpGet]
        public IEnumerable<Message> BiggerThanId(int id = 0)
        {
            // if nothing sent return all

            // if id was sent return all messages with ID bigger than the id sent...
            IEnumerable<Message> result = messages.Where(m => m.Id > id);
            return result;
        }
        */
    }
}
